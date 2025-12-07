using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class StockTransferDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual long TransferId { get; set; }
        public virtual string ItemDetailNo { get; set; } = null!;
        public virtual int Quantity { get; set; }
        public virtual decimal Uprice { get; set; }
        public virtual decimal Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual StockTransfer Transfer { get; set; } = null!;
    }
}
