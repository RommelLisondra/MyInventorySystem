using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class SystemSettingDto
    {
        public int id { get; set; }
        public string SettingKey { get; set; } = null!;
        public string? SettingValue { get; set; }
        public string? Description { get; set; }
        public string? RecStatus { get; set; }
    }
}
