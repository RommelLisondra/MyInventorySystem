using Sample.ApplicationService.DTOs;
using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class StockTransferModel 
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

        public BranchModel Branch { get; set; } = null!;

        public ICollection<StockTransferDetailModel>? StockTransferDetail { get; set; }
    }
}
