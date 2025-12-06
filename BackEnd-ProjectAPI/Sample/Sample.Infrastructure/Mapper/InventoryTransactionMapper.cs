using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class InventoryTransactionMapper
    {
        public static entityframework.InventoryTransaction MapToEntityFramework(entities.InventoryTransaction entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.InventoryTransaction
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                TransactionDate = entity.TransactionDate,
                CompanyId = entity.CompanyId,
                WarehouseId = entity.WarehouseId,
                RefModule = entity.RefModule,
                RefNo = entity.RefNo,
                RefId = entity.RefId,
                Quantity = entity.Quantity,
                UnitCost = entity.UnitCost,
                MovementType = entity.MovementType,
                Remarks = entity.Remarks,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                RecStatus = entity.RecStatus
            };

            return mapped;
        }

        public static entities.InventoryTransaction MapToEntity(entityframework.InventoryTransaction inventoryTransaction)
        {
            if (inventoryTransaction == null) return null!;

            return new entities.InventoryTransaction
            {
                id = inventoryTransaction.Id,
                TransactionDate = inventoryTransaction.TransactionDate,
                CompanyId = inventoryTransaction.CompanyId,
                WarehouseId = inventoryTransaction.WarehouseId,
                RefModule = inventoryTransaction.RefModule,
                RefNo = inventoryTransaction.RefNo,
                RefId = inventoryTransaction.RefId,
                Quantity = inventoryTransaction.Quantity,
                UnitCost = inventoryTransaction.UnitCost,
                MovementType = inventoryTransaction.MovementType,
                Remarks = inventoryTransaction.Remarks,
                CreatedBy = inventoryTransaction.CreatedBy,
                CreatedDate = inventoryTransaction.CreatedDate,
                RecStatus = inventoryTransaction.RecStatus
            };
        }
    }
}
