using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class StockCountMapper
    {
        public static entityframework.StockCountMaster MapToEntityFramework(entities.StockCountMaster entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.StockCountMaster
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                Scmno = entity.Scmno ?? string.Empty,
                Date = entity.Date,
                WarehouseCode = entity.WarehouseCode,
                PreparedBy = entity.PreparedBy,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.StockCountMaster MapToEntity(entityframework.StockCountMaster stockCountMaster)
        {
            if (stockCountMaster == null) return null!;

            return new entities.StockCountMaster
            {
                id = stockCountMaster.Id,
                Scmno = stockCountMaster.Scmno ?? string.Empty,
                Date = stockCountMaster.Date,
                WarehouseCode = stockCountMaster.WarehouseCode,
                PreparedBy = stockCountMaster.PreparedBy,
                RecStatus = stockCountMaster.RecStatus,
            };
        }
    }
}
