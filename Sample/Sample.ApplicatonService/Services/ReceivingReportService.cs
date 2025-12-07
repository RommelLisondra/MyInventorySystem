using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;
using System.Diagnostics.Metrics;

namespace Sample.ApplicationService.Services
{
    public class ReceivingReportService : IReceivingReportService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ReceivingReportService));

        public ReceivingReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceivingReportMasterDto>> GetAll()
        {
            try
            {
                var receivingReports = await _unitOfWork.ReceivingReportRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ReceivingReportMaster>, IEnumerable<ReceivingReportMasterDto>>(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ReceivingReportMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ReceivingReportRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ReceivingReportMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<ReceivingReportMasterDto>>
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

        public async Task<ReceivingReportMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ReceivingReportMaster, ReceivingReportMasterDto>
                    (await _unitOfWork.ReceivingReportRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateReceivingReportAsync(ReceivingReportMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Rrmno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.Rrmno));

            if (string.IsNullOrWhiteSpace(dto.Pono))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Pono));

            if (dto.SupNo == Guid.Empty)
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase Order total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.ReceivingReportMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.ReceivingReportMaster.Create(mappedInvoice, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.ReceivingReportDetailFile ?? Enumerable.Empty<ReceivingReportDetailDto>())
                {
                    if (detailDto.QtyReceived <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.DecreaseQuatityOnOrder(detailDto.QtyReceived);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var poDetail = await _unitOfWork.PurchaseOrderRepository.GetByPODNoAndItemAsync(dto.Pono, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Purchase order detail not found for {dto.Pono} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyReceived > poDetail.QtyOrder)
                        throw new InvalidOperationException($"purchase received quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    poDetail.AddPurchaseOrderQtyOnOrder(detailDto.QtyReceived, poDetail.QtyReceived);
                    await _unitOfWork.PurchaseOrderRepository.UpdatePurchaseOrderQtyReceiveAsync(poDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, createdBy);

                    var newDetail = new Domain.Entities.ReceivingReportDetail
                    {
                        Rrdno = dto.Rrmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        QtyReceived = detailDto.QtyReceived,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        PretrunRecStatus = "O",
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);                   
                }

                await _unitOfWork.ReceivingReportRepository.AddAsync(master);

                var poDetails = await _unitOfWork.PurchaseOrderRepository.GetPODetailsByPoNoAsync(dto.Pono);
                if (poDetails == null)
                    throw new InvalidOperationException($"Purchase order details not found for {dto.Pono}.");

                var newStatus = poDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                var newRRRecStatus = poDetails.All(s => s.RrrecStatus == "C") ? "C" : "O";
                await _unitOfWork.PurchaseOrderRepository.UpdatePurchaseOrderStatusAsync(dto.Pono, newStatus, newRRRecStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateReceivingReportAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateReceivingReportAsync(ReceivingReportMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Rrmno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.Rrmno));

            if (string.IsNullOrWhiteSpace(dto.Pono))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Pono));

            if (dto.SupNo == Guid.Empty)
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase Order total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.ReceivingReportMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.ReceivingReportMaster.Update(mappedInvoice, editedBy)
                    ?? throw new InvalidOperationException("Failed to Update Purchase Order Master domain entity.");

                foreach (var detailDto in dto.ReceivingReportDetailFile ?? Enumerable.Empty<ReceivingReportDetailDto>())
                {
                    if (detailDto.QtyReceived <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.DecreaseQuatityOnOrder(detailDto.QtyReceived);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var poDetail = await _unitOfWork.PurchaseOrderRepository.GetByPODNoAndItemAsync(dto.Pono, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Purchase order detail not found for {dto.Pono} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyReceived > poDetail.QtyOrder)
                        throw new InvalidOperationException($"purchase received quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    poDetail.AddPurchaseOrderQtyOnOrder(detailDto.QtyReceived, poDetail.QtyReceived);
                    await _unitOfWork.PurchaseOrderRepository.UpdatePurchaseOrderQtyReceiveAsync(poDetail);

                    await CreateInventoryTransactionAsync(master, detailDto, editedBy);

                    var newDetail = new Domain.Entities.ReceivingReportDetail
                    {
                        Rrdno = dto.Rrmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyRet = detailDto.QtyRet,
                        QtyReceived = detailDto.QtyReceived,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        PretrunRecStatus = detailDto.PretrunRecStatus,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.ReceivingReportRepository.AddAsync(master);

                var poDetails = await _unitOfWork.PurchaseOrderRepository.GetPODetailsByPoNoAsync(dto.Pono);
                if (poDetails == null)
                    throw new InvalidOperationException($"Purchase order details not found for {dto.Pono}.");

                var newStatus = poDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                var newRRRecStatus = poDetails.All(s => s.RrrecStatus == "C") ? "C" : "O";
                await _unitOfWork.PurchaseOrderRepository.UpdatePurchaseOrderStatusAsync(dto.Pono, newStatus, newRRRecStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateReceivingReportAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ReceivingReportMasterDto>> GetReceivingReportByRrNoAsync(string rrNo)
        {
            try
            {
                var receivingReports = await _unitOfWork.ReceivingReportRepository.FindAsync(e => e.Rrmno == rrNo);
                return _mapper.Map<IEnumerable<ReceivingReportMasterDto>>(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetReceivingReportByRrNoAsync", ex);
                throw;
            }
        }

        public async Task<ReceivingReportMasterDto?> GetReceivingReportByIdAsync(int id)
        {
            try
            {
                var receivingReports = await _unitOfWork.ReceivingReportRepository.FindAsync(e => e.id == id);
                var receivingReport = receivingReports.FirstOrDefault();
                return receivingReport == null ? null : _mapper.Map<ReceivingReportMasterDto>(receivingReport);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetReceivingReportByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ReceivingReportMasterDto>> SearchReceivingReportByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var receivingReports = await _unitOfWork.ReceivingReportRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                        EF.Functions.Like((e.Rrmno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.Pono ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.RefNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.SupNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.Date.HasValue ? e.Date.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ReceivingReportMasterDto>>(receivingReports);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchReceivingReportByKeywordAsync", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransactionAsync(Domain.Entities.ReceivingReportMaster master, ReceivingReportDetailDto detailDto , string createdBy)
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
                RefModule = "Receiving",
                RefNo = master.Rrmno,
                RefId = master.id,
                Quantity = (int)detailDto.QtyReceived,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = "IN",
                Remarks = "Received from Supplier" + " " + master.SupNo,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);

            await UpdateInventoryBalanceAsync(detailDto.ItemDetailCode, warehouse.id, detailDto.QtyReceived);
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
