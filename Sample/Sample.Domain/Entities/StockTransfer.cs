using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class StockTransfer : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual long TransferId { get; set; }
        public virtual string TransferNo { get; set; } = null!;
        public virtual DateTime TransferDate { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int FromWarehouseId { get; set; }
        public virtual int ToWarehouseId { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string CreatedBy { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; }
        public virtual string? RecStatus { get; set; }
        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; } = null!;

        public virtual ICollection<StockTransferDetail>? StockTransferDetail { get; set; }

        public static StockTransfer Create(StockTransfer stock, string createdBy)
        {
            //Place your Business logic here
            stock.Created_by = createdBy;
            stock.Date_created = DateTime.Now;
            return stock;
        }

        public static StockTransfer Update(StockTransfer stock, string editedBy)
        {
            //Place your Business logic here
            stock.Edited_by = editedBy;
            stock.Date_edited = DateTime.Now;
            return stock;
        }
        public void AddDetail(StockTransferDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            StockTransferDetail ??= new List<StockTransferDetail>();

            StockTransferDetail.Add(detail);
        }
    }
}
