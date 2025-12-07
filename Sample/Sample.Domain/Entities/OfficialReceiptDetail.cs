using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class OfficialReceiptDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Ordno { get; set; } = null!;
        public virtual string Simno { get; set; } = null!;
        public virtual decimal? AmountPaid { get; set; }
        public virtual decimal? AmountDue { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual OfficialReceiptMaster OrdnoNavigation { get; set; } = null!;
        public virtual SalesInvoiceMaster SalesInvoiceMasterFileNavigation { get; set; } = null!;

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
    }
}
