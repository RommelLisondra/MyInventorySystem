using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ExpenseCategoryMapper
    {
        public static entityframework.ExpenseCategory MapToEntityFramework(entities.ExpenseCategory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var mapped = new entityframework.ExpenseCategory
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                Code = entity.Code ?? string.Empty,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate
            };

            return mapped;
        }

        public static entities.ExpenseCategory MapToEntity(entityframework.ExpenseCategory expenseCategory)
        {
            if (expenseCategory == null) return null!;

            return new entities.ExpenseCategory
            {
                id = expenseCategory.Id,
                Code = expenseCategory.Code ?? string.Empty,
                Name = expenseCategory.Name,
                Description = expenseCategory.Description,
                IsActive = expenseCategory.IsActive,
                CreatedDate = expenseCategory.CreatedDate,
                ModifiedDate = expenseCategory.ModifiedDate
            };
        }
    }
}
