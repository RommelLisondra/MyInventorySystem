using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class LocationMapper
    {
        public static entityframework.Location MapToEntityFramework(entities.Location entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Location entity cannot be null.");

            var mapped = new entityframework.Location
            {
                LocationCode = entity.LocationCode ?? string.Empty,
                Name = entity.Name,
                WarehouseCode = entity.WareHouseCode,
                RecStatus = entity.Status,
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.Location MapToEntity(entityframework.Location location)
        {
            if (location == null) return null!;

            return new entities.Location
            {
                id = location.Id,
                Name = location.Name,
                LocationCode = location.LocationCode,
                WareHouseCode = location.WarehouseCode,
                //ModifiedDateTime = customer.,
                //CreatedDateTime = customer.cre,
                Status = location.RecStatus,

                WareHouseCodeNavigation = WarehouseMapper.MapToEntity(location.WarehouseCodeNavigation),
                //ItemDetail = ItemMapper.MapToEntity(location.ite),
            };
        }
    }
}
