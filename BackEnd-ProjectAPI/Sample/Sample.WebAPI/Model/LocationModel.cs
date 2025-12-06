using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class LocationModel 
    {
        public int id { get; set; }
        public string LocationCode { get; set; } = null!;
        public string WareHouseCode { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? Status { get; set; }

        public WarehouseModel WareHouseCodeNavigation { get; set; } = null!;
        public ItemDetailModel? ItemDetail { get; set; }
    }
}
