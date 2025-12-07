using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class Warehouse : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string WareHouseCode { get; set; } = null!;
        public virtual string? Name { get; set; }
        public virtual string? Address { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string? Status { get; set; }

        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual Location? Location { get; set; }

        public static Warehouse Create(Warehouse warehouse, string createdBy)
        {
            //Place your Business logic here
            warehouse.Created_by = createdBy;
            warehouse.Date_created = DateTime.Now;
            return warehouse;
        }

        public static Warehouse Update(Warehouse warehouse, string editedBy)
        {
            //Place your Business logic here
            warehouse.Edited_by = editedBy;
            warehouse.Date_edited = DateTime.Now;
            return warehouse;
        }
    }
}
