using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ClassificationMapper
    {
        public static entityframework.Classification MapToEntityFramework(entities.Classification entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "classification entity cannot be null.");

            var mapped = new entityframework.Classification
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                Name = entity.Name ?? string.Empty,
                Description = entity.Description,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.Classification MapToEntity(entityframework.Classification classification)
        {
            if (classification == null) return null!;

            return new entities.Classification
            {
                id = classification.Id,
                Name = classification.Name ?? string.Empty,
                Description = classification.Description,
                CreatedDateTime = classification.CreatedDateTime,
                ModifiedDateTime = classification.ModifiedDateTime,
                RecStatus = classification.RecStatus,
            };
        }
    }
}
