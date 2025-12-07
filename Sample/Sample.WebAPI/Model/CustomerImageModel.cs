using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class CustomerImageModel
    {
        public int id { get; set; }
        public string CustNo { get; set; } = null!;
        public string? FilePath { get; set; }
        public byte[]? Picture { get; set; }

        public CustomerModel CustNoNavigation { get; set; } = null!;
    }
}
