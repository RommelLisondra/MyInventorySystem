using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeCheckerService : IDisposable
    { 
        Task<IEnumerable<EmployeeCheckerDto>> GetAll();
        Task<EmployeeCheckerDto> GetById(int id);
        Task CreateEmployeeChecker(EmployeeCheckerDto EmployeeCheckerDto, string createdBy);
        Task UpdateEmployeeChecker(EmployeeCheckerDto EmployeeCheckerDto, string editedBy);
        Task<IEnumerable<EmployeeCheckerDto>> SearchEmployeeCheckersByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<EmployeeCheckerDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
