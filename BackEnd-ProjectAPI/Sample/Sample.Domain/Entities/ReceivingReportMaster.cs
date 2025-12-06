using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ReceivingReportMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Rrmno { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
        public virtual TimeSpan? Time { get; set; }
        public virtual string? SupNo { get; set; }
        public virtual string? RefNo { get; set; }
        public virtual string Pono { get; set; } = null!;
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
        public virtual string ReceivedBy { get; set; } = null!;
        public virtual string? PreturnRecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual PurchaseOrderMaster? PonoNavigation { get; set; }
        public virtual Employee? ReceivedByNavigation { get; set; }
        public virtual Supplier? SupNoNavigation { get; set; }
        public virtual PurchaseReturnMaster? PurchaseReturnMasterFile { get; set; }
        public virtual ICollection<ReceivingReportDetail>? ReceivingReportDetailFile { get; set; }

        public static ReceivingReportMaster Create(ReceivingReportMaster receivingReportMaster, string createdBy)
        {
            //Place your Business logic here
            receivingReportMaster.Created_by = createdBy;
            receivingReportMaster.Date_created = DateTime.Now;
            return receivingReportMaster;
        }

        public static ReceivingReportMaster Update(ReceivingReportMaster receivingReportMaster, string editedBy)
        {
            //Place your Business logic here
            receivingReportMaster.Edited_by = editedBy;
            receivingReportMaster.Date_edited = DateTime.Now;
            return receivingReportMaster;
        }

        public void AddDetail(ReceivingReportDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            ReceivingReportDetailFile ??= new List<ReceivingReportDetail>();

            ReceivingReportDetailFile.Add(detail);
        }
    }
}
