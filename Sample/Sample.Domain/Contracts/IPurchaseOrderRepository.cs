using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrderMaster>
    {
        Task<PurchaseOrderDetail?> GetByPODNoAndItemAsync(string poNo, string itemDetailCode);
        Task UpdatePurchaseOrderQtyReceiveAsync(PurchaseOrderDetail? orderDetail);
        Task<List<PurchaseOrderDetail>> GetPODetailsByPoNoAsync(string poNo);
        Task UpdatePurchaseOrderStatusAsync(string poNo, string status, string rrStatus);
    }
}
