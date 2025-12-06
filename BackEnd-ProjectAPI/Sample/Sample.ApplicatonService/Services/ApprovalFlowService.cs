using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;


namespace Sample.ApplicationService.Services
{
    public class ApprovalFlowService : IApprovalFlowService , IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ApprovalFlowService));

        public ApprovalFlowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApprovalFlowDto>> GetAll()
        {
            try
            {
                var approvalFlowList = await _unitOfWork.ApprovalFlowRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.ApprovalFlow>, IEnumerable<ApprovalFlowDto>>(approvalFlowList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<ApprovalFlowDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ApprovalFlowRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ApprovalFlowDto>>(result.Data);

                return new PagedResponse<IEnumerable<ApprovalFlowDto>>
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

        public async Task<ApprovalFlowDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.ApprovalFlow, ApprovalFlowDto>(await _unitOfWork.ApprovalFlowRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateApprovalFlow(ApprovalFlowDto ApprovalFlowDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var approvalFlowEntity = Domain.Entities.ApprovalFlow.Create(
                    _mapper.Map<ApprovalFlowDto, Domain.Entities.ApprovalFlow>(ApprovalFlowDto), createdBy);

                await _unitOfWork.ApprovalFlowRepository.AddAsync(approvalFlowEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateApprovalFlow", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateApprovalFlow(ApprovalFlowDto approvalFlowDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var approvalFlowEntity = Domain.Entities.ApprovalFlow.Update(
                    _mapper.Map<ApprovalFlowDto, Domain.Entities.ApprovalFlow>(approvalFlowDto), editedBy);

                await _unitOfWork.ApprovalFlowRepository.UpdateAsync(approvalFlowEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateApprovalFlow", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ApprovalFlowDto>> SearchApprovalFlowsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var approvalFlows = await _unitOfWork.ApprovalFlowRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.ApprovalId ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ModuleName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.Level.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.ApproverId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.IsFinalLevel.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ApprovalFlowDto>>(approvalFlows);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchApprovalFlowsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
