using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ItemImage : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual string? FilePath { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;

        public static ItemImage Create(ItemImage itemImage, string createdBy)
        {
            //Place your Business logic here
            itemImage.Created_by = createdBy;
            itemImage.Date_created = DateTime.Now;
            return itemImage;
        }

        public static ItemImage Update(ItemImage itemImage, string editedBy)
        {
            //Place your Business logic here
            itemImage.Edited_by = editedBy;
            itemImage.Date_edited = DateTime.Now;
            return itemImage;
        }
    }
}
