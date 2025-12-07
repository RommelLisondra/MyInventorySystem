using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISalesInvoiceService : IDisposable
    {
        Task<IEnumerable<SalesInvoiceMasterDto>> GetAll();
        Task<SalesInvoiceMasterDto> GetById(int id);
        Task CreateSalesInvoiceAsync(SalesInvoiceMasterDto dto, string createdBy);
        Task UpdateSalesinvoiceAsync(SalesInvoiceMasterDto SalesInvoiceMasterDto, string editedBy);
        Task<IEnumerable<SalesInvoiceMasterDto>> GetSalesinvoiceByCustNoAsync(string custNo);
        Task<SalesInvoiceMasterDto?> GetSalesinvoiceByIdAsync(int id);
        Task<IEnumerable<SalesInvoiceMasterDto>> SearchSalesInvoiceByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SalesInvoiceMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
