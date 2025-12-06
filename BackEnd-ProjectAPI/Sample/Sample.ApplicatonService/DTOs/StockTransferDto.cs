using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class StockTransferDto 
    {
        public int id { get; set; }
        public string TransferNo { get; set; } = null!;
        public DateTime TransferDate { get; set; }
        public int CompanyId { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchDto Branch { get; set; } = null!;

        public ICollection<StockTransferDetailDto>? StockTransferDetail { get; set; }
    }
}
