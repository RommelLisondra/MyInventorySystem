using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class PurchaseReturnService : IPurchaseReturnService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseReturnService));

        public PurchaseReturnService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseReturnMasterDto>> GetAll()
        {
            try
            {
                var pReturnList = await _unitOfWork.PurchaseReturnRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.PurchaseReturnMaster>, IEnumerable<PurchaseReturnMasterDto>>(pReturnList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<PurchaseReturnMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.PurchaseReturnRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<PurchaseReturnMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<PurchaseReturnMasterDto>>
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

        public async Task<PurchaseReturnMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.PurchaseReturnMaster, PurchaseReturnMasterDto>
                    (await _unitOfWork.PurchaseReturnRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreatePurchaseReturnAsync(PurchaseReturnMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.PretMno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.PretMno));

            if (string.IsNullOrWhiteSpace(dto.Rrno))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Rrno));

            if (string.IsNullOrWhiteSpace(dto.SupplierNo))
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupplierNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase return total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.PurchaseReturnMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.PurchaseReturnMaster.Create(mappedInvoice, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.PurchaseReturnDetailFile ?? Enumerable.Empty<PurchaseReturnDetailDto>())
                {
                    if (detailDto.QtyRet <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.PurchaseReturnQuantity(detailDto.QtyRet);

                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var rrDetail = await _unitOfWork.ReceivingReportRepository.GetByRRDNoAndItemAsync(dto.Rrno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Receiving report detail not found for {dto.Rrno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyRet > rrDetail.QtyReceived)
                        throw new InvalidOperationException($"purchase received quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    rrDetail.AddPurchaseReturnQuantity(detailDto.QtyRet, rrDetail.QtyReceived);
                    await _unitOfWork.ReceivingReportRepository.UpdateReceivingQtyReceiveAsync(rrDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, createdBy);

                    var newDetail = new Domain.Entities.PurchaseReturnDetail
                    {
                        PretDno = dto.PretMno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.PurchaseReturnRepository.AddAsync(master);

                var rrDetails = await _unitOfWork.ReceivingReportRepository.GetRRDetailsByRrNoAsync(dto.Rrno);
                if (rrDetails == null)
                    throw new InvalidOperationException($"Purchase order details not found for {dto.Rrno}.");

                var newStatus = rrDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.ReceivingReportRepository.UpdateReceivingStatusAsync(dto.Rrno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreatePurchaseOrderAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdatePurchaseRetunrAsync(PurchaseReturnMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.PretMno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.PretMno));

            if (string.IsNullOrWhiteSpace(dto.Rrno))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Rrno));

            if (string.IsNullOrWhiteSpace(dto.SupplierNo))
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupplierNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase return total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.PurchaseReturnMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.PurchaseReturnMaster.Update(mappedInvoice, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.PurchaseReturnDetailFile ?? Enumerable.Empty<PurchaseReturnDetailDto>())
                {
                    if (detailDto.QtyRet <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.PurchaseReturnQuantity(detailDto.QtyRet);

                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var rrDetail = await _unitOfWork.ReceivingReportRepository.GetByRRDNoAndItemAsync(dto.Rrno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Receiving report detail not found for {dto.Rrno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyRet > rrDetail.QtyReceived)
                        throw new InvalidOperationException($"purchase received quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    rrDetail.AddPurchaseReturnQuantity(detailDto.QtyRet, rrDetail.QtyReceived);
                    await _unitOfWork.ReceivingReportRepository.UpdateReceivingQtyReceiveAsync(rrDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, editedBy);

                    var newDetail = new Domain.Entities.PurchaseReturnDetail
                    {
                        PretDno = dto.PretMno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.PurchaseReturnRepository.AddAsync(master);

                var rrDetails = await _unitOfWork.ReceivingReportRepository.GetRRDetailsByRrNoAsync(dto.Rrno);
                if (rrDetails == null)
                    throw new InvalidOperationException($"Purchase order details not found for {dto.Rrno}.");

                var newStatus = rrDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.ReceivingReportRepository.UpdateReceivingStatusAsync(dto.Rrno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdatePurchaseRetunrAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseReturnMasterDto>> GetPurchaseReturnByPrNoAsync(string prNo)
        {
            try
            {
                var purchaseReturns = await _unitOfWork.PurchaseReturnRepository.FindAsync(e => e.PretMno == prNo);
                return _mapper.Map<IEnumerable<PurchaseReturnMasterDto>>(purchaseReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetPurchaseReturnByPrNoAsync", ex);
                throw;
            }
        }

        public async Task<PurchaseReturnMasterDto?> GetPurchaseReturnByIdAsync(int id)
        {
            try
            {
                var purchaseReturns = await _unitOfWork.PurchaseReturnRepository.FindAsync(e => e.id == id);
                var purchaseReturn = purchaseReturns.FirstOrDefault();
                return purchaseReturn == null ? null : _mapper.Map<PurchaseReturnMasterDto>(purchaseReturn);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetPurchaseReturnByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseReturnMasterDto>> SearchPurchaseReturnByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var purchaseReturns = await _unitOfWork.PurchaseReturnRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                        EF.Functions.Like((e.PretMno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                         EF.Functions.Like((e.Rrno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                         EF.Functions.Like((e.RefNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                         EF.Functions.Like((e.SupplierNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.Date.HasValue ? e.Date.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<PurchaseReturnMasterDto>>(purchaseReturns);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchPurchaseReturnByKeywordAsync", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransactionAsync(Domain.Entities.PurchaseReturnMaster master, PurchaseReturnDetailDto detailDto, string createdBy)
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
                RefModule = "Purchase Returned",
                RefNo = master.PretMno,
                RefId = master.id,
                Quantity = (int)detailDto.QtyRet,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = "OUT",
                Remarks = "Returned to Supplier" + " " + master.SupplierNo,
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
