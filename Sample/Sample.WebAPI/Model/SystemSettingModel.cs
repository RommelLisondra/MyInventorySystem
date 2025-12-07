using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class SystemSettingModel
    {
        public int id { get; set; }
        public string SettingKey { get; set; } = null!;
        public string? SettingValue { get; set; }
        public string? Description { get; set; }
        public string? RecStatus { get; set; }
    }
}
