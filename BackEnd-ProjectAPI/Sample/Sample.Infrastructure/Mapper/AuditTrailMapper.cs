using System;
using System.Collections.Generic;
using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class AuditTrailMapper
    {
        public static entityframework.AuditTrail MapToEntityFramework(entities.AuditTrail entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.AuditTrail
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                TableName = entity.TableName ?? string.Empty,
                PrimaryKey = entity.PrimaryKey,
                Action = entity.Action,
                ChangedBy = entity.ChangedBy,
                ChangedDate = entity.ChangedDate,
                OldValue = entity.OldValue,
                NewValue = entity.NewValue
            };

            return mapped;
        }

        public static entities.AuditTrail MapToEntity(entityframework.AuditTrail auditTrail)
        {
            if (auditTrail == null) return null!;

            return new entities.AuditTrail
            {
                id = auditTrail.Id,
                TableName = auditTrail.TableName ?? string.Empty,
                PrimaryKey = auditTrail.PrimaryKey,
                Action = auditTrail.Action,
                ChangedBy = auditTrail.ChangedBy,
                ChangedDate = auditTrail.ChangedDate,
                OldValue = auditTrail.OldValue,
                NewValue = auditTrail.NewValue
            };
        }
    }
}
