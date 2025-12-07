using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class SupplierImageModel 
    {
        public int id { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? FilePath { get; set; }

        public SupplierModel SupNoNavigation { get; set; } = null!;
    }
}
