using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class StockTransferDetailModel 
    {
        public int id { get; set; }
        public long TransferId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string? RecStatus { get; set; }

        public StockTransferModel Transfer { get; set; } = null!;
    }
}
