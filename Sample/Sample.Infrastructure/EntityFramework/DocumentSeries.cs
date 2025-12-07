using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class DocumentSeries
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string DocumentType { get; set; } = null!;
        public string? Prefix { get; set; }
        public int NextNumber { get; set; }
        public int Year { get; set; }
        public string? Suffix { get; set; }
        public string? RecStatus { get; set; }
    }
}
