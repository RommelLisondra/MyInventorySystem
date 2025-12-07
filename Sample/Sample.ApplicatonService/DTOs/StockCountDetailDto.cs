using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class StockCountDetailDto 
    {
        public int id { get; set; }
        public string Scmno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? CountedQty { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
        public StockCountMasterDto ScmnoNavigation { get; set; } = null!;
    }
}
