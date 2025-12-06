using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISystemLogsService : IDisposable
    {
        Task<IEnumerable<SystemLogDto>> GetAll();
        Task<SystemLogDto> GetById(int id);
        Task CreateSystemLog(SystemLogDto SystemLogDto, string createdBy);
        Task UpdateSystemLog(SystemLogDto SystemLogDto, string editedBy);
        Task<IEnumerable<SystemLogDto>> SearchSystemLogsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SystemLogDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
