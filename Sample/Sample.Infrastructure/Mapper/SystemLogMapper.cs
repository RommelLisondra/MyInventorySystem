using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class SystemLogMapper
    {
        public static entityframework.SystemLog MapToEntityFramework(entities.SystemLog entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SystemLog entity cannot be null.");

            var mapped = new entityframework.SystemLog
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                LogType = entity.LogType ?? string.Empty,
                Message = entity.Message,
                StackTrace = entity.StackTrace,
                LoggedBy = entity.LoggedBy,
                LoggedDate = entity.LoggedDate,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.SystemLog MapToEntity(entityframework.SystemLog systemLog)
        {
            if (systemLog == null) return null!;

            return new entities.SystemLog
            {
                id = systemLog.Id,
                LogType = systemLog.LogType ?? string.Empty,
                Message = systemLog.Message,
                StackTrace = systemLog.StackTrace,
                LoggedBy = systemLog.LoggedBy,
                LoggedDate = systemLog.LoggedDate,
                RecStatus = systemLog.RecStatus,
            };
        }
    }
}
