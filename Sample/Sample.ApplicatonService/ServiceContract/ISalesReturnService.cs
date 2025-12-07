using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISalesReturnService : IDisposable
    {
        Task<IEnumerable<SalesReturnMasterDto>> GetAll();
        Task<SalesReturnMasterDto> GetById(int id);
        Task CreateSalesReturnAsync(SalesReturnMasterDto dto, string createdBy);
        Task UpdateSalesReturnAsync(SalesReturnMasterDto dto, string editedBy);
        Task<IEnumerable<SalesReturnMasterDto>> GetSalesinvoiceByCustNoAsync(string custNo);
        Task<SalesReturnMasterDto?> GetSalesinvoiceByIdAsync(int id);
        Task<IEnumerable<SalesReturnMasterDto>> SearchSalesReturnByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SalesReturnMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
