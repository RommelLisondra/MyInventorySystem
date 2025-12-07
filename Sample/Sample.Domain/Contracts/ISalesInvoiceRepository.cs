using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface ISalesInvoiceRepository : IRepository<SalesInvoiceMaster>
    {
        Task<SalesInvoiceDetail?> GetBySINoAndItemAsync(string siNo, string itemDetailCode);
        Task UpdateSalesInvoiceDetailStatusAsync(SalesInvoiceDetail invoiceDetail);
        Task<List<SalesInvoiceDetail>> GetSIDetailsBySiNoAsync(string siNo);
        Task UpdateSalesInvoiceStatusAsync(string siNo, string status);
    }
}
