using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class SalesInvoiceDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Sidno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyInv { get; set; }
        public virtual int? QtyRet { get; set; }
        public virtual int? QtyDel { get; set; }
        public virtual decimal? Uprice { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string? SrrecStatus { get; set; }
        public virtual string? DrrecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual SalesInvoiceMaster SidnoNavigation { get; set; } = null!;

        public static Customer Create(Customer customer, string createdBy)
        {
            //Place your Business logic here
            customer.Created_by = createdBy;
            customer.Date_created = DateTime.Now;
            return customer;
        }

        public static Customer Update(Customer customer, string editedBy)
        {
            //Place your Business logic here
            customer.Edited_by = editedBy;
            customer.Date_edited = DateTime.Now;
            return customer;
        }

        public void UpdateInvoicedQty(int? qty)
        {
            QtyInv += qty;
            DrrecStatus = (QtyInv == QtyDel) ? "C" : "O";
            RecStatus = (QtyInv == QtyDel) ? "C" : "O";
        }
    }
}
