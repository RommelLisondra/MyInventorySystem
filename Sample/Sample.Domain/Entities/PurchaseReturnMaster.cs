using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class PurchaseReturnMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string PretMno { get; set; } = null!;
        public virtual string Rrno { get; set; } = null!;
        public virtual string? RefNo { get; set; }
        public virtual string SupplierNo { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
        public virtual TimeSpan? Time { get; set; }
        public virtual string? Terms { get; set; }
        public virtual string? TypesOfPay { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual decimal? DisPercent { get; set; }
        public virtual decimal? DisTotal { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual string PrepBy { get; set; } = null!;
        public virtual string ApprBy { get; set; } = null!;
        public virtual string? RecStatus { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual ReceivingReportMaster? RrnoNavigation { get; set; }
        public virtual Supplier? SupNoNavigation { get; set; }
        public virtual ICollection<PurchaseReturnDetail>? PurchaseReturnDetailFile { get; set; }

        public static PurchaseReturnMaster Create(PurchaseReturnMaster purchaseReturn, string createdBy)
        {
            //Place your Business logic here
            purchaseReturn.Created_by = createdBy;
            purchaseReturn.Date_created = DateTime.Now;
            return purchaseReturn;
        }

        public static PurchaseReturnMaster Update(PurchaseReturnMaster purchaseReturn, string editedBy)
        {
            //Place your Business logic here
            purchaseReturn.Edited_by = editedBy;
            purchaseReturn.Date_edited = DateTime.Now;
            return purchaseReturn;
        }

        public void AddDetail(PurchaseReturnDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            PurchaseReturnDetailFile ??= new List<PurchaseReturnDetail>();

            PurchaseReturnDetailFile.Add(detail);
        }
    }
}
