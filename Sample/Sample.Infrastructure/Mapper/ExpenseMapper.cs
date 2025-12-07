using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ExpenseMapper
    {
        public static entityframework.Expense MapToEntityFramework(entities.Expense entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var mapped = new entityframework.Expense
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ExpenseNo = entity.ExpenseNo ?? string.Empty,
                ExpenseDate = entity.ExpenseDate,
                ExpenseCategoryId = entity.ExpenseCategoryId,
                Amount = entity.Amount,
                Vendor = entity.Vendor,
                ReferenceNo = entity.ReferenceNo,
                Notes = entity.Notes,
                CreatedDate = entity.CreatedDate,
                IsDeleted = entity.IsDeleted,
            };

            return mapped;
        }

        public static entities.Expense MapToEntity(entityframework.Expense customer)
        {
            if (customer == null) return null!;

            return new entities.Expense
            {
                id = customer.Id,
                ExpenseNo = customer.ExpenseNo ?? string.Empty,
                ExpenseDate = customer.ExpenseDate,
                ExpenseCategoryId = customer.ExpenseCategoryId,
                Amount = customer.Amount,
                Vendor = customer.Vendor,
                ReferenceNo = customer.ReferenceNo,
                Notes = customer.Notes,
                CreatedDate = customer.CreatedDate,
                IsDeleted = customer.IsDeleted,
            };
        }
    }
}
