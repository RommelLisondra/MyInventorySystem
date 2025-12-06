using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class ItemModel
    {
        public int id { get; set; }
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
        public BrandModel Brand { get; set; } = null!;
        public CategoryModel Category { get; set; } = null!;
        public ICollection<ItemDetailModel>? ItemDetails { get; set; }
    }
}
