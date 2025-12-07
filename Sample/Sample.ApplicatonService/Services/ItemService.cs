using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemService : IItemService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemService));

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            try
            {
                var item = await _unitOfWork.ItemRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Item>, IEnumerable<ItemDto>>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemDto>>
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

        public async Task<ItemDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Item, ItemDto>(await _unitOfWork.ItemRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItem(ItemDto itemDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var item = Domain.Entities.Item.Create(
                    _mapper.Map<ItemDto, Domain.Entities.Item>(itemDto), createdBy);

                await _unitOfWork.ItemRepository.AddAsync(item);
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

        public async Task UpdateItem(ItemDto itemDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var item = Domain.Entities.Item.Update(
                    _mapper.Map<ItemDto, Domain.Entities.Item>(itemDto), editedBy);

                await _unitOfWork.ItemRepository.UpdateAsync(item);
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

        public async Task<IEnumerable<ItemDto>> GetItemsByitemNoAsync(string itemNo)
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.FindAsync(e => e.ItemCode == itemNo);
                return _mapper.Map<IEnumerable<ItemDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByitemNoAsync", ex);
                throw;
            }
        }

        public async Task<ItemDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.ItemRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<ItemDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> SearchItemsAsync(string? brandName)
        {
            try
            {
                brandName ??= string.Empty;
                var items = await _unitOfWork.ItemRepository.FindAsync(e =>
                    (string.IsNullOrEmpty(brandName) || EF.Functions.Like(e.ItemName ?? string.Empty, $"%{brandName}%"))
                );

                return _mapper.Map<IEnumerable<ItemDto>>(items);
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
