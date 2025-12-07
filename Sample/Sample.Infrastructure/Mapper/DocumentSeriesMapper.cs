using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class DocumentSeriesMapper
    {
        public static entityframework.DocumentSeries MapToEntityFramework(entities.DocumentSeries entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.DocumentSeries
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                BranchId = entity.BranchId,
                DocumentType = entity.DocumentType,
                Prefix = entity.Prefix,
                NextNumber = entity.NextNumber,
                Year = entity.Year,
                Suffix = entity.Suffix,
                RecStatus = entity.RecStatus
            };

            return mapped;
        }

        public static entities.DocumentSeries MapToEntity(entityframework.DocumentSeries documentSeries)
        {
            if (documentSeries == null) return null!;

            return new entities.DocumentSeries
            {
                id = documentSeries.Id,
                BranchId = documentSeries.BranchId,
                DocumentType = documentSeries.DocumentType,
                Prefix = documentSeries.Prefix,
                NextNumber = documentSeries.NextNumber,
                Year = documentSeries.Year,
                Suffix = documentSeries.Suffix,
                RecStatus = documentSeries.RecStatus
            };
        }
    }
}
