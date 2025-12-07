using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class SystemLogDto 
    {
        public int id { get; set; }
        public string LogType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string? LoggedBy { get; set; }
        public DateTime LoggedDate { get; set; }
        public string? RecStatus { get; set; }
    }
}
