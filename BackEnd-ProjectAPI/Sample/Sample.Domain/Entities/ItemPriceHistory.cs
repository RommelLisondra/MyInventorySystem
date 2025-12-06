
using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class ItemPriceHistory : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }               // Primary key
        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual decimal Price { get; set; }        // Selling price
        public virtual DateTime EffectiveDate { get; set; } = DateTime.Now;
        public virtual string? Remarks { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;

        public static ItemPriceHistory Create(ItemPriceHistory itemPrice, string createdBy)
        {
            //Place your Business logic here
            itemPrice.Created_by = createdBy;
            itemPrice.Date_created = DateTime.Now;
            return itemPrice;
        }

        public static ItemPriceHistory Update(ItemPriceHistory itemPrice, string editedBy)
        {
            //Place your Business logic here
            itemPrice.Edited_by = editedBy;
            itemPrice.Date_edited = DateTime.Now;
            return itemPrice;
        }
    }
}
