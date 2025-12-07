using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class StockCountMaster
    {
        public StockCountMaster()
        {
            StockCountDetail = new HashSet<StockCountDetail>();
        }

        public int Id { get; set; }
        public string Scmno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string? PreparedBy { get; set; }
        public string? RecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual Warehouse WarehouseCodeNavigation { get; set; } = null!;
        public virtual ICollection<StockCountDetail> StockCountDetail { get; set; }
    }
}
