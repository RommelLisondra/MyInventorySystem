using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class InventoryAdjustmentDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual long DetailId { get; set; }
        public virtual long AdjustmentId { get; set; }
        public virtual string ItemDetailNo { get; set; } = null!;
        public virtual int Quantity { get; set; }
        public virtual decimal Uprice { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string MovementType { get; set; } = null!;

        public virtual InventoryAdjustment Adjustment { get; set; } = null!;
    }
}
