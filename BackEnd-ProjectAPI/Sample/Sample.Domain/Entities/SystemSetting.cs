using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class SystemSetting : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int SettingId { get; set; }
        public virtual string SettingKey { get; set; } = null!;
        public virtual string? SettingValue { get; set; }
        public string? Description { get; set; }
        public string? RecStatus { get; set; }

        public static SystemSetting Create(SystemSetting systemSetting, string createdBy)
        {
            //Place your Business logic here
            systemSetting.Created_by = createdBy;
            systemSetting.Date_created = DateTime.Now;
            return systemSetting;
        }

        public static SystemSetting Update(SystemSetting systemSetting, string editedBy)
        {
            //Place your Business logic here
            systemSetting.Edited_by = editedBy;
            systemSetting.Date_edited = DateTime.Now;
            return systemSetting;
        }
    }
}
