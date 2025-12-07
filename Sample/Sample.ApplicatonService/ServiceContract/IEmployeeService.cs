using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeService : IDisposable
    {
        Task<EmployeeDto> GetById(int id);
        Task<IEnumerable<EmployeeDto>> GetAll();
        Task CreateEmployee(EmployeeDto employeeDto, string createdBy);
        Task UpdateEmployee(EmployeeDto employeeDto, string editedBy);
        Task<IEnumerable<EmployeeDto>> SearchEmployeeByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<EmployeeDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
