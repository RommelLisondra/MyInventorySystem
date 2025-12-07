using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class SalesInvoiceMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Simno { get; set; } = null!;
        public virtual string Somno { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
        public virtual string CustNo { get; set; } = null!;
        public virtual string? Terms { get; set; }
        public virtual string? TypesOfPay { get; set; }
        public virtual string? SalesRef { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual decimal? DisPercent { get; set; }
        public virtual decimal? DisTotal { get; set; }
        public virtual decimal? DeliveryCost { get; set; }
        public virtual decimal? Balance { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string PrepBy { get; set; } = null!;
        public virtual string ApprBy { get; set; } = null!;
        public virtual string? OrrecStatus { get; set; }
        public virtual string? DrrecStatus { get; set; }
        public virtual string? SrrecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Customer? CustNoNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual SalesOrderMaster? SomnoNavigation { get; set; }
        public virtual SalesReturnMaster? SalesReturnMasterFile { get; set; }
        public virtual ICollection<SalesInvoiceDetail>? SalesInvoiceDetail { get; set; }

        public static SalesInvoiceMaster Update(SalesInvoiceMaster salesInvoice, string editedBy)
        {
            //Place your Business logic here
            salesInvoice.Edited_by = editedBy;
            salesInvoice.Date_edited = DateTime.Now;
            return salesInvoice;
        }

        public SalesInvoiceMaster() { }

        public static SalesInvoiceMaster Create(SalesInvoiceMaster dto, string createdBy)
        {
            return new SalesInvoiceMaster
            {
                Simno = dto.Simno ?? throw new ArgumentNullException(nameof(dto.Simno)),
                Somno = dto.Somno ?? throw new ArgumentNullException(nameof(dto.Somno)),
                CustNo = dto.CustNo ?? throw new ArgumentNullException(nameof(dto.CustNo)),
                Date = dto.Date,
                Terms = dto.Terms,
                PrepBy = createdBy,
                ApprBy = dto.ApprBy,
                Total = dto.Total,
                RecStatus = "O"
            };
        }

        public void AddDetail(SalesInvoiceDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            SalesInvoiceDetail ??= new List<SalesInvoiceDetail>();

            SalesInvoiceDetail.Add(detail);
        }

        public void DecreaseBalance(decimal? amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount to decrease must be greater than zero.", nameof(amount));

            Balance -= amount;
        }

        public void ChangeSalesInvoiceMasterStatus(decimal? balance)
        {
            RecStatus = (balance <= 0) ? "C" : "O";
        }
    }
}
