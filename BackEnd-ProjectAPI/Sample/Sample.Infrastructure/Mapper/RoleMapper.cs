using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class RoleMapper
    {
        public static entityframework.Role MapToEntityFramework(entities.Role entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.Role
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                RoleName = entity.RoleName ?? string.Empty,
                Description = entity.Description,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.Role MapToEntity(entityframework.Role role)
        {
            if (role == null) return null!;

            return new entities.Role
            {
                id = role.Id,
                RoleName = role.RoleName ?? string.Empty,
                Description = role.Description,
                RecStatus = role.RecStatus,
            };
        }
    }
}
