using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class Location : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string LocationCode { get; set; } = null!;
        public virtual string WareHouseCode { get; set; } = null!;
        public virtual string? Name { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string? Status { get; set; }

        public virtual Warehouse WareHouseCodeNavigation { get; set; } = null!;
        public virtual ItemDetail? ItemDetail { get; set; }

        public static Location Create(Location location, string createdBy)
        {
            //Place your Business logic here
            location.Created_by = createdBy;
            location.Date_created = DateTime.Now;
            return location;
        }

        public static Location Update(Location location, string editedBy)
        {
            //Place your Business logic here
            location.Edited_by = editedBy;
            location.Date_edited = DateTime.Now;
            return location;
        }
    }
}
