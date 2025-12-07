using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemUnitMeasureDto
    {
        public int id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string UnitCode { get; set; } = null!;
        public decimal? ConversionRate { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
    }
}
