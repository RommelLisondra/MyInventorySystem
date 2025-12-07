using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SystemLogsService : ISystemLogsService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SystemLogsService));

        public SystemLogsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SystemLogDto>> GetAll()
        {
            try
            {
                var systemLogs = await _unitOfWork.SystemLogsRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SystemLog>, IEnumerable<SystemLogDto>>(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SystemLogDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SystemLogsRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SystemLogDto>>(result.Data);

                return new PagedResponse<IEnumerable<SystemLogDto>>
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

        public async Task<SystemLogDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SystemLog, SystemLogDto>(await _unitOfWork.SystemLogsRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSystemLog(SystemLogDto systemLogDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var systemLogs = Domain.Entities.SystemLog.Create(
                    _mapper.Map<SystemLogDto, Domain.Entities.SystemLog>(systemLogDto), createdBy);

                await _unitOfWork.SystemLogsRepository.AddAsync(systemLogs);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSystemLog", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateSystemLog(SystemLogDto systemLogDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var systemLogs = Domain.Entities.SystemLog.Update(
                    _mapper.Map<SystemLogDto, Domain.Entities.SystemLog>(systemLogDto), editedBy);

                await _unitOfWork.SystemLogsRepository.UpdateAsync(systemLogs);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateSystemLog", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<SystemLogDto>> SearchSystemLogsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var systemLogs = await _unitOfWork.SystemLogsRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.LogType ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Message ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.StackTrace ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.LoggedBy ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SystemLogDto>>(systemLogs);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSystemLogsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
