using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemImageService : IItemImageService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemImageService));

        public ItemImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemImageDto>> GetAll()
        {
            try
            {
                var item = await _unitOfWork.ItemImageRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemImage>, IEnumerable<ItemImageDto>>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemImageDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemImageRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemImageDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemImageDto>>
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

        public async Task<ItemImageDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemImage, ItemImageDto>(await _unitOfWork.ItemImageRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemImage(ItemImageDto itemImageDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemEntity = Domain.Entities.ItemImage.Create(
                    _mapper.Map<ItemImageDto, Domain.Entities.ItemImage>(itemImageDto), createdBy);

                await _unitOfWork.ItemImageRepository.AddAsync(itemEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItem", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemImage(ItemImageDto itemImageDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var itemEntity = Domain.Entities.ItemImage.Update(
                    _mapper.Map<ItemImageDto, Domain.Entities.ItemImage>(itemImageDto), editedBy);

                await _unitOfWork.ItemImageRepository.UpdateAsync(itemEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItem", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ItemImageDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.ItemImageRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<ItemImageDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemImageDto>> SearchItemsAsync(string? itemDetailCode)
        {
            try
            {
                // Normalize null values
                itemDetailCode ??= string.Empty;

                // Perform case-insensitive search
                var Items = await _unitOfWork.ItemImageRepository.FindAsync(e =>
                    (string.IsNullOrEmpty(itemDetailCode) || EF.Functions.Like(e.ItemDetailCode ?? string.Empty, $"%{itemDetailCode}%"))
                );

                return _mapper.Map<IEnumerable<ItemImageDto>>(Items);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemsAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
