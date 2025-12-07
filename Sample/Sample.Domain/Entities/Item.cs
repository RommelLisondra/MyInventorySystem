using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class Item : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemCode { get; set; } = null!;
        public virtual string ItemName { get; set; } = null!;
        public virtual string? Desc { get; set; }
        public virtual int BrandId { get; set; }          // FK to Brand
        public virtual string Model { get; set; } = null!;
        public virtual int CategoryId { get; set; }       // FK to Category
        public virtual int ClassificationId { get; set; } // ← FK to Classification
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        // Navigation Properties
        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ItemDetail>? ItemDetails { get; set; }

        public static Item Create(Item item, string createdBy)
        {
            //Place your Business logic here
            item.Created_by = createdBy;
            item.Date_created = DateTime.Now;
            return item;
        }

        public static Item Update(Item item, string editedBy)
        {
            //Place your Business logic here
            item.Edited_by = editedBy;
            item.Date_edited = DateTime.Now;
            return item;
        }
    }
}
