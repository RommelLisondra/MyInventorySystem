using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class WarehouseMapper
    {
        public static entities.Warehouse MapToEntity(entityframework.Warehouse entity)
        {
            var item = new entities.Warehouse
            {
                id = entity.Id,
                WareHouseCode = entity.WarehouseCode,
                Name = entity.Name,
                Address = entity.Address,
                Remarks = entity.Remarks,
                Status = entity.RecStatus
            };

            return item;
        }

        public static entityframework.Warehouse MapToEntityFramework(entities.Warehouse entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Warehouse entity cannot be null.");

            var mapped = new entityframework.Warehouse
            {
                WarehouseCode = entity.WareHouseCode,
                Name = entity.Name,
                Address = entity.Address,
                Remarks = entity.Remarks,
                RecStatus = entity.Status,
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }
    }
}
