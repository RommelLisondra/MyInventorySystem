using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SystemLog
    {
        public int Id { get; set; }
        public string LogType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string? LoggedBy { get; set; }
        public DateTime LoggedDate { get; set; }
        public string? RecStatus { get; set; }
    }
}
