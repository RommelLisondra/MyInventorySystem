using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class InventoryAdjustmentService : IInventoryAdjustmentService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(InventoryAdjustmentService));

        public InventoryAdjustmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryAdjustmentDto>> GetAll()
        {
            try
            {
                var inventoryAdjustment = await _unitOfWork.InventoryAdjustmentRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.InventoryAdjustment>, IEnumerable<InventoryAdjustmentDto>>(inventoryAdjustment);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<InventoryAdjustmentDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.InventoryAdjustmentRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<InventoryAdjustmentDto>>(result.Data);

                return new PagedResponse<IEnumerable<InventoryAdjustmentDto>>
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

        public async Task<InventoryAdjustmentDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.InventoryAdjustment, InventoryAdjustmentDto>(await _unitOfWork.InventoryAdjustmentRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateInventoryAdjustment(InventoryAdjustmentDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.AdjustmentNo))
                throw new ArgumentException("Adjustment No cannot be null or empty.", nameof(dto.AdjustmentNo));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInventoryAdjustment = _mapper.Map<Domain.Entities.InventoryAdjustment>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.InventoryAdjustment.Create(mappedInventoryAdjustment, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.InventoryAdjustmentDetail ?? Enumerable.Empty<InventoryAdjustmentDetailDto>())
                {
                    if (detailDto.Quantity <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailNo}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailNo)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailNo} not found.");

                    var warehouse = await _unitOfWork.WarehouseRepository.GetByIdAsync(dto.WarehouseId)
                                    ?? throw new InvalidOperationException($"Item {dto.WarehouseId} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == warehouse.WareHouseCode &&
                                                              i.LocationCode == warehouse.Location?.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    if (detailDto.MovementType == "IN")
                    {
                        inventory.IncreaseOnHand(detailDto.Quantity, "");
                    }     
                    else
                    { 
                        inventory.DecreaseOnHand(detailDto.Quantity, ""); 
                    }

                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    await CreateInventoryTransactionAsync(master, detailDto, createdBy);

                    var newDetail = new Domain.Entities.InventoryAdjustmentDetail
                    {
                        DetailId = dto.id,
                        AdjustmentId = detailDto.AdjustmentId,
                        ItemDetailNo = detailDto.ItemDetailNo,
                        Quantity = detailDto.Quantity,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        MovementType = detailDto.MovementType
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.InventoryAdjustmentRepository.AddAsync(master);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateInventoryAdjustment", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateInventoryAdjustment(InventoryAdjustmentDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.AdjustmentNo))
                throw new ArgumentException("Adjustment No cannot be null or empty.", nameof(dto.AdjustmentNo));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInventoryAdjustment = _mapper.Map<Domain.Entities.InventoryAdjustment>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.InventoryAdjustment.Update(mappedInventoryAdjustment, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.InventoryAdjustmentDetail ?? Enumerable.Empty<InventoryAdjustmentDetailDto>())
                {
                    if (detailDto.Quantity <= 0)
                        throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailNo}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailNo)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailNo} not found.");

                    var warehouse = await _unitOfWork.WarehouseRepository.GetByIdAsync(dto.WarehouseId)
                                    ?? throw new InvalidOperationException($"Item {dto.WarehouseId} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == warehouse.WareHouseCode &&
                                                              i.LocationCode == warehouse.Location?.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    if (detailDto.MovementType == "IN")
                    {
                        inventory.IncreaseOnHand(detailDto.Quantity, "");
                    }
                    else
                    {
                        inventory.DecreaseOnHand(detailDto.Quantity, "");
                    }

                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    await CreateInventoryTransactionAsync(master, detailDto, editedBy);
                }

                await _unitOfWork.InventoryAdjustmentRepository.AddAsync(master);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateInventoryAdjustment", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<InventoryAdjustmentDto>> SearchInventoryAdjustmentsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var inventoryAdjustment = await _unitOfWork.InventoryAdjustmentRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.AdjustmentNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.CompanyId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<InventoryAdjustmentDto>>(inventoryAdjustment);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchInventoryAdjustmentsByKeywordAsync", ex);
                throw;
            }
        }

        public async Task CreateInventoryTransactionAsync(Domain.Entities.InventoryAdjustment master, InventoryAdjustmentDetailDto detailDto, string createdBy)
        {
            var invTrans = new Domain.Entities.InventoryTransaction
            {
                TransactionDate = DateTime.Now,
                CompanyId = master.CompanyId,
                BranchId = master.BranchId,
                WarehouseId = master.WarehouseId,
                ItemDetailNo = detailDto.ItemDetailNo,
                RefModule = "Purchase Returned",
                RefNo = master.AdjustmentNo,
                RefId = master.id,
                Quantity = (int)detailDto.Quantity,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = detailDto.MovementType,
                Remarks = detailDto.MovementType == "IN" ? "Stock Adjustment (Over Count)" : "Stock Adjustment (Short Count)",
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);

            await UpdateInventoryBalanceAsync(detailDto.ItemDetailNo, master.WarehouseId, detailDto.Quantity, detailDto.MovementType);
        }

        public async Task UpdateInventoryBalanceAsync(string itemDetailNo, int warehouseId, int qty, string movementType)
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
                if (movementType == "IN")
                    inventoryBalance.QuantityOnHand += qty;
                else
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
