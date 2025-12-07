using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class WarehouseDto
    {
        public int id { get; set; }
        public string WareHouseCode { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? Status { get; set; }

        public ItemDetailDto? ItemDetail { get; set; }
        public LocationDto? Location { get; set; }
    }
}
