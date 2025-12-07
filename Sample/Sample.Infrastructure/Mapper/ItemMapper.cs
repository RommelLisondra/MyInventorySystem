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
    internal class ItemMapper
    {
        public static entities.ItemDetail MapToEntity(entityframework.ItemDetail entityitem)
        {
            var item = new entities.ItemDetail
            {
                id = entityitem.Id,
                ItemDetailCode = entityitem.ItemDetailCode,
                ItemId = entityitem.ItemId,
                ItemDetailNo = entityitem.ItemDetailNo,
                Barcode = entityitem.Barcode,
                SerialNo = entityitem.SerialNo,
                PartNo = entityitem.PartNo,
                WarehouseCode = entityitem.WarehouseCode,
                LocationCode = entityitem.LocationCode,
                Volume = entityitem.Volume,
                Size = entityitem.Size,
                Weight = entityitem.Weight,
                ExpiryDate = entityitem.ExpiryDate,
                UnitMeasure = entityitem.UnitMeasure,
                Unitprice = entityitem.Unitprice,
                Warranty = entityitem.Warranty,
                ModifiedDateTime = entityitem.ModifiedDateTime,
                CreatedDateTime = entityitem.CreatedDateTime,
                Eoq = entityitem.Eoq,
                Rop = entityitem.Rop,
                RecStatus = entityitem.RecStatus,
                ItemMaster = entityitem.ItemMaster != null
                                    ? MapToItem(entityitem.ItemMaster)
                                    : throw new ArgumentNullException(nameof(entityitem.ItemMaster), "Item cannot be null"),
                ItemInventory = entityitem.ItemInventory != null
                        ? MapToEntityIteInvetoryList(entityitem.ItemInventory)
                        : null,
            };

            return item;
        }

        public static List<entities.ItemDetail> MapToEntityItemDetailList(IEnumerable<entityframework.ItemDetail> list)
        {
            if (list == null) return new List<entities.ItemDetail>();

            return list.Select(a => new entities.ItemDetail
            {
                id = a.Id,
                ItemDetailCode = a.ItemDetailCode,
                ItemId = a.ItemId,
                ItemDetailNo = a.ItemDetailNo,
                Barcode = a.Barcode,
                SerialNo = a.SerialNo,
                PartNo = a.PartNo,
                WarehouseCode = a.WarehouseCode,
                LocationCode = a.LocationCode,
                Volume = a.Volume,
                Size = a.Size,
                Weight = a.Weight,
                ExpiryDate = a.ExpiryDate,
                UnitMeasure = a.UnitMeasure,
                Unitprice = a.Unitprice,
                Warranty = a.Warranty,
                ModifiedDateTime = a.ModifiedDateTime,
                CreatedDateTime = a.CreatedDateTime,
                Eoq = a.Eoq,
                Rop = a.Rop,
                RecStatus = a.RecStatus,
            }).ToList();
        }

        public static List<entities.ItemInventory> MapToEntityIteInvetoryList(IEnumerable<entityframework.ItemInventory> list)
        {
            if (list == null) return new List<entities.ItemInventory>();

            return list.Select(a => new entities.ItemInventory
            {
                id = a.Id,
                ItemDetailCode = a.ItemDetailCode,
                WarehouseCode = a.WarehouseCode,
                LocationCode = a.LocationCode,
                QtyOnHand = a.QtyOnHand,
                QtyOnOrder = a.QtyOnOrder,
                QtyReserved = a.QtyReserved,
                ExtendedQtyOnHand = a.ExtendedQtyOnHand,
                SalesReturnItemQty = a.SalesReturnItemQty,
                PurchaseReturnItemQty = a.PurchaseReturnItemQty,
                LastStockCount = a.LastStockCount,
                RecStatus = a.RecStatus,
            }).ToList();
        }

        public static entities.ItemInventory MapToEntityInventory(entityframework.ItemInventory efInventory)
        {
            if (efInventory == null) return null!; // o throw depende sa business rule

            return new entities.ItemInventory
            {
                id = efInventory.Id,
                ItemDetailCode = efInventory.ItemDetailCode,
                WarehouseCode = efInventory.WarehouseCode,
                LocationCode = efInventory.LocationCode,
                QtyOnHand = efInventory.QtyOnHand,
                QtyOnOrder = efInventory.QtyOnOrder,
                QtyReserved = efInventory.QtyReserved,
                ExtendedQtyOnHand = efInventory.ExtendedQtyOnHand,
                SalesReturnItemQty = efInventory.SalesReturnItemQty,
                PurchaseReturnItemQty = efInventory.PurchaseReturnItemQty,
                LastStockCount = efInventory.LastStockCount,
                RecStatus = efInventory.RecStatus,
            };
        }

        public static entityframework.ItemInventory MapToEntityFramework(entities.ItemInventory entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Inventory entity cannot be null.");

            var mapped = new entityframework.ItemInventory
            {
                ItemDetailCode = entity.ItemDetailCode,
                WarehouseCode = entity.WarehouseCode,
                LocationCode = entity.LocationCode,
                QtyOnHand = entity.QtyOnHand,
                QtyOnOrder = entity.QtyOnOrder,
                QtyReserved = entity.QtyReserved,
                ExtendedQtyOnHand = entity.ExtendedQtyOnHand,
                SalesReturnItemQty = entity.SalesReturnItemQty,
                PurchaseReturnItemQty = entity.PurchaseReturnItemQty,
                LastStockCount = entity.LastStockCount,
                RecStatus = entity.RecStatus,
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }

        public static entityframework.ItemDetail MapToEntityFramework(entities.ItemDetail entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var mapped = new entityframework.ItemDetail
            {
                ItemDetailCode = entity.ItemDetailCode,
                ItemId = entity.ItemId,
                ItemDetailNo = entity.ItemDetailNo,
                Barcode = entity.Barcode,
                SerialNo = entity.SerialNo,
                PartNo = entity.PartNo,
                WarehouseCode = entity.WarehouseCode,
                LocationCode = entity.LocationCode,
                Volume = entity.Volume,
                Size = entity.Size,
                Weight = entity.Weight,
                ExpiryDate = entity.ExpiryDate,
                UnitMeasure = entity.UnitMeasure,
                Unitprice = entity.Unitprice,
                Warranty = entity.Warranty,
                ModifiedDateTime = entity.ModifiedDateTime,
                CreatedDateTime = entity.CreatedDateTime,
                Eoq = entity.Eoq,
                Rop = entity.Rop,
                RecStatus = entity.RecStatus
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }

        public static entityframework.ItemImage MapToEntityFramework(entities.ItemImage entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemImage entity cannot be null.");

            var mapped = new entityframework.ItemImage
            {
                ItemDetailCode = entity.ItemDetailCode,
                ImagePath = entity.FilePath
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.ItemImage MapToItemImage(entityframework.ItemImage itemImage)
        {
            if (itemImage == null) return null!;

            return new entities.ItemImage
            {
                id = itemImage.Id,
                ItemDetailCode = itemImage.ItemDetailCode,
                FilePath = itemImage.ImagePath,
                ItemDetailCodeNavigation = itemImage.ItemDetailCodeNavigation != null
                                    ? MapToEntity(itemImage.ItemDetailCodeNavigation)
                                    : throw new ArgumentNullException(nameof(itemImage.ItemDetailCodeNavigation), "ItemDetailCodeNavigation cannot be null")
            };
        }

        public static entityframework.Item MapToEntityFramework(entities.Item entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item entity cannot be null.");

            var mapped = new entityframework.Item
            {
                ItemCode = entity.ItemCode,
                ItemName = entity.ItemName,
                Desc = entity.Desc,
                BrandId = entity.BrandId,
                Model = entity.Model,
                CategoryId = entity.CategoryId,
                ClassificationId = entity.ClassificationId,
                CreatedDateTime = entity.CreatedDateTime,
                ModifiedDateTime = entity.ModifiedDateTime,
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.Item MapToItem(entityframework.Item item)
        {
            if (item == null) return null!;

            return new entities.Item
            {
                id = item.Id,
                ItemCode = item.ItemCode,
                ItemName = item.ItemName,
                Desc = item.Desc,
                BrandId = item.BrandId,
                Model = item.Model,
                CategoryId = item.CategoryId,
                ClassificationId = item.ClassificationId,
                CreatedDateTime = item.CreatedDateTime,
                ModifiedDateTime = item.ModifiedDateTime,
                Brand = BrandMapper.MapToEntity(item.Brand),
                Category = CategoryMapper.MapToEntity(item.Category),
                ItemDetails = item.ItemDetails != null
                        ? MapToEntityItemDetailList(item.ItemDetails)
                        : null,
            };
        }
    }
}
