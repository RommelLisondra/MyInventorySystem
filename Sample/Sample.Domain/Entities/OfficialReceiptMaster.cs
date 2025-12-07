using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class OfficialReceiptMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Orno { get; set; } = null!;
        public virtual string? CustNo { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual decimal? TotalAmtPaid { get; set; }
        public virtual decimal? DisPercent { get; set; }
        public virtual decimal? DisTotal { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual string? FormPay { get; set; }
        public virtual decimal? CashAmt { get; set; }
        public virtual decimal? CheckAmt { get; set; }
        public virtual string? CheckNo { get; set; }
        public virtual string? BankName { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual string? PrepBy { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual Customer? custNoNavigation { get; set; }
        public virtual Employee? prepByNavigation { get; set; }
        public virtual ICollection<OfficialReceiptDetail>? OfficialReceiptDetailFile { get; set; }

        public static OfficialReceiptMaster Create(OfficialReceiptMaster officialReceipt, string createdBy)
        {
            //Place your Business logic here
            officialReceipt.Created_by = createdBy;
            officialReceipt.Date_created = DateTime.Now;
            return officialReceipt;
        }

        public static OfficialReceiptMaster Update(OfficialReceiptMaster officialReceipt, string editedBy)
        {
            //Place your Business logic here
            officialReceipt.Edited_by = editedBy;
            officialReceipt.Date_edited = DateTime.Now;
            return officialReceipt;
        }

        public void AddDetail(OfficialReceiptDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            OfficialReceiptDetailFile ??= new List<OfficialReceiptDetail>();

            OfficialReceiptDetailFile.Add(detail);
        }
    }
}
