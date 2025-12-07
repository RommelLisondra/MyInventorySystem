using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class StockCountMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Scmno { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
        public virtual string WarehouseCode { get; set; } = null!;
        public virtual string? PreparedBy { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual Warehouse WarehouseCodeNavigation { get; set; } = null!;
        public virtual ICollection<StockCountDetail>? StockCountDetail { get; set; }

        public static StockCountMaster Create(StockCountMaster stockCount, string createdBy)
        {
            //Place your Business logic here
            stockCount.Created_by = createdBy;
            stockCount.Date_created = DateTime.Now;
            return stockCount;
        }

        public static StockCountMaster Update(StockCountMaster stockCount, string editedBy)
        {
            //Place your Business logic here
            stockCount.Edited_by = editedBy;
            stockCount.Date_edited = DateTime.Now;
            return stockCount;
        }

        public void AddDetail(StockCountDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            StockCountDetail ??= new List<StockCountDetail>();

            StockCountDetail.Add(detail);
        }
    }
}
