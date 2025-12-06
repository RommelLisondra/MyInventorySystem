using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class SupplierImage : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string SupplierNo { get; set; } = null!;
        public virtual string? FilePath { get; set; }

        public virtual Supplier SupNoNavigation { get; set; } = null!;

        public static SupplierImage Create(SupplierImage supplierImage, string createdBy)
        {
            //Place your Business logic here
            supplierImage.Created_by = createdBy;
            supplierImage.Date_created = DateTime.Now;
            return supplierImage;
        }

        public static SupplierImage Update(SupplierImage supplierImage, string editedBy)
        {
            //Place your Business logic here
            supplierImage.Edited_by = editedBy;
            supplierImage.Date_edited = DateTime.Now;
            return supplierImage;
        }
    }
}
