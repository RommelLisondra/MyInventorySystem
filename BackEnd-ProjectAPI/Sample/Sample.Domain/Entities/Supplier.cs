using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class Supplier : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual string SupplierNo { get; set; } = null!;
        public virtual string? Name { get; set; }
        public virtual string? Address { get; set; }
        public virtual string? Address1 { get; set; }
        public virtual string? Address2 { get; set; }
        public virtual string? City { get; set; }
        public virtual string? State { get; set; }
        public virtual string? Country { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? FaxNo { get; set; }
        public virtual string? MobileNo { get; set; }
        public virtual string? PostalCode { get; set; }
        public virtual string? Notes { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? ContactPerson { get; set; }
        public virtual string? RecStatus { get; set; }
        public virtual string? LastPono { get; set; }

        public virtual PurchaseOrderMaster? LastPonoNavigation { get; set; }
        public virtual ICollection<ItemDetail>? ItemDetails { get; set; }
        public virtual ICollection<ItemSupplier>? ItemSuppliers { get; set; }
        public virtual ICollection<PurchaseOrderMaster>? PurchaseOrderMasterFiles { get; set; }
        public virtual ICollection<PurchaseReturnMaster>? PurchaseReturnMasterFiles { get; set; }
        public virtual ICollection<ReceivingReportMaster>? ReceivingReportMasterFiles { get; set; }
        public virtual ICollection<SupplierImage>? SupplerImages { get; set; }

        public static Supplier Create(Supplier supplier, string createdBy)
        {
            //Place your Business logic here
            supplier.Created_by = createdBy;
            supplier.Date_created = DateTime.Now;
            return supplier;
        }

        public static Supplier Update(Supplier supplier, string editedBy)
        {
            //Place your Business logic here
            supplier.Edited_by = editedBy;
            supplier.Date_edited = DateTime.Now;
            return supplier;
        }

        public void UpdateLastPono(string pono)
        {
            if (string.IsNullOrWhiteSpace(pono))
                throw new ArgumentException("PoNO cannot be empty.", nameof(pono));

            LastPono = pono;
        }
    }
}
