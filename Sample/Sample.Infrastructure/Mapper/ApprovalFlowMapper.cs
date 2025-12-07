using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ApprovalFlowMapper
    {
        public static entityframework.ApprovalFlow MapToEntityFramework(entities.ApprovalFlow entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.ApprovalFlow
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ModuleName = entity.ModuleName ?? string.Empty,
                Level = entity.Level,
                ApproverId = entity.ApproverId,
                IsFinalLevel = entity.IsFinalLevel,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.ApprovalFlow MapToEntity(entityframework.ApprovalFlow customer)
        {
            if (customer == null) return null!;

            return new entities.ApprovalFlow
            {
                id = customer.Id,
                ModuleName = customer.ModuleName ?? string.Empty,
                Level = customer.Level,
                ApproverId = customer.ApproverId,
                IsFinalLevel = customer.IsFinalLevel,
                RecStatus = customer.RecStatus,
            };
        }
    }
}
