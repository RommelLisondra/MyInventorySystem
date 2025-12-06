using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface ISalesOrderRepository : IRepository<SalesOrderMaster>
    {
        Task<SalesOrderDetail?> GetBySODNoAndItemAsync(string soNo, string itemDetailCode);
        Task<List<SalesOrderDetail>> GetSODetailsBySoNoAsync(string soNo);
        Task UpdateSalesOrderStatusAsync(string soNo, string status);
        Task UpdateSalesOrderQtyInvoiceAsync(SalesOrderDetail? orderDetail);
    }
}
