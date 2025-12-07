using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class ItemBarcode : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }               // Primary key
        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual string Barcode { get; set; } = null!;
        public virtual string? Description { get; set; }  // Optional note
        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public virtual Item Item { get; set; } = null!;

        public static ItemBarcode Create(ItemBarcode itemBarcode, string createdBy)
        {
            //Place your Business logic here
            itemBarcode.Created_by = createdBy;
            itemBarcode.Date_created = DateTime.Now;
            return itemBarcode;
        }

        public static ItemBarcode Update(ItemBarcode itemBarcode, string editedBy)
        {
            //Place your Business logic here
            itemBarcode.Edited_by = editedBy;
            itemBarcode.Date_edited = DateTime.Now;
            return itemBarcode;
        }
    }
}
