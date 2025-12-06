using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class DocumentReferenceMapper
    {
        public static entityframework.DocumentReference MapToEntityFramework(entities.DocumentReference entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.DocumentReference
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                RefNo = entity.RefNo ?? string.Empty,
                ModuleName = entity.ModuleName,
                DateReferenced = entity.DateReferenced,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.DocumentReference MapToEntity(entityframework.DocumentReference documentReference)
        {
            if (documentReference == null) return null!;

            return new entities.DocumentReference
            {
                id = documentReference.Id,
                RefNo = documentReference.RefNo ?? string.Empty,
                ModuleName = documentReference.ModuleName,
                DateReferenced = documentReference.DateReferenced,
                RecStatus = documentReference.RecStatus,
            };
        }
    }
}
