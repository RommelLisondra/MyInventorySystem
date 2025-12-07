using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class PurchaseOrderMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Pomno { get; set; } = null!;
        public virtual string Prno { get; set; } = null!;
        public virtual string SupplierNo { get; set; } = null!;
        public virtual string? TypesofPay { get; set; }
        public virtual string? Terms { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual DateTime? Time { get; set; }
        public virtual DateTime? DateNeeded { get; set; }
        public virtual string? ReferenceNo { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual decimal? DisPercent { get; set; }
        public virtual decimal? DisTotal { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual string PrepBy { get; set; } = null!;
        public virtual string ApprBy { get; set; } = null!;
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual string? RrrecStatus { get; set; }
        public virtual string? PreturnRecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual PurchaseRequisitionMaster? PrnoNavigation { get; set; } = null!;
        public virtual Supplier? SupNoNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseOrderDetail>? PurchaseOrderDetailFile { get; set; }

        public static PurchaseOrderMaster Create(PurchaseOrderMaster purchaseOrder, string createdBy)
        {
            //Place your Business logic here
            purchaseOrder.Created_by = createdBy;
            purchaseOrder.Date_created = DateTime.Now;
            return purchaseOrder;
        }

        public static PurchaseOrderMaster Update(PurchaseOrderMaster purchaseOrder, string editedBy)
        {
            //Place your Business logic here
            purchaseOrder.Edited_by = editedBy;
            purchaseOrder.Date_edited = DateTime.Now;
            return purchaseOrder;
        }

        public void AddDetail(PurchaseOrderDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            PurchaseOrderDetailFile ??= new List<PurchaseOrderDetail>();

            PurchaseOrderDetailFile.Add(detail);
        }
    }
}
