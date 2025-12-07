using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class CostingHistoryService : ICostingHistoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CostingHistoryService));

        public CostingHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CostingHistoryDto>> GetAll()
        {
            try
            {
                var costingHistoryList = await _unitOfWork.CostingHistoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.CostingHistory>, IEnumerable<CostingHistoryDto>>(costingHistoryList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<CostingHistoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.CostingHistoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<CostingHistoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<CostingHistoryDto>>
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

        public async Task<CostingHistoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.CostingHistory, CostingHistoryDto>(await _unitOfWork.CostingHistoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateCostingHistory(CostingHistoryDto costingHistoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var costingHistoryEntity = Domain.Entities.CostingHistory.Create(
                    _mapper.Map<CostingHistoryDto, Domain.Entities.CostingHistory>(costingHistoryDto), createdBy);

                await _unitOfWork.CostingHistoryRepository.AddAsync(costingHistoryEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateCostingHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateCostingHistory(CostingHistoryDto costingHistoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var costingHistoryEntity = Domain.Entities.CostingHistory.Update(
                    _mapper.Map<CostingHistoryDto, Domain.Entities.CostingHistory>(costingHistoryDto), editedBy);

                await _unitOfWork.CostingHistoryRepository.UpdateAsync(costingHistoryEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateCostingHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<CostingHistoryDto>> SearchCostingHistorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var costingHistory = await _unitOfWork.CostingHistoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.ItemId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.BranchId.ToString().ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<CostingHistoryDto>>(costingHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchCostingHistorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
