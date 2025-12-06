using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ICompanyService : IDisposable
    {
        Task<IEnumerable<CompanyDto>> GetAll();
        Task<PagedResponse<IEnumerable<CompanyDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<CompanyDto> GetById(int id);
        Task CreateCompany(CompanyDto companyDto, string createdBy);
        Task UpdateCompany(CompanyDto CompanyDto, string editedBy);
        Task<IEnumerable<CompanyDto>> SearchCompanysByKeywordAsync(string? keyword);
    }
}
