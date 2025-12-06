using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface IPurchaseRequisitionRepository : IRepository<PurchaseRequisitionMaster>
    {
        Task<PurchaseRequisitionDetail?> GetByPRDNoAndItemAsync(string prNo, string itemDetailCode);
        Task UpdatePurchaseRequisitionQtyOnOrderAsync(PurchaseRequisitionDetail? requisitionDetail);
        Task<PurchaseRequisitionMaster> GetByPrNoAsync(string prNo);
        Task<List<PurchaseRequisitionDetail>> GetPRDetailsByPrNoAsync(string prNo);
        Task UpdatePurchaseRequisitionStatusAsync(string prNo, string status);
    }
}
