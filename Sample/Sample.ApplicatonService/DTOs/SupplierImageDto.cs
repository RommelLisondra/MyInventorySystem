using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class SupplierImageDto 
    {
        public int id { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? FilePath { get; set; }

        public SupplierDto SupNoNavigation { get; set; } = null!;
    }
}
