using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class DocumentReferenceDto 
    {
        public int id { get; set; }
        public string RefNo { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public DateTime? DateReferenced { get; set; }
        public string? RecStatus { get; set; }
    }
}
