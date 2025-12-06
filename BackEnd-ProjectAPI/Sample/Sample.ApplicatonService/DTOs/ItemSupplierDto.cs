using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemSupplierDto
    {
        public int id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string SupplierNo { get; set; } = null!;
        public decimal? Unitprice { get; set; }
        public string? LeadTime { get; set; }
        public string? Terms { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
        public SupplierDto SupNoNavigation { get; set; } = null!;
    }
}



