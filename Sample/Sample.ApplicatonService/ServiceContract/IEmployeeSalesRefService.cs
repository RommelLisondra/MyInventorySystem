using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeSalesRefService : IDisposable
    {
        Task<IEnumerable<EmployeeSalesRefDto>> GetAll();
        Task<EmployeeSalesRefDto> GetById(int id);
        Task CreateEmployeeSalesRef(EmployeeSalesRefDto EmployeeSalesRefDto, string createdBy);
        Task UpdateEmployeeSalesRef(EmployeeSalesRefDto EmployeeSalesRefDto, string editedBy);
        Task<IEnumerable<EmployeeSalesRefDto>> SearchEmployeeSalesRefsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<EmployeeSalesRefDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
