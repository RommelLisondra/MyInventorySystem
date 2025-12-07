using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class BrandMapper
    {
        public static entityframework.Brand MapToEntityFramework(entities.Brand entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var mapped = new entityframework.Brand
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                BrandName = entity.BrandName ?? string.Empty,
                Description = entity.Description,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.Brand MapToEntity(entityframework.Brand customer)
        {
            if (customer == null) return null!;

            return new entities.Brand
            {
                id = customer.Id, 
                BrandName = customer.BrandName,
                Description = customer.Description,
                CreatedDateTime = customer.CreatedDateTime,
                ModifiedDateTime = customer.ModifiedDateTime,
                RecStatus = customer.RecStatus,
            };
        }
    }
}
