using Sample.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemBarcode
    {
        public int Id { get; set; }               // Primary key
        public int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public string Barcode { get; set; } = null!;
        public string? Description { get; set; }  // Optional note
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public virtual Item Item { get; set; } = null!;
    }
}
