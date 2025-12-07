using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class RolePermissionMapper
    {
        public static entityframework.RolePermission MapToEntityFramework(entities.RolePermission entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.RolePermission
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                RoleId = entity.RoleId,
                PermissionName = entity.PermissionName,
                CanView = entity.CanView,
                CanAdd = entity.CanAdd,
                CanEdit = entity.CanEdit,
                CanDelete = entity.CanDelete,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.RolePermission MapToEntity(entityframework.RolePermission rolePermission)
        {
            if (rolePermission == null) return null!;

            return new entities.RolePermission
            {
                id = rolePermission.Id,
                RoleId = rolePermission.RoleId,
                PermissionName = rolePermission.PermissionName,
                CanView = rolePermission.CanView,
                CanAdd = rolePermission.CanAdd,
                CanEdit = rolePermission.CanEdit,
                CanDelete = rolePermission.CanDelete,
                RecStatus = rolePermission.RecStatus,
            };
        }
    }
}
