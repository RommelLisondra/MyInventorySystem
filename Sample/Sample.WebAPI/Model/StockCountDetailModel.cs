using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class StockCountDetailModel 
    {
        public int id { get; set; }
        public string Scmno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? CountedQty { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
        public StockCountMasterModel ScmnoNavigation { get; set; } = null!;
    }
}
