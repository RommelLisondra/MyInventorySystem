using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class PurchaseReturnDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string PretDno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyRet { get; set; }
        public virtual decimal? Uprice { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseReturnMaster PretDnoNavigation { get; set; } = null!;

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
