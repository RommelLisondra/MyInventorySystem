using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class CategoryMapper
    {
        public static entityframework.Category MapToEntityFramework(entities.Category entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Category entity cannot be null.");

            var mapped = new entityframework.Category
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                CategoryName = entity.CategoryName ?? string.Empty,
                Description = entity.Description,
                BrandId = entity.BrandId,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.Category MapToEntity(entityframework.Category category)
        {
            if (category == null) return null!;

            return new entities.Category
            {
                id = category.Id,
                CategoryName = category.CategoryName ?? string.Empty,
                Description = category.Description,
                BrandId = category.BrandId,
                CreatedDateTime = category.CreatedDateTime,
                ModifiedDateTime = category.ModifiedDateTime,
                RecStatus = category.RecStatus,
            };
        }
    }
}
