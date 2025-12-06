using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ItemWarehouseMappingService : IItemWarehouseMappingService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ItemWarehouseMappingService));

        public ItemWarehouseMappingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemWarehouseMappingDto>> GetAll()
        {
            try
            {
                var itemWarehouseMappingList = await _unitOfWork.ItemWarehouseMappingRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ItemWarehouseMapping>, IEnumerable<ItemWarehouseMappingDto>>(itemWarehouseMappingList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ItemWarehouseMappingDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ItemWarehouseMappingRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ItemWarehouseMappingDto>>(result.Data);

                return new PagedResponse<IEnumerable<ItemWarehouseMappingDto>>
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

        public async Task<ItemWarehouseMappingDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ItemWarehouseMapping, ItemWarehouseMappingDto>(await _unitOfWork.ItemWarehouseMappingRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateItemWarehouseMapping(ItemWarehouseMappingDto itemWarehouseMappingDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemWarehouseMapping.Create(
                    _mapper.Map<ItemWarehouseMappingDto, Domain.Entities.ItemWarehouseMapping>(itemWarehouseMappingDto), createdBy);

                await _unitOfWork.ItemWarehouseMappingRepository.AddAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateItemWarehouseMapping", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateItemWarehouseMapping(ItemWarehouseMappingDto itemWarehouseMappingDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var items = Domain.Entities.ItemWarehouseMapping.Update(
                    _mapper.Map<ItemWarehouseMappingDto, Domain.Entities.ItemWarehouseMapping>(itemWarehouseMappingDto), editedBy);

                await _unitOfWork.ItemWarehouseMappingRepository.UpdateAsync(items);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateItemWarehouseMapping", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ItemWarehouseMappingDto>> SearchItemWarehouseMappingsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var itemWarehouseMappings = await _unitOfWork.ItemWarehouseMappingRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.ItemId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.WarehouseId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.BranchId.ToString().ToLower(), $"%{keyword}%") 
                );

                return _mapper.Map<IEnumerable<ItemWarehouseMappingDto>>(itemWarehouseMappings);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchItemWarehouseMappingsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
