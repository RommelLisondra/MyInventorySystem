using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IHolidayService : IDisposable
    {
        Task<IEnumerable<HolidayDto>> GetAll();
        Task<PagedResponse<IEnumerable<HolidayDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<HolidayDto> GetById(int id);
        Task CreateHoliday(HolidayDto HolidayDto, string createdBy);
        Task UpdateHoliday(HolidayDto HolidayDto, string editedBy);
        Task<IEnumerable<HolidayDto>> SearchHolidaysByKeywordAsync(string? keyword);
    }
}
