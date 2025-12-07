using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class DeliveryReceiptDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Drdno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyDel { get; set; }
        public virtual decimal? Uprice { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual DeliveryReceiptMaster DrdnoNavigation { get; set; } = null!;
        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
    }
}
