using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ApprovalHistoryMapper
    {
        public static entityframework.ApprovalHistory MapToEntityFramework(entities.ApprovalHistory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.ApprovalHistory
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ModuleName = entity.ModuleName ?? string.Empty,
                RefNo = entity.RefNo,
                ApprovedBy = entity.ApprovedBy,
                DateApproved = entity.DateApproved,
                Remarks = entity.Remarks,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.ApprovalHistory MapToEntity(entityframework.ApprovalHistory approvalHistory)
        {
            if (approvalHistory == null) return null!;

            return new entities.ApprovalHistory
            {
                id = approvalHistory.Id,
                ModuleName = approvalHistory.ModuleName ?? string.Empty,
                RefNo = approvalHistory.RefNo,
                ApprovedBy = approvalHistory.ApprovedBy,
                DateApproved = approvalHistory.DateApproved,
                Remarks = approvalHistory.Remarks,
                RecStatus = approvalHistory.RecStatus,
            };
        }
    }
}
