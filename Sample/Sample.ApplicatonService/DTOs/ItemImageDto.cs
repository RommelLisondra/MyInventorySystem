using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemImageDto
    {
        public int id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string? FilePath { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
    }
}
