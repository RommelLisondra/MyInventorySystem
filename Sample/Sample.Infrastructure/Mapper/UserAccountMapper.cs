using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class UserAccountMapper
    {
        public static entityframework.UserAccount MapToEntityFramework(entities.UserAccount entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.UserAccount
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                Username = entity.Username ?? string.Empty,
                PasswordHash = entity.PasswordHash,
                FullName = entity.FullName,
                Email = entity.Email,
                RoleId = entity.RoleId,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.UserAccount MapToEntity(entityframework.UserAccount userAccount)
        {
            if (userAccount == null) return null!;

            return new entities.UserAccount
            {
                id = userAccount.Id, 
                Username = userAccount.Username ?? string.Empty,
                PasswordHash = userAccount.PasswordHash,
                FullName = userAccount.FullName,
                Email = userAccount.Email,
                RoleId = userAccount.RoleId,
                RecStatus = userAccount.RecStatus,
            };
        }
    }
}
