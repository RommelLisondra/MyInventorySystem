using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class SubCategoryMapper
    {
        public static entityframework.SubCategory MapToEntityFramework(entities.SubCategory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var mapped = new entityframework.SubCategory
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                SubCategoryName = entity.SubCategoryName ?? string.Empty,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                RecStatus = entity.RecStatus
            };

            return mapped;
        }

        public static entities.SubCategory MapToEntity(entityframework.SubCategory subCategory)
        {
            if (subCategory == null) return null!;

            return new entities.SubCategory
            {
                id = subCategory.Id,
                SubCategoryName = subCategory.SubCategoryName ?? string.Empty,
                Description = subCategory.Description,
                CategoryId = subCategory.CategoryId,
                CreatedDateTime = subCategory.CreatedDateTime,
                ModifiedDateTime = subCategory.ModifiedDateTime,
                RecStatus = subCategory.RecStatus
            };
        }
    }
}
