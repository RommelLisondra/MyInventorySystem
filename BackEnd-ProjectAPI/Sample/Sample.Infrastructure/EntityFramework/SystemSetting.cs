using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SystemSetting
    {
        public int Id { get; set; }
        public string SettingKey { get; set; } = null!;
        public string? SettingValue { get; set; }
        public string? Description { get; set; }
        public string? RecStatus { get; set; }
    }
}
