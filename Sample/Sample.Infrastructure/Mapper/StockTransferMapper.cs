using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class StockTransferMapper
    {
        public static entityframework.StockTransfer MapToEntityFramework(entities.StockTransfer entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "StockTransfer entity cannot be null.");

            var mapped = new entityframework.StockTransfer
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                TransferNo = entity.TransferNo ?? string.Empty,
                TransferDate = entity.TransferDate,
                CompanyId = entity.CompanyId,
                FromWarehouseId = entity.FromWarehouseId,
                ToWarehouseId = entity.ToWarehouseId,
                Remarks = entity.Remarks,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.StockTransfer MapToEntity(entityframework.StockTransfer stockTransfer)
        {
            if (stockTransfer == null) return null!;

            return new entities.StockTransfer
            {
                id = stockTransfer.Id,
                TransferNo = stockTransfer.TransferNo ?? string.Empty,
                TransferDate = stockTransfer.TransferDate,
                CompanyId = stockTransfer.CompanyId,
                FromWarehouseId = stockTransfer.FromWarehouseId,
                ToWarehouseId = stockTransfer.ToWarehouseId,
                Remarks = stockTransfer.Remarks,
                CreatedBy = stockTransfer.CreatedBy,
                CreatedDate = stockTransfer.CreatedDate,
                RecStatus = stockTransfer.RecStatus,
            };
        }
    }
}
