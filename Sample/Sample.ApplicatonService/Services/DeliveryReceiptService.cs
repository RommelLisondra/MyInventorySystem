using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class DeliveryReceiptService : IDeliveryReceiptService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DeliveryReceiptService));

        public DeliveryReceiptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryReceiptMasterDto>> GetAll()
        {
            try
            {
                var itemList = await _unitOfWork.DeliveryReceiptRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.DeliveryReceiptMaster>, IEnumerable<DeliveryReceiptMasterDto>>(itemList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<DeliveryReceiptMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.DeliveryReceiptRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<DeliveryReceiptMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<DeliveryReceiptMasterDto>>
                {
                    Data = dtoData,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalRecords = result.TotalRecords,
                    TotalPages = result.TotalPages
                };
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAllPaged", ex);
                throw;
            }
        }

        public async Task<DeliveryReceiptMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.DeliveryReceiptMaster, DeliveryReceiptMasterDto>(await _unitOfWork.DeliveryReceiptRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateDeliveryReceiptAsync(DeliveryReceiptMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (string.IsNullOrWhiteSpace(dto.Drmno))
                throw new ArgumentException("Delivery Receipt number cannot be null or empty.", nameof(dto.Drmno));

            if (dto.Total <= 0)
                throw new ArgumentException("Delivery total amount must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedDelivery = _mapper.Map<Domain.Entities.DeliveryReceiptMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Delivery Receipt Master is null.");

                var master = Domain.Entities.DeliveryReceiptMaster.Create(mappedDelivery, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Delivery Receipt Master domain entity.");

                foreach (var detailDto in dto.DeliveryReceiptDetail ?? Enumerable.Empty<DeliveryReceiptDetailDto>())
                {
                    if (detailDto.QtyDel <= 0)
                        throw new InvalidOperationException($"Invalid QtyInv for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var siDetail = await _unitOfWork.SalesInvoiceRepository.GetBySINoAndItemAsync(dto.Simno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Sales invoice detail not found for {dto.Simno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyDel > siDetail.QtyInv)
                        throw new InvalidOperationException($"Delivery quantity exceeds remaining Sales invoice quantity for item {detailDto.ItemDetailCode}.");

                    siDetail.UpdateInvoicedQty(detailDto.QtyDel);
                    await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceDetailStatusAsync(siDetail);

                    var newDetail = new Domain.Entities.DeliveryReceiptDetail
                    {
                        Drdno = dto.Drmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyDel = detailDto.QtyDel,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.DeliveryReceiptRepository.AddAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                    ?? throw new InvalidOperationException($"Customer with CustNo '{dto.CustNo}' not found.");

                customer.UpdateLastDrno(dto.Drmno);
                await _unitOfWork.CustomerRepository.UpdateFieldAsync(customer.id, nameof(Domain.Entities.Customer.LastDrno), dto.Drmno);

                var siDetails = await _unitOfWork.SalesInvoiceRepository.GetSIDetailsBySiNoAsync(dto.Simno);
                if (siDetails == null)
                    throw new InvalidOperationException($"Sales Invoice details not found for {dto.Simno}.");

                var newStatus = siDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceStatusAsync(dto.Simno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("CreateDeliveryReceiptAsync", ex);
                throw;
            }
        }

        public async Task UpdateDeliveryReceiptAsync(DeliveryReceiptMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (string.IsNullOrWhiteSpace(dto.Drmno))
                throw new ArgumentException("Delivery Receipt number cannot be null or empty.", nameof(dto.Drmno));

            if (dto.Total <= 0)
                throw new ArgumentException("Delivery total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedDelivery = _mapper.Map<Domain.Entities.DeliveryReceiptMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Delivery Receipt Master is null.");

                var master = Domain.Entities.DeliveryReceiptMaster.Update(mappedDelivery, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Delivery Receipt Master domain entity.");

                foreach (var detailDto in dto.DeliveryReceiptDetail ?? Enumerable.Empty<DeliveryReceiptDetailDto>())
                {
                    if (detailDto.QtyDel <= 0) throw new InvalidOperationException($"Invalid Quantity delivered for item {detailDto.ItemDetailCode}.");

                    var siDetail = await _unitOfWork.SalesInvoiceRepository.GetBySINoAndItemAsync(dto.Simno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Sales invoice detail not found for {dto.Simno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyDel > siDetail.QtyInv)
                        throw new InvalidOperationException($"Delivered quantity exceeds remaining sales invoice quantity for item {detailDto.ItemDetailCode}.");

                    siDetail.UpdateInvoicedQty(detailDto.QtyDel);
                    await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceDetailStatusAsync(siDetail);

                    var newDetail = new Domain.Entities.DeliveryReceiptDetail
                    {
                        Drdno = dto.Drmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyDel = detailDto.QtyDel,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.DeliveryReceiptRepository.UpdateAsync(master);

                var siDetails = await _unitOfWork.SalesInvoiceRepository.GetSIDetailsBySiNoAsync(dto.Simno);
                if (siDetails == null)
                    throw new InvalidOperationException($"Sales Invoice details not found for {dto.Simno}.");

                var newStatus = siDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceStatusAsync(dto.Simno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("UpdateDeliveryReceiptAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<DeliveryReceiptMasterDto>> GetItemsByCustNoAsync(string custNo)
        {
            try
            {
                var items = await _unitOfWork.DeliveryReceiptRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<DeliveryReceiptMasterDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<DeliveryReceiptMasterDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.DeliveryReceiptRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<DeliveryReceiptMasterDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<DeliveryReceiptMasterDto>> SearchDeliveryReceiptByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var deliveries = await _unitOfWork.DeliveryReceiptRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Drmno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Simno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<DeliveryReceiptMasterDto>>(deliveries);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchDeliveryReceiptByKeywordAsync", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransactionAsync(Domain.Entities.DeliveryReceiptMaster master, DeliveryReceiptDetailDto detailDto, string createdBy)
        {
            var warehouse = await _unitOfWork.WarehouseRepository.GetByWarehouseCodeAsync(detailDto.ItemDetailCodeNavigation.WarehouseCode);

            if (warehouse == null)
            {
                throw new InvalidOperationException($"Warehouse with code {detailDto.ItemDetailCodeNavigation.WarehouseCode} not found.");
            }

            var invTrans = new Domain.Entities.InventoryTransaction
            {
                TransactionDate = DateTime.Now,
                CompanyId = master.Branch.CompanyId,
                BranchId = master.BranchId,
                WarehouseId = warehouse.id,
                ItemDetailNo = detailDto.ItemDetailCode,
                RefModule = "Delivery",
                RefNo = master.Drmno,
                RefId = master.id,
                Quantity = (int)detailDto.QtyDel,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = "OUT",
                Remarks = "Delivered to Customer" + " " + master.CustNo,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);

            await UpdateInventoryBalanceAsync(detailDto.ItemDetailCode, warehouse.id, detailDto.QtyDel);
        }

        public async Task UpdateInventoryBalanceAsync(string itemDetailNo, int warehouseId, int qty)
        {
            var inventoryBalance = await _unitOfWork.InventoryBalanceRepository
                .GetByitemDetailNoandWarehouseIdAsync(itemDetailNo, warehouseId);

            if (inventoryBalance == null)
            {
                var createnewBalance = new Domain.Entities.InventoryBalance
                {
                    ItemDetailNo = itemDetailNo,
                    WarehouseId = warehouseId,
                    QuantityOnHand = qty,
                    LastUpdated = DateTime.Now,
                    RecStatus = "A"
                };

                var createinventoryBalance = Domain.Entities.InventoryBalance.Create(createnewBalance, "")
                    ?? throw new InvalidOperationException("Failed to create inventory balance domain entity.");

                await _unitOfWork.InventoryBalanceRepository.AddAsync(createinventoryBalance);
            }
            else
            {
                inventoryBalance.QuantityOnHand -= qty;
                inventoryBalance.LastUpdated = DateTime.Now;

                var updateinventoryBalance = Domain.Entities.InventoryBalance.Update(inventoryBalance, "")
                    ?? throw new InvalidOperationException("Failed to update inventory balance domain entity.");

                await _unitOfWork.InventoryBalanceRepository.UpdateAsync(updateinventoryBalance);
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
