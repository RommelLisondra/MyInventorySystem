using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SystemSettingService : ISystemSettingsService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SystemSettingService));

        public SystemSettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SystemSettingDto>> GetAll()
        {
            try
            {
                var systemSettings = await _unitOfWork.SystemSettingsRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SystemSetting>, IEnumerable<SystemSettingDto>>(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SystemSettingDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SystemSettingsRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SystemSettingDto>>(result.Data);

                return new PagedResponse<IEnumerable<SystemSettingDto>>
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

        public async Task<SystemSettingDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SystemSetting, SystemSettingDto>(await _unitOfWork.SystemSettingsRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSystemSetting(SystemSettingDto systemSettingDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var systemSettings = Domain.Entities.SystemSetting.Create(
                    _mapper.Map<SystemSettingDto, Domain.Entities.SystemSetting>(systemSettingDto), createdBy);

                await _unitOfWork.SystemSettingsRepository.AddAsync(systemSettings);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSystemSetting", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateSystemSetting(SystemSettingDto systemSettingDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var systemSettings = Domain.Entities.SystemSetting.Update(
                    _mapper.Map<SystemSettingDto, Domain.Entities.SystemSetting>(systemSettingDto), editedBy);

                await _unitOfWork.SystemSettingsRepository.UpdateAsync(systemSettings);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateSystemSetting", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<SystemSettingDto>> SearchSystemSettingsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var systemSettings = await _unitOfWork.SystemSettingsRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.SettingKey ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.SettingValue ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SystemSettingDto>>(systemSettings);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSystemSettingsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
