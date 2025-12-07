using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ItemPriceHistoryMapper
    {
        public static entityframework.ItemPriceHistory MapToEntityFramework(entities.ItemPriceHistory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Account entity cannot be null.");

            var mapped = new entityframework.ItemPriceHistory
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ItemId = entity.ItemId,
                Price = entity.Price,
                EffectiveDate = entity.EffectiveDate,
                Remarks = entity.Remarks,
                CreatedDateTime = entity.CreatedDateTime,
                BranchId = entity.BranchId,
                //Branch
                //Item
            };

            return mapped;
        }

        public static entities.ItemPriceHistory MapToEntity(entityframework.ItemPriceHistory itemPrice)
        {
            if (itemPrice == null) return null!;

            return new entities.ItemPriceHistory
            {
                id = itemPrice.Id,         // default = 0 kung int
                ItemId = itemPrice.ItemId,
                Price = itemPrice.Price,
                EffectiveDate = itemPrice.EffectiveDate,
                Remarks = itemPrice.Remarks,
                CreatedDateTime = itemPrice.CreatedDateTime,
                BranchId = itemPrice.BranchId,
                //Branch
                //Item
            };
        }
    }
}
