using Sample.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemWarehouseMapping
    {
        public int Id { get; set; }

        public int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public int WarehouseId { get; set; }      // FK → Warehouse
        public int? BranchId { get; set; }        // Optional, can derive from Warehouse

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }

        // Navigation properties
        public virtual Item Item { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;
        public virtual Branch? Branch { get; set; }
    }
}
