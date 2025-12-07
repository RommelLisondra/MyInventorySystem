using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class ItemPriceHistoryModel
    {
        public int id { get; set; }               // Primary key
        public int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public decimal Price { get; set; }        // Selling price
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public int BranchId { get; set; }
        public BranchModel Branch { get; set; } = null!;
        public ItemModel Item { get; set; } = null!;
    }
}
