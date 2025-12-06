using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemWarehouseMappingDto
    {
        public virtual int id { get; set; }

        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual int WarehouseId { get; set; }      // FK → Warehouse
        public virtual int? BranchId { get; set; }        // Optional, can derive from Warehouse

        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }

        // Navigation properties
        public virtual ItemDto Item { get; set; } = null!;
        public virtual WarehouseDto Warehouse { get; set; } = null!;
        public virtual BranchDto? Branch { get; set; }
    }
}
