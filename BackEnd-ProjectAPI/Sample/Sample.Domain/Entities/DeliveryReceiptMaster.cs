using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class DeliveryReceiptMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Drmno { get; set; } = null!;
        public virtual string? CustNo { get; set; }
        public virtual string Simno { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
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
        public virtual decimal? DeliveryCost { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual string? PrepBy { get; set; }
        public virtual string? ApprBy { get; set; }
        public virtual string? RecStatus { get; set; }
        public virtual int BranchId { get; set; }

        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Customer? CustNoNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual ICollection<DeliveryReceiptDetail>? DeliveryReceiptDetail { get; set; }

        public static DeliveryReceiptMaster Create(DeliveryReceiptMaster deliveryReceipt, string createdBy)
        {
            //Place your Business logic here
            deliveryReceipt.Created_by = createdBy;
            deliveryReceipt.Date_created = DateTime.Now;
            return deliveryReceipt;
        }

        public static DeliveryReceiptMaster Update(DeliveryReceiptMaster deliveryReceipt, string editedBy)
        {
            //Place your Business logic here
            deliveryReceipt.Edited_by = editedBy;
            deliveryReceipt.Date_edited = DateTime.Now;
            return deliveryReceipt;
        }

        public void AddDetail(DeliveryReceiptDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            DeliveryReceiptDetail ??= new List<DeliveryReceiptDetail>();

            DeliveryReceiptDetail.Add(detail);
        }
    }
}
