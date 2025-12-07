using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class StockCountService : IStockCountService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(StockCountService));

        public StockCountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockCountMasterDto>> GetAll()
        {
            try
            {
                var stockCount = await _unitOfWork.StockCountRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.StockCountMaster>, IEnumerable<StockCountMasterDto>>(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<StockCountMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.StockCountRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<StockCountMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<StockCountMasterDto>>
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

        public async Task<StockCountMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.StockCountMaster, StockCountMasterDto>(await _unitOfWork.StockCountRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateStockCount(StockCountMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Scmno))
                throw new ArgumentException("Scmno cannot be null or empty.", nameof(dto.Scmno));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedStockTransfer = _mapper.Map<Domain.Entities.StockCountMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.StockCountMaster.Create(mappedStockTransfer, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.StockCountDetail ?? Enumerable.Empty<StockCountDetailDto>())
                {
                    if (detailDto.CountedQty <= 0) throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var itemInv = await _unitOfWork.ItemInventoryRepository
                            .GetByItemdetailCodeAndWarehouseCodeAsync(detailDto.ItemDetailCode, master.WarehouseCode);

                    if (itemInv == null)
                        throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} has no inventory record.");

                    int systemQty = itemInv.QtyOnHand;
                    int countedQty = detailDto.CountedQty ?? 0;

                    int variance = countedQty - systemQty;

                    // No variance → no transaction
                    if (variance == 0)
                        continue;

                    // Determine type
                    string movement = variance > 0 ? "IN" : "OUT";
                    int quantity = Math.Abs(variance);

                    await CreateInventoryTransactionAsync(master, detailDto, movement, createdBy, quantity);

                    // --- Update ItemInventory (QtyOnHand) ---
                    if (variance > 0)
                        itemInv.QtyOnHand += variance;      // Add missing qty
                    else
                        itemInv.QtyOnHand -= Math.Abs(variance); // Remove excess qty

                    await _unitOfWork.ItemInventoryRepository.UpdateAsync(itemInv);

                    var newDetail = new Domain.Entities.StockCountDetail
                    {
                        Scmno = dto.Scmno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        CountedQty = detailDto.CountedQty,
                        RecStatus = "A"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.StockCountRepository.AddAsync(master);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateStockCount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateStockCount(StockCountMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Scmno))
                throw new ArgumentException("Scmno cannot be null or empty.", nameof(dto.Scmno));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedStockTransfer = _mapper.Map<Domain.Entities.StockCountMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.StockCountMaster.Update(mappedStockTransfer, editedBy)
                    ?? throw new InvalidOperationException("Failed to Update StockCount Master domain entity.");

                foreach (var detailDto in dto.StockCountDetail ?? Enumerable.Empty<StockCountDetailDto>())
                {
                    if (detailDto.CountedQty <= 0) throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailCode}.");

                    var itemInv = await _unitOfWork.ItemInventoryRepository
                            .GetByItemdetailCodeAndWarehouseCodeAsync(detailDto.ItemDetailCode, master.WarehouseCode);

                    if (itemInv == null)
                        throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} has no inventory record.");

                    int systemQty = itemInv.QtyOnHand;
                    int countedQty = detailDto.CountedQty ?? 0;

                    int variance = countedQty - systemQty;

                    // No variance → no transaction
                    if (variance == 0)
                        continue;

                    // Determine type
                    string movement = variance > 0 ? "IN" : "OUT";
                    int quantity = Math.Abs(variance);

                    await CreateInventoryTransactionAsync(master, detailDto, movement, editedBy, quantity);

                    // --- Update ItemInventory (QtyOnHand) ---
                    if (variance > 0)
                        itemInv.QtyOnHand += variance;      // Add missing qty
                    else
                        itemInv.QtyOnHand -= Math.Abs(variance); // Remove excess qty

                    await _unitOfWork.ItemInventoryRepository.UpdateAsync(itemInv);
                }

                await _unitOfWork.StockCountRepository.AddAsync(master);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateStockCount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<StockCountMasterDto>> SearchStockCountMastersByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var stockCount = await _unitOfWork.StockCountRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Scmno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseCode.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<StockCountMasterDto>>(stockCount);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchStockCountMastersByKeywordAsync", ex);
                throw;
            }
        }

        private async Task CreateInventoryTransactionAsync(Domain.Entities.StockCountMaster master, StockCountDetailDto detailDto, string movementType, string createdBy, int qty)
        {
            var invTrans = new Domain.Entities.InventoryTransaction
            {
                TransactionDate = DateTime.Now,
                CompanyId = master.Branch.CompanyId,
                BranchId = master.BranchId,
                WarehouseId = master.WarehouseCodeNavigation.id,
                ItemDetailNo = detailDto.ItemDetailCode,
                RefModule = "Stock Count",
                RefNo = master.Scmno,
                RefId = master.id,
                Quantity = (int)qty,
                UnitCost = 0,
                MovementType = movementType,
                Remarks = "Stock Count Adjustment",
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
