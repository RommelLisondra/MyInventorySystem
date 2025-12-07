using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class StockCountMasterModel 
    {
        public int id { get; set; }
        public string Scmno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string? PreparedBy { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;

        public EmployeeModel? PreparedByNavigation { get; set; }
        public WarehouseModel WarehouseCodeNavigation { get; set; } = null!;
        public ICollection<StockCountDetailModel>? StockCountDetail { get; set; }
    }
}
