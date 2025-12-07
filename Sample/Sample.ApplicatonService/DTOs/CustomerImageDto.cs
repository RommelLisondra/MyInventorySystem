using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class CustomerImageDto
    {
        public int id { get; set; }
        public string CustNo { get; set; } = null!;
        public string? FilePath { get; set; }
        public byte[]? Picture { get; set; }

        public CustomerDto CustNoNavigation { get; set; } = null!;
    }
}
