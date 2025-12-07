using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class ItemBarcodeMapper
    {
        public static entityframework.ItemBarcode MapToEntityFramework(entities.ItemBarcode entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemBarcode entity cannot be null.");

            var mapped = new entityframework.ItemBarcode
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                ItemId = entity.ItemId,
                Barcode = entity.Barcode,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedDateTime = entity.CreatedDateTime
            };

            return mapped;
        }

        public static entities.ItemBarcode MapToEntity(entityframework.ItemBarcode itemBarcode)
        {
            if (itemBarcode == null) return null!;

            return new entities.ItemBarcode
            {
                id = itemBarcode.Id,         // default = 0 kung int
                ItemId = itemBarcode.ItemId,
                Barcode = itemBarcode.Barcode,
                Description = itemBarcode.Description,
                IsActive = itemBarcode.IsActive,
                CreatedDateTime = itemBarcode.CreatedDateTime
            };
        }
    }
}
