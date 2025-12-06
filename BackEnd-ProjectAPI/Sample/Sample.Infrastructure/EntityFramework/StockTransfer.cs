using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class StockTransfer
    {
        public StockTransfer()
        {
            StockTransferDetail = new HashSet<StockTransferDetail>();
        }

        public int Id { get; set; }
        public string TransferNo { get; set; } = null!;
        public DateTime TransferDate { get; set; }
        public int CompanyId { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual ICollection<StockTransferDetail> StockTransferDetail { get; set; }
    }
}
