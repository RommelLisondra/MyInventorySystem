using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Location
    {
        public int Id { get; set; }
        public string LocationCode { get; set; } = null!;
        public string? Name { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string? Remarks { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public virtual Warehouse WarehouseCodeNavigation { get; set; } = null!;
    }
}
