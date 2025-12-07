using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class SalesReturnMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Srmno { get; set; } = null!;
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
        public virtual decimal? Balance { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual string? PrepBy { get; set; }
        public virtual string? ApprBy { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Customer? CustNoNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual SalesInvoiceMaster SimnoNavigation { get; set; } = null!;
        public virtual ICollection<SalesReturnDetail>? SalesReturnDetailFile { get; set; }

        public static SalesReturnMaster Create(SalesReturnMaster salesReturn, string createdBy)
        {
            //Place your Business logic here
            salesReturn.Created_by = createdBy;
            salesReturn.Date_created = DateTime.Now;
            return salesReturn;
        }

        public static SalesReturnMaster Update(SalesReturnMaster salesReturn, string editedBy)
        {
            //Place your Business logic here
            salesReturn.Edited_by = editedBy;
            salesReturn.Date_edited = DateTime.Now;
            return salesReturn;
        }

        public void AddDetail(SalesReturnDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            SalesReturnDetailFile ??= new List<SalesReturnDetail>();

            SalesReturnDetailFile.Add(detail);
        }
    }
}
