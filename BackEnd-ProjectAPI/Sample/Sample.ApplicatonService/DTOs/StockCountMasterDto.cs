using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class StockCountMasterDto 
    {
        public int id { get; set; }
        public string Scmno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string? PreparedBy { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchDto Branch { get; set; } = null!;
        public EmployeeDto? PreparedByNavigation { get; set; }
        public WarehouseDto WarehouseCodeNavigation { get; set; } = null!;
        public ICollection<StockCountDetailDto>? StockCountDetail { get; set; }
    }
}
