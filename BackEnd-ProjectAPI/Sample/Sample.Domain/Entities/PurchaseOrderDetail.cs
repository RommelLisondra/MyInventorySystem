using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;
using Sample.Domain.Contracts;

namespace Sample.Domain.Entities
{
    public class PurchaseOrderDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Podno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyOrder { get; set; }
        public virtual int? QtyReceived { get; set; }
        public virtual decimal? Uprice { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string? RrrecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseOrderMaster PodnoNavigation { get; set; } = null!;

        public void AddPurchaseOrderQtyOnOrder(int? rrQtyReceive, int? poQtyreceive)
        {
            QtyReceived = (poQtyreceive ?? 0) + (rrQtyReceive ?? 0);
            RecStatus = (QtyOrder == QtyReceived) ? "C" : "O";
            RrrecStatus = (QtyOrder == QtyReceived) ? "C" : "O";
        }
    }
}
