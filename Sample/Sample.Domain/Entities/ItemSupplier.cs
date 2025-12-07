using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ItemSupplier : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual string SupplierNo { get; set; } = null!;
        public virtual decimal? Unitprice { get; set; }
        public virtual string? LeadTime { get; set; }
        public virtual string? Terms { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual Supplier SupNoNavigation { get; set; } = null!;

        public static ItemSupplier Create(ItemSupplier itemSupplier, string createdBy)
        {
            //Place your Business logic here
            itemSupplier.Created_by = createdBy;
            itemSupplier.Date_created = DateTime.Now;
            return itemSupplier;
        }

        public static ItemSupplier Update(ItemSupplier itemSupplier, string editedBy)
        {
            //Place your Business logic here
            itemSupplier.Edited_by = editedBy;
            itemSupplier.Date_edited = DateTime.Now;
            return itemSupplier;
        }
    }
}
