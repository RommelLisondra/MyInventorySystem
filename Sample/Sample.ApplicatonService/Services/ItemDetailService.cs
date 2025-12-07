using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Infrastructure;
using Sample.Common.Logger;

namespace Sample.ApplicationService.Services
{
    public class ItemDetailService : IItemDetailService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemDetailService));

        public ItemDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDetailDto>> GetAll()
        {
            try
            {
                var item = await _unitOfWork.ItemDetailRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemDetail>, IEnumerable<ItemDetailDto>>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemDetailDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemDetailRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemDetailDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemDetailDto>>
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

        public async Task<ItemDetailDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemDetail, ItemDetailDto>(await _unitOfWork.ItemDetailRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItem(ItemDetailDto itemDetailDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var item = Domain.Entities.ItemDetail.Create(
                    _mapper.Map<ItemDetailDto, Domain.Entities.ItemDetail>(itemDetailDto), createdBy);

                await _unitOfWork.ItemDetailRepository.AddAsync(item);
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

        public async Task UpdateItem(ItemDetailDto itemDetailDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var item = Domain.Entities.ItemDetail.Update(
                    _mapper.Map<ItemDetailDto, Domain.Entities.ItemDetail>(itemDetailDto), editedBy);

                await _unitOfWork.ItemDetailRepository.UpdateAsync(item);
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

        public async Task<IEnumerable<ItemDetailDto>> GetItemsByCustNoAsync(string itemNo)
        {
            try
            {
                var items = await _unitOfWork.ItemDetailRepository.FindAsync(e => e.ItemId == itemNo);
                return _mapper.Map<IEnumerable<ItemDetailDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<ItemDetailDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.ItemDetailRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<ItemDetailDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemDetailDto>> SearchItemsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var item = await _unitOfWork.ItemDetailRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    (e.ItemMaster != null && e.ItemMaster.Brand != null && EF.Functions.Like((e.ItemMaster.Brand.BrandName ?? string.Empty).ToLower(), $"%{keyword}%")) ||
                    EF.Functions.Like((e.ItemDetailCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.PartNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    (e.ItemMaster != null && EF.Functions.Like((e.ItemMaster.Model ?? string.Empty).ToLower(), $"%{keyword}%")) ||
                    EF.Functions.Like((e.SerialNo ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemDetailDto>>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
