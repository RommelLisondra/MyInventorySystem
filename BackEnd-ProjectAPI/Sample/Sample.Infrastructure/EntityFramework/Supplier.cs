using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Supplier
    {
        public Supplier()
        {
            ItemSupplier = new HashSet<ItemSupplier>();
            PurchaseOrderMasterFile = new HashSet<PurchaseOrderMasterFile>();
            PurchaseReturnMasterFile = new HashSet<PurchaseReturnMasterFile>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ContactPerson { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? RecStatus { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? FaxNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PostalCode { get; set; }
        public string? Notes { get; set; }
        public string? ContactNo { get; set; }
        public string? LastPono { get; set; }

        public virtual ICollection<ItemSupplier> ItemSupplier { get; set; }
        public virtual ICollection<PurchaseOrderMasterFile> PurchaseOrderMasterFile { get; set; }
        public virtual ICollection<PurchaseReturnMasterFile> PurchaseReturnMasterFile { get; set; }
    }
}
