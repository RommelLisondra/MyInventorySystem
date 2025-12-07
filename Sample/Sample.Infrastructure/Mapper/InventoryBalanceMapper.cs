using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class InventoryBalanceMapper
    {
        public static entityframework.InventoryBalance MapToEntityFramework(entities.InventoryBalance entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryBalance entity cannot be null.");

            var mapped = new entityframework.InventoryBalance
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ItemDetailNo = entity.ItemDetailNo ?? string.Empty,
                WarehouseId = entity.WarehouseId,
                QuantityOnHand = entity.QuantityOnHand,
                LastUpdated = entity.LastUpdated,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.InventoryBalance MapToEntity(entityframework.InventoryBalance inventoryBalance)
        {
            if (inventoryBalance == null) return null!;

            return new entities.InventoryBalance
            {
                id = inventoryBalance.Id,
                ItemDetailNo = inventoryBalance.ItemDetailNo ?? string.Empty,
                WarehouseId = inventoryBalance.WarehouseId,
                QuantityOnHand = inventoryBalance.QuantityOnHand,
                LastUpdated = inventoryBalance.LastUpdated,
                RecStatus = inventoryBalance.RecStatus
            };
        }
    }
}
