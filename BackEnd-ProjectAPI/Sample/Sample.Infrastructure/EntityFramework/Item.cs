using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Item
    {
        public Item()
        {
            ItemDetails = new HashSet<ItemDetail>();
        }

        public int Id { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string? Desc { get; set; }
        public int BrandId { get; set; }          // FK to Brand
        public string Model { get; set; } = null!;
        public int CategoryId { get; set; }       // FK to Category
        public int ClassificationId { get; set; } // ← FK to Classification
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Properties
        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
    }
}
