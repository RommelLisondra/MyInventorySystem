using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ItemUnitMeasure : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual string UnitCode { get; set; } = null!;
        public virtual decimal? ConversionRate { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;

        public static ItemUnitMeasure Create(ItemUnitMeasure itemUnitMeasure, string createdBy)
        {
            //Place your Business logic here
            itemUnitMeasure.Created_by = createdBy;
            itemUnitMeasure.Date_created = DateTime.Now;
            return itemUnitMeasure;
        }

        public static ItemUnitMeasure Update(ItemUnitMeasure itemUnitMeasure, string editedBy)
        {
            //Place your Business logic here
            itemUnitMeasure.Edited_by = editedBy;
            itemUnitMeasure.Date_edited = DateTime.Now;
            return itemUnitMeasure;
        }
    }
}
