using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeDeliveredService : IDisposable
    {
        Task<IEnumerable<EmployeeDeliveredDto>> GetAll();
        Task<EmployeeDeliveredDto> GetById(int id);
        Task CreateEmployeeDelivered(EmployeeDeliveredDto EmployeeDeliveredDto, string createdBy);
        Task UpdateEmployeeDelivered(EmployeeDeliveredDto EmployeeDeliveredDto, string editedBy);
        Task<IEnumerable<EmployeeDeliveredDto>> SearchEmployeeDeliveredsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<EmployeeDeliveredDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
