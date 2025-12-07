using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class CustomerImage
    {
        public int Id { get; set; }
        public string CustNo { get; set; } = null!;
        public string? FilePath { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public virtual Customer CustNoNavigation { get; set; } = null!;
    }
}
