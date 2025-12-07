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
    internal class ItemSupplierMapper
    {
        public static List<entities.ItemSupplier> MapToEntityList(IEnumerable<entityframework.ItemSupplier> list)
        {
            if (list == null) return new List<entities.ItemSupplier>();

            return list?.Select(p => new entities.ItemSupplier
            {
                id = p.Id,
                ItemDetailCode = p.ItemDetailCode,
                SupplierNo = p.SupplierNo,
                RecStatus = p.RecStatus,
                Unitprice = p.UnitPrice,
                LeadTime = p.LeadTime,
                Terms = p.Terms,
                ItemDetailCodeNavigation = ItemMapper.MapToEntity(p.ItemDetailCodeNavigation),
            }).ToList() ?? new List<entities.ItemSupplier>();
        }
        public static entities.ItemSupplier MapToEntity(entityframework.ItemSupplier entityitem)
        {
            var item = new entities.ItemSupplier
            {
                id = entityitem.Id,
                ItemDetailCode = entityitem.ItemDetailCode,
                SupplierNo = entityitem.SupplierNo,
                RecStatus = entityitem.RecStatus,
                Unitprice = entityitem.UnitPrice,
                LeadTime = entityitem.LeadTime,
                Terms = entityitem.Terms,
                ItemDetailCodeNavigation = ItemMapper.MapToEntity(entityitem.ItemDetailCodeNavigation),
                //SupplierNoNavigation = SalesOrderMapper.MapToEntitySalesOrderDetailList(entityitem.SalesOrderDetailFile),
            };

            return item;
        }

        public static entityframework.ItemSupplier MapToEntityFramework(entities.ItemSupplier entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var mapped = new entityframework.ItemSupplier
            {
                ItemDetailCode = entity.ItemDetailCode,
                SupplierNo = entity.SupplierNo,
                RecStatus = entity.RecStatus,
                UnitPrice = entity.Unitprice,
                LeadTime = entity.LeadTime,
                Terms = entity.Terms,
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }
    }
}
