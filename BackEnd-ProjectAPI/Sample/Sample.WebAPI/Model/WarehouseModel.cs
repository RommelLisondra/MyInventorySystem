using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class WarehouseModel
    {
        public int id { get; set; }
        public string WareHouseCode { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? Status { get; set; }

        public ItemDetailModel? ItemDetail { get; set; }
        public LocationModel? Location { get; set; }
    }
}
