using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Infrastructure;
using Sample.Common.Logger;

namespace Sample.ApplicationService.Services
{
    public class ItemInventoryService : IItemInventoryService, IDisposable
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemInventoryService));

        public ItemInventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemInventoryDto>> GetAll()
        {
            try
            {
                var itemInventory = await _unitOfWork.ItemInventoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemInventory>, IEnumerable<ItemInventoryDto>>(itemInventory);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemInventoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemInventoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemInventoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemInventoryDto>>
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

        public async Task<ItemInventoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemInventory, ItemInventoryDto>(await _unitOfWork.ItemInventoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemInventory(ItemInventoryDto itemInventoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemInventory = Domain.Entities.ItemInventory.Create(
                    _mapper.Map<ItemInventoryDto, Domain.Entities.ItemInventory>(itemInventoryDto), createdBy);

                await _unitOfWork.ItemInventoryRepository.AddAsync(itemInventory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemInventory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemInventory(ItemInventoryDto itemInventoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemInventory = Domain.Entities.ItemInventory.Update(
                    _mapper.Map<ItemInventoryDto, Domain.Entities.ItemInventory>(itemInventoryDto), editedBy);

                await _unitOfWork.ItemInventoryRepository.UpdateAsync(itemInventory);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemInventory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemInventoryDto>> SearchItemInventorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var itemInventory = await _unitOfWork.ItemInventoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ItemDetailCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseCode.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.LocationCode.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemInventoryDto>>(itemInventory);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemInventorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
