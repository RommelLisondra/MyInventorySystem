using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class StockCountDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Scmno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? CountedQty { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual StockCountMaster ScmnoNavigation { get; set; } = null!;
    }
}
