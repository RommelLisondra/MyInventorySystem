using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemPriceHistoryDto
    {
        public int id { get; set; }               // Primary key
        public int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public decimal Price { get; set; }        // Selling price
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public int BranchId { get; set; }
        public BranchDto Branch { get; set; } = null!;
        public ItemDto Item { get; set; } = null!;
    }
}
