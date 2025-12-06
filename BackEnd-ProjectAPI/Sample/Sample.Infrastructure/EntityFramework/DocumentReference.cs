using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class DocumentReference
    {
        public int Id { get; set; }
        public string RefNo { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public DateTime? DateReferenced { get; set; }
        public string? RecStatus { get; set; }
    }
}
