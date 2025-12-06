using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ApprovalHistoryService : IApprovalHistoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ApprovalHistoryService));

        public ApprovalHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApprovalHistoryDto>> GetAll()
        {
            try
            {
                var approvalHistoryList = await _unitOfWork.ApprovalHistoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ApprovalHistory>, IEnumerable<ApprovalHistoryDto>>(approvalHistoryList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ApprovalHistoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ApprovalHistoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ApprovalHistoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<ApprovalHistoryDto>>
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

        public async Task<ApprovalHistoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ApprovalHistory, ApprovalHistoryDto>(await _unitOfWork.ApprovalHistoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateApprovalHistory(ApprovalHistoryDto approvalHistoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var approvalHistoryEntity = Domain.Entities.ApprovalHistory.Create(
                    _mapper.Map<ApprovalHistoryDto, Domain.Entities.ApprovalHistory>(approvalHistoryDto), createdBy);

                await _unitOfWork.ApprovalHistoryRepository.AddAsync(approvalHistoryEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateApprovalHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateApprovalHistory(ApprovalHistoryDto approvalHistoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var approvalHistoryEntity = Domain.Entities.ApprovalHistory.Update(
                    _mapper.Map<ApprovalHistoryDto, Domain.Entities.ApprovalHistory>(approvalHistoryDto), editedBy);

                await _unitOfWork.ApprovalHistoryRepository.UpdateAsync(approvalHistoryEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateApprovalHistory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ApprovalHistoryDto>> SearchApprovalHistorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var approvalHistory = await _unitOfWork.ApprovalHistoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    //EF.Functions.Like((e.ApprovalId).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ModuleName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.RefNo.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ApprovedBy ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Remarks ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ApprovalHistoryDto>>(approvalHistory);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchApprovalHistorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
