using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class AuditTrail
    {
        public int Id { get; set; }
        public string TableName { get; set; } = null!;
        public string PrimaryKey { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string ChangedBy { get; set; } = null!;
        public DateTime ChangedDate { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
