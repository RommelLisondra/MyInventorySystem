using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class AccountMapper
    {

        public static entityframework.Account MapToEntityFramework(entities.Account entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Account entity cannot be null.");

            var mapped = new entityframework.Account
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                AccountCode = entity.AccountCode ?? string.Empty,
                AccountName = entity.AccountName,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
            };

            return mapped;
        }

        public static entities.Account MapToEntity(entityframework.Account account)
        {
            if (account == null) return null!;

            return new entities.Account
            {
                id = account.Id,
                AccountCode = account.AccountCode ?? string.Empty,
                AccountName = account.AccountName,
                Description = account.Description,
                IsActive = account.IsActive,
                CreatedDateTime = account.CreatedDateTime,
                ModifiedDateTime = account.ModifiedDateTime,
            };
        }
    }
}
