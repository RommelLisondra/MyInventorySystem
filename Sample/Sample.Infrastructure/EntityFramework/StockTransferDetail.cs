using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class StockTransferDetail
    {
        public int Id { get; set; }
        public int TransferId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Uprice { get; set; }
        public decimal Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual StockTransfer Transfer { get; set; } = null!;
    }
}
