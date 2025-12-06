using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemImage
    {
        public int Id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string? ImagePath { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
    }
}
