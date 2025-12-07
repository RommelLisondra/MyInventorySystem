using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemUnitMeasure
    {
        public int Id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string UnitCode { get; set; } = null!;
        public decimal? ConversionRate { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
    }
}
