using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Mapper
{
    internal class ItemUnitMeasureMapper
    {
        public static entities.ItemUnitMeasure MapToEntity(entityframework.ItemUnitMeasure entityitem)
        {
            var item = new entities.ItemUnitMeasure
            {
                id = entityitem.Id,
                ItemDetailCode = entityitem.ItemDetailCode,
                UnitCode = entityitem.UnitCode,
                RecStatus = entityitem.RecStatus,
                ConversionRate = entityitem.ConversionRate,
                ItemDetailCodeNavigation = ItemMapper.MapToEntity(entityitem.ItemDetailCodeNavigation),
            };

            return item;
        }

        public static entityframework.ItemUnitMeasure MapToEntityFramework(entities.ItemUnitMeasure entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var mapped = new entityframework.ItemUnitMeasure
            {
                ItemDetailCode = entity.ItemDetailCode,
                UnitCode = entity.UnitCode,
                RecStatus = entity.RecStatus,
                ConversionRate = entity.ConversionRate,
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }
    }
}
