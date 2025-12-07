using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class SystemSettingMapper
    {
        public static entityframework.SystemSetting MapToEntityFramework(entities.SystemSetting entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "systemSetting entity cannot be null.");

            var mapped = new entityframework.SystemSetting
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                SettingKey = entity.SettingKey ?? string.Empty,
                SettingValue = entity.SettingValue,
                Description = entity.Description,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.SystemSetting MapToEntity(entityframework.SystemSetting systemSetting)
        {
            if (systemSetting == null) return null!;

            return new entities.SystemSetting
            {
                id = systemSetting.Id,
                SettingKey = systemSetting.SettingKey ?? string.Empty,
                SettingValue = systemSetting.SettingValue,
                Description = systemSetting.Description,
                RecStatus = systemSetting.RecStatus,
            };
        }
    }
}
