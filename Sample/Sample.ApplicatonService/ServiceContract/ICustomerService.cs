using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ICustomerService : IDisposable
    {
        Task<IEnumerable<CustomerDto>> GetAll();
        Task<CustomerDto> GetById(int id);
        Task CreateCustomer(CustomerDto customerDto, string createdBy);
        Task UpdateCustomer(CustomerDto CustomerDto, string editedBy);
        Task<IEnumerable<CustomerDto>> GetCustomersByCustNoAsync(string custNo);
        Task<CustomerDto?> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> SearchCustomersByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<CustomerDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
