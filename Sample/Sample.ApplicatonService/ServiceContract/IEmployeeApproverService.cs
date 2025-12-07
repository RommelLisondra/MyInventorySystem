using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeApproverService : IDisposable
    {
        Task<IEnumerable<EmployeeApproverDto>> GetAll();
        Task<EmployeeApproverDto> GetById(int id);
        Task CreateEmployeeApprover(EmployeeApproverDto EmployeeApproverDto, string createdBy);
        Task UpdateEmployeeApprover(EmployeeApproverDto EmployeeApproverDto, string editedBy);
        Task<IEnumerable<EmployeeApproverDto>> SearchEmployeeApproversByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<EmployeeApproverDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
