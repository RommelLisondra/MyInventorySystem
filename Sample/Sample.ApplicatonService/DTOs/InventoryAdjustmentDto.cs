using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class InventoryAdjustmentDto 
    {
        public int id { get; set; }
        public string AdjustmentNo { get; set; } = null!;
        public DateTime AdjustmentDate { get; set; }
        public int CompanyId { get; set; }
        public int WarehouseId { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }

        public ICollection<InventoryAdjustmentDetailDto>? InventoryAdjustmentDetail { get; set; }
    }
}
