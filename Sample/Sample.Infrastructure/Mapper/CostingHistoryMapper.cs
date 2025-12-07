using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class CostingHistoryMapper
    {
        public static entityframework.CostingHistory MapToEntityFramework(entities.CostingHistory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "CostingHistory entity cannot be null.");

            var mapped = new entityframework.CostingHistory
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ItemId = entity.ItemId,
                Cost = entity.Cost,
                EffectiveDate = entity.EffectiveDate,
                Remarks = entity.Remarks,
                CreatedDateTime = entity.CreatedDateTime,
                BranchId = entity.BranchId,

                //Branch
                //Item
            };

            return mapped;
        }

        public static entities.CostingHistory MapToEntity(entityframework.CostingHistory costingHistory)
        {
            if (costingHistory == null) return null!;

            return new entities.CostingHistory
            {
                id = costingHistory.Id,         // default = 0 kung int
                ItemId = costingHistory.ItemId,
                Cost = costingHistory.Cost,
                EffectiveDate = costingHistory.EffectiveDate,
                Remarks = costingHistory.Remarks,
                CreatedDateTime = costingHistory.CreatedDateTime,
                BranchId = costingHistory.BranchId,

                //Branch
                //Item
            };
        }
    }
}
