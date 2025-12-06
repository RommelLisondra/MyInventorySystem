using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class StockTransferDetailDto 
    {
        public int id { get; set; }
        public long TransferId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Uprice { get; set; }
        public decimal Amount { get; set; }
        public string? RecStatus { get; set; }

        public StockTransferDto Transfer { get; set; } = null!;
    }
}
