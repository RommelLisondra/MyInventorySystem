using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;


namespace Sample.ApplicationService.Services
{
    public class AuditTrailService : IAuditTrailService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(AuditTrailService));

        public AuditTrailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditTrailDto>> GetAll()
        {
            try
            {
                var auditTrailList = await _unitOfWork.AuditTrailRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.AuditTrail>, IEnumerable<AuditTrailDto>>(auditTrailList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<AuditTrailDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.AuditTrailRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<AuditTrailDto>>(result.Data);

                return new PagedResponse<IEnumerable<AuditTrailDto>>
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

        public async Task<AuditTrailDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.AuditTrail, AuditTrailDto>(await _unitOfWork.AuditTrailRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateAuditTrail(AuditTrailDto auditTrailDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var auditTrailEntity = Domain.Entities.AuditTrail.Create(
                    _mapper.Map<AuditTrailDto, Domain.Entities.AuditTrail>(auditTrailDto), createdBy);

                await _unitOfWork.AuditTrailRepository.AddAsync(auditTrailEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateAuditTrail", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateAuditTrail(AuditTrailDto auditTrailDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var auditTrailEntity = Domain.Entities.AuditTrail.Update(
                    _mapper.Map<AuditTrailDto, Domain.Entities.AuditTrail>(auditTrailDto), editedBy);

                await _unitOfWork.AuditTrailRepository.UpdateAsync(auditTrailEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateAuditTrail", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<AuditTrailDto>> SearchAuditTrailsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var auditTrails = await _unitOfWork.AuditTrailRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.TableName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.PrimaryKey ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Action ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ChangedBy ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.OldValue ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.NewValue ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<AuditTrailDto>>(auditTrails);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchAuditTrailsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
