using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class InventoryAdjustmentMapper
    {
        public static entityframework.InventoryAdjustment MapToEntityFramework(entities.InventoryAdjustment entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.InventoryAdjustment
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                AdjustmentNo = entity.AdjustmentNo ?? string.Empty,
                AdjustmentDate = entity.AdjustmentDate,
                CompanyId = entity.CompanyId,
                WarehouseId = entity.WarehouseId,
                Remarks = entity.Remarks,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.InventoryAdjustment MapToEntity(entityframework.InventoryAdjustment inventoryAdjustment)
        {
            if (inventoryAdjustment == null) return null!;

            return new entities.InventoryAdjustment
            {
                id = inventoryAdjustment.Id,
                AdjustmentNo = inventoryAdjustment.AdjustmentNo ?? string.Empty,
                AdjustmentDate = inventoryAdjustment.AdjustmentDate,
                CompanyId = inventoryAdjustment.CompanyId,
                WarehouseId = inventoryAdjustment.WarehouseId,
                Remarks = inventoryAdjustment.Remarks,
                CreatedBy = inventoryAdjustment.CreatedBy,
                CreatedDate = inventoryAdjustment.CreatedDate,
                RecStatus = inventoryAdjustment.RecStatus
            };
        }
    }
}
