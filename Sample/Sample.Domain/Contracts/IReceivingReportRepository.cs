using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface IReceivingReportRepository : IRepository<ReceivingReportMaster>
    {
        Task<ReceivingReportDetail?> GetByRRDNoAndItemAsync(string rrNo, string itemDetailCode);
        Task UpdateReceivingQtyReceiveAsync(ReceivingReportDetail? receivingDetail);
        Task<List<ReceivingReportDetail>> GetRRDetailsByRrNoAsync(string rrNo);
        Task UpdateReceivingStatusAsync(string rrNo, string status);
    }
}
