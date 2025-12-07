using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ItemWarehouseMappingMapper
    {
        public static entityframework.ItemWarehouseMapping MapToEntityFramework(entities.ItemWarehouseMapping entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Account entity cannot be null.");

            var mapped = new entityframework.ItemWarehouseMapping
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ItemId = entity.ItemId,
                WarehouseId = entity.WarehouseId,
                BranchId = entity.BranchId,
                IsActive = entity.IsActive,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
                //Item
                //Warehouse
                //Branch
            };

            return mapped;
        }

        public static entities.ItemWarehouseMapping MapToEntity(entityframework.ItemWarehouseMapping itemWarehouse)
        {
            if (itemWarehouse == null) return null!;

            return new entities.ItemWarehouseMapping
            {
                id = itemWarehouse.Id,         // default = 0 kung int
                ItemId = itemWarehouse.ItemId,
                WarehouseId = itemWarehouse.WarehouseId,
                BranchId = itemWarehouse.BranchId,
                IsActive = itemWarehouse.IsActive,
                CreatedDateTime = itemWarehouse.CreatedDateTime,
                ModifiedDateTime = itemWarehouse.ModifiedDateTime,
                //Item
                //Warehouse
                //Branch
            };
        }
    }
}
