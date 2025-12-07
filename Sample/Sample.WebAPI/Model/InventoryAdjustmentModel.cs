using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class InventoryAdjustmentModel 
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
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;

        public ICollection<InventoryAdjustmentDetailModel>? InventoryAdjustmentDetail { get; set; }
    }
}
