using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemSupplierService : IItemSupplierService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemSupplierService));

        public ItemSupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemSupplierDto>> GetAll()
        {
            try
            {
                var items = await _unitOfWork.ItemSupplierRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemSupplier>, IEnumerable<ItemSupplierDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemSupplierDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemSupplierRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemSupplierDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemSupplierDto>>
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

        public async Task<ItemSupplierDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemSupplier, ItemSupplierDto>(await _unitOfWork.ItemSupplierRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemSupplier(ItemSupplierDto itemSupplierDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemSupplier.Create(
                    _mapper.Map<ItemSupplierDto, Domain.Entities.ItemSupplier>(itemSupplierDto), createdBy);

                await _unitOfWork.ItemSupplierRepository.AddAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemSupplier", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemSupplier(ItemSupplierDto itemSupplierDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemSupplier.Update(
                    _mapper.Map<ItemSupplierDto, Domain.Entities.ItemSupplier>(itemSupplierDto), editedBy);

                await _unitOfWork.ItemSupplierRepository.UpdateAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemSupplier", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemSupplierDto>> GetItemsByitemNoAsync(string itemNo)
        {
            try
            {
                var items = await _unitOfWork.ItemSupplierRepository.FindAsync(e => e.ItemDetailCode == itemNo);
                return _mapper.Map<IEnumerable<ItemSupplierDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByitemNoAsync", ex);
                throw;
            }
        }

        public async Task<ItemSupplierDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.ItemSupplierRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<ItemSupplierDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemSupplierDto>> SearchItemSupplierItemCodeAsync(string? itemDetailCode)
        {
            try
            {
                itemDetailCode ??= string.Empty;

                var items = await _unitOfWork.ItemSupplierRepository.FindAsync(e =>
                    (string.IsNullOrEmpty(itemDetailCode) || EF.Functions.Like(e.ItemDetailCode ?? string.Empty, $"%{itemDetailCode}%"))
                );

                return _mapper.Map<IEnumerable<ItemSupplierDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemSupplierItemCodeAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemSupplierDto>> SearchItemSupplierKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var items = await _unitOfWork.ItemSupplierRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.SupplierNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ItemDetailCode ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemSupplierDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemSupplierKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
