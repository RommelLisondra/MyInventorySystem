using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SalesReturnService : ISalesReturnService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesReturnService));

        public SalesReturnService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesReturnMasterDto>> GetAll()
        {
            try
            {
                var salesReturns = await _unitOfWork.SalesReturnRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SalesReturnMaster>, IEnumerable<SalesReturnMasterDto>>(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SalesReturnMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SalesReturnRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SalesReturnMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<SalesReturnMasterDto>>
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

        public async Task<SalesReturnMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SalesReturnMaster, SalesReturnMasterDto>(await _unitOfWork.SalesReturnRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSalesReturnAsync(SalesReturnMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (dto.Total <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedSalesReturn = _mapper.Map<Domain.Entities.SalesReturnMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesReturnMaster.Create(mappedSalesReturn, createdBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesReturnDetailFile ?? Enumerable.Empty<SalesReturnDetailDto>())
                {
                    if (detailDto.QtyRet <= 0)
                        throw new InvalidOperationException($"Invalid QtyInv for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.SalesReturnQuantity(detailDto.QtyRet);
                    //await _unitOfWork.ItemDetailRepository.UpdateFieldAsync(item.id, nameof(Domain.Entities.ItemDetail.LastSrno), dto.Srmno);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var siDetail = await _unitOfWork.SalesInvoiceRepository.GetBySINoAndItemAsync(dto.Simno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"SO detail not found for {dto.Simno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyRet > siDetail.QtyInv)
                        throw new InvalidOperationException($"Invoiced quantity exceeds remaining SO quantity for item {detailDto.ItemDetailCode}.");

                    siDetail.UpdateInvoicedQty(detailDto.QtyRet);
                    await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceDetailStatusAsync(siDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, createdBy);

                    var newDetail = new Domain.Entities.SalesReturnDetail
                    {
                        Srdno = dto.Srmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesReturnRepository.AddAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                    ?? throw new InvalidOperationException($"Customer with CustNo '{dto.CustNo}' not found.");

                customer.UpdateLastSrno(dto.Srmno);
                await _unitOfWork.CustomerRepository.UpdateFieldAsync(customer.id, nameof(Domain.Entities.Customer.LastSrno), dto.Srmno);

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
                logCentral.Error("CreateSalesReturnAsync", ex);
                throw;
            }
        }

        public async Task UpdateSalesReturnAsync(SalesReturnMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (dto.Total <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedSalesReturn = _mapper.Map<Domain.Entities.SalesReturnMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesReturnMaster.Update(mappedSalesReturn, editedBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesReturnDetailFile ?? Enumerable.Empty<SalesReturnDetailDto>())
                {
                    if (detailDto.QtyRet <= 0)
                        throw new InvalidOperationException($"Invalid QtyInv for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.SalesReturnQuantity(detailDto.QtyRet);
                    //await _unitOfWork.ItemDetailRepository.UpdateFieldAsync(item.id, nameof(Domain.Entities.ItemDetail.LastSrno), dto.Srmno);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var siDetail = await _unitOfWork.SalesInvoiceRepository.GetBySINoAndItemAsync(dto.Simno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"SO detail not found for {dto.Simno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyRet > siDetail.QtyInv)
                        throw new InvalidOperationException($"Invoiced quantity exceeds remaining SO quantity for item {detailDto.ItemDetailCode}.");

                    siDetail.UpdateInvoicedQty(detailDto.QtyRet);
                    await _unitOfWork.SalesInvoiceRepository.UpdateSalesInvoiceDetailStatusAsync(siDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, editedBy);

                    var newDetail = new Domain.Entities.SalesReturnDetail
                    {
                        Srdno = dto.Srmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesReturnRepository.UpdateAsync(master);

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
                logCentral.Error("UpdateSalesReturnAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalesReturnMasterDto>> GetSalesinvoiceByCustNoAsync(string custNo)
        {
            try
            {
                var salesReturns = await _unitOfWork.SalesReturnRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<SalesReturnMasterDto>>(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSalesinvoiceByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<SalesReturnMasterDto?> GetSalesinvoiceByIdAsync(int id)
        {
            try
            {
                var salesReturns = await _unitOfWork.SalesReturnRepository.FindAsync(e => e.id == id);
                var item = salesReturns.FirstOrDefault();
                return item == null ? null : _mapper.Map<SalesReturnMasterDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSalesinvoiceByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalesReturnMasterDto>> SearchSalesReturnByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var salesReturns = await _unitOfWork.SalesReturnRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Srmno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Simno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Date.HasValue ? e.Date.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SalesReturnMasterDto>>(salesReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSalesReturnByKeywordAsync", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransactionAsync(Domain.Entities.SalesReturnMaster master, SalesReturnDetailDto detailDto, string createdBy)
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
                RefModule = "Sales Returned",
                RefNo = master.Srmno,
                RefId = master.id,
                Quantity = (int)detailDto.QtyRet,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = "IN",
                Remarks = "Returned to Customer" + " " + master.CustNo,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);

            await UpdateInventoryBalanceAsync(detailDto.ItemDetailCode, warehouse.id, detailDto.QtyRet);
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
                inventoryBalance.QuantityOnHand += qty;
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
