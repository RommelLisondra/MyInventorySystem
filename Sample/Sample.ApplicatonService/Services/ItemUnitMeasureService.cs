using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemUnitMeasureService : IItemUnitMeasureService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemUnitMeasureService));

        public ItemUnitMeasureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemUnitMeasureDto>> GetAll()
        {
            try
            {
                var items = await _unitOfWork.ItemUnitMeasureRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemUnitMeasure>, IEnumerable<ItemUnitMeasureDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemUnitMeasureDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemUnitMeasureRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemUnitMeasureDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemUnitMeasureDto>>
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

        public async Task<ItemUnitMeasureDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemUnitMeasure, ItemUnitMeasureDto>(await _unitOfWork.ItemUnitMeasureRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemUnitMeasure(ItemUnitMeasureDto itemUnitMeasureDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemUnitMeasure.Create(
                    _mapper.Map<ItemUnitMeasureDto, Domain.Entities.ItemUnitMeasure>(itemUnitMeasureDto), createdBy);

                await _unitOfWork.ItemUnitMeasureRepository.AddAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemUnitMeasure", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemUnitMeasure(ItemUnitMeasureDto itemUnitMeasureDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemUnitMeasure.Update(
                    _mapper.Map<ItemUnitMeasureDto, Domain.Entities.ItemUnitMeasure>(itemUnitMeasureDto), editedBy);

                await _unitOfWork.ItemUnitMeasureRepository.UpdateAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemUnitMeasure", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemUnitMeasureDto>> GetItemUnitMeasureByunitCodeAsync(string unitCode)
        {
            try
            {
                var items = await _unitOfWork.ItemUnitMeasureRepository.FindAsync(e => e.UnitCode == unitCode);
                return _mapper.Map<IEnumerable<ItemUnitMeasureDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemUnitMeasureByunitCodeAsync", ex);
                throw;
            }
        }

        public async Task<ItemUnitMeasureDto?> GetItemUnitMeasureByIdAsync(int id)
        {
            try
            {
                var items = await _unitOfWork.ItemUnitMeasureRepository.FindAsync(e => e.id == id);
                var item = items.FirstOrDefault();
                return item == null ? null : _mapper.Map<ItemUnitMeasureDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemUnitMeasureByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ItemUnitMeasureDto>> SearchItemUnitMeasureAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var items = await _unitOfWork.ItemUnitMeasureRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ItemDetailCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.UnitCode ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ItemUnitMeasureDto>>(items);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemUnitMeasureAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
