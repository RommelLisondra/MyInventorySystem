using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class SalesOrderMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Somno { get; set; } = null!;
        public virtual DateTime? Date { get; set; }
        public virtual string? CustNo { get; set; } = string.Empty;
        public virtual string? Terms { get; set; }
        public virtual string? TypesOfPay { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual decimal? DisPercent { get; set; }
        public virtual decimal? DisTotal { get; set; }
        public virtual decimal? SubTotal { get; set; }
        public virtual decimal? Vat { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual string PrepBy { get; set; } = null!;
        public virtual string ApprBy { get; set; } = null!;
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; } = null!;
        public virtual Customer? CustNoNavigation { get; set; }
        public virtual Employee? PrepByNavigation { get; set; } = null!;
        public virtual ICollection<SalesOrderDetail>? SalesOrderDetail { get; set; }

        public static SalesOrderMaster Create(SalesOrderMaster salesOrder, string createdBy)
        {
            //Place your Business logic here
            salesOrder.Created_by = createdBy;
            salesOrder.Date_created = DateTime.Now;
            return salesOrder;
        }

        public static SalesOrderMaster Update(SalesOrderMaster salesOrder, string editedBy)
        {
            //Place your Business logic here
            salesOrder.Edited_by = editedBy;
            salesOrder.Date_edited = DateTime.Now;
            return salesOrder;
        }

        public void AddDetail(SalesOrderDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            SalesOrderDetail ??= new List<SalesOrderDetail>();

            SalesOrderDetail.Add(detail);
        }
    }
}
