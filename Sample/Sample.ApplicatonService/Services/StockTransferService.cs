using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class StockTransferService : IStockTransferService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(StockTransferService));

        public StockTransferService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockTransferDto>> GetAll()
        {
            try
            {
                var stocks = await _unitOfWork.StockTransferRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.StockTransfer>, IEnumerable<StockTransferDto>>(stocks);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<StockTransferDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.StockTransferRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<StockTransferDto>>(result.Data);

                return new PagedResponse<IEnumerable<StockTransferDto>>
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

        public async Task<StockTransferDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.StockTransfer, StockTransferDto>(await _unitOfWork.StockTransferRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateStockTransfer(StockTransferDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.TransferNo))
                throw new ArgumentException("Transfer No cannot be null or empty.", nameof(dto.TransferNo));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedStockTransfer = _mapper.Map<Domain.Entities.StockTransfer>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.StockTransfer.Create(mappedStockTransfer, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.StockTransferDetail ?? Enumerable.Empty<StockTransferDetailDto>())
                {
                    if (detailDto.Quantity <= 0) throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailNo}.");

                    await DecreaseQty(dto, detailDto);

                    await IncreaseQty(dto, detailDto);

                    await CreateInventoryTransactionAsync(master, detailDto, "IN", createdBy, dto.FromWarehouseId);

                    await CreateInventoryTransactionAsync(master, detailDto, "OUT", createdBy, dto.ToWarehouseId);

                    var newDetail = new Domain.Entities.StockTransferDetail
                    {
                        TransferId = dto.id,
                        ItemDetailNo = detailDto.ItemDetailNo,
                        Quantity = detailDto.Quantity,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "A"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.StockTransferRepository.AddAsync(master);

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

        public async Task UpdateStockTransfer(StockTransferDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.TransferNo))
                throw new ArgumentException("Transfer No cannot be null or empty.", nameof(dto.TransferNo));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedStockTransfer = _mapper.Map<Domain.Entities.StockTransfer>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Receiving Report Master is null.");

                var master = Domain.Entities.StockTransfer.Create(mappedStockTransfer, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.StockTransferDetail ?? Enumerable.Empty<StockTransferDetailDto>())
                {
                    if (detailDto.Quantity <= 0) throw new InvalidOperationException($"Invalid Quantity for item {detailDto.ItemDetailNo}.");

                    await DecreaseQty(dto, detailDto);

                    await IncreaseQty(dto, detailDto);

                    await CreateInventoryTransactionAsync(master, detailDto, "IN", editedBy, dto.FromWarehouseId);

                    await CreateInventoryTransactionAsync(master, detailDto, "OUT", editedBy, dto.ToWarehouseId);
                }

                await _unitOfWork.StockTransferRepository.AddAsync(master);

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

        public async Task<IEnumerable<StockTransferDto>> SearchStockTransfersByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var stocks = await _unitOfWork.StockTransferRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.TransferId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.TransferNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.CompanyId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.FromWarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.ToWarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<StockTransferDto>>(stocks);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchStockTransfersByKeywordAsync", ex);
                throw;
            }
        }

        private async Task CreateInventoryTransactionAsync(Domain.Entities.StockTransfer master, StockTransferDetailDto detailDto, string movementType, string createdBy, int warehouseId)
        {
            var invTrans = new Domain.Entities.InventoryTransaction
            {
                TransactionDate = DateTime.Now,
                CompanyId = master.CompanyId,
                BranchId = master.BranchId,
                WarehouseId = warehouseId,
                ItemDetailNo = detailDto.ItemDetailNo,
                RefModule = "Purchase Returned",
                RefNo = master.TransferNo,
                RefId = master.id,
                Quantity = (int)detailDto.Quantity,
                UnitCost = (decimal)detailDto.Uprice,
                MovementType = movementType,
                Remarks = movementType == "IN" ? "Received from Warehouse" : "Transfer to Warehouse",
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                RecStatus = "O"
            };

            var inventorybalance = Domain.Entities.InventoryTransaction.Create(invTrans, createdBy)
                    ?? throw new InvalidOperationException("Failed to create inventoryTransaction domain entity.");

            await _unitOfWork.InventoryTransactionRepository.AddAsync(inventorybalance);
        }

        private async Task DecreaseQty(StockTransferDto dto, StockTransferDetailDto detailDto)
        {
            var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailNo)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailNo} not found.");

            var warehouse = await _unitOfWork.WarehouseRepository.GetByIdAsync(dto.FromWarehouseId)
                            ?? throw new InvalidOperationException($"Item {dto.FromWarehouseId} not found.");

            var inventory = item.ItemInventory
                                ?.FirstOrDefault(i => i.WarehouseCode == warehouse.WareHouseCode &&
                                                      i.LocationCode == warehouse.Location?.LocationCode)
                             ?? throw new InvalidOperationException("Inventory record not found.");

            inventory.DecreaseOnHand(detailDto.Quantity, "");

            await _unitOfWork.ItemDetailRepository.UpdateAsync(item);
        }
        private async Task IncreaseQty(StockTransferDto dto, StockTransferDetailDto detailDto)
        {
            var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailNo)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailNo} not found.");

            var warehouse = await _unitOfWork.WarehouseRepository.GetByIdAsync(dto.ToWarehouseId)
                            ?? throw new InvalidOperationException($"Item {dto.ToWarehouseId} not found.");

            var inventory = item.ItemInventory
                                ?.FirstOrDefault(i => i.WarehouseCode == warehouse.WareHouseCode &&
                                                      i.LocationCode == warehouse.Location?.LocationCode)
                             ?? throw new InvalidOperationException("Inventory record not found.");

            inventory.IncreaseOnHand(detailDto.Quantity, "");

            await _unitOfWork.ItemDetailRepository.UpdateAsync(item);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
