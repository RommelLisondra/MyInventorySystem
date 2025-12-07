using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class ItemImageModel
    {
        public int id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string? FilePath { get; set; }

        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
    }
}
