using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISystemSettingsService : IDisposable
    {
        Task<IEnumerable<SystemSettingDto>> GetAll();
        Task<SystemSettingDto> GetById(int id);
        Task CreateSystemSetting(SystemSettingDto SystemSettingDto, string createdBy);
        Task UpdateSystemSetting(SystemSettingDto SystemSettingDto, string editedBy);
        Task<IEnumerable<SystemSettingDto>> SearchSystemSettingsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SystemSettingDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
