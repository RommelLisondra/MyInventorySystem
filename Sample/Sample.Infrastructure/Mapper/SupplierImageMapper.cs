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
    internal class SupplierImageMapper
    {
        public static entities.SupplierImage MapToEntity(entityframework.SupplierImage entity)
        {
            var item = new entities.SupplierImage
            {
                id = entity.Id,
                SupplierNo = entity.SupplierNo,
                FilePath = entity.FilePath,
                SupNoNavigation = SupplierMapper.MapToEntity(entity.SupplierNoNavigation)
            };

            return item;
        }

        public static entityframework.SupplierImage MapToEntityFramework(entities.SupplierImage entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var mapped = new entityframework.SupplierImage
            {
                SupplierNo = entity.SupplierNo,
                FilePath = entity.FilePath
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }
    }
}
