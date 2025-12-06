using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class DocumentSeriesModel 
    {
        public int id { get; set; }
        public int BranchId { get; set; }
        public string DocumentType { get; set; } = null!;
        public string? Prefix { get; set; }
        public int NextNumber { get; set; }
        public int Year { get; set; }
        public string? Suffix { get; set; }
        public string? RecStatus { get; set; }
    }
}
