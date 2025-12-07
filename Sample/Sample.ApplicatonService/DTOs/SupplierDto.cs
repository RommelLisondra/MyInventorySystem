using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class SupplierDto 
    {
        public int id { get; set; }
        public Guid Guid { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? EmailAddress { get; set; }
        public string? FaxNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PostalCode { get; set; }
        public string? Notes { get; set; }
        public string? ContactNo { get; set; }
        public string? ContactPerson { get; set; }
        public string? RecStatus { get; set; }
        public string? LastPono { get; set; }

        public PurchaseOrderMasterDto? LastPonoNavigation { get; set; }
        public ICollection<ItemDetailDto>? ItemDetails { get; set; }
        public ICollection<ItemSupplierDto>? ItemSuppliers { get; set; }
        public ICollection<PurchaseOrderMasterDto>? PurchaseOrderMasterFiles { get; set; }
        public ICollection<PurchaseReturnMasterDto>? PurchaseReturnMasterFiles { get; set; }
        public ICollection<ReceivingReportMasterDto>? ReceivingReportMasterFiles { get; set; }
        public ICollection<SupplierImageDto>? SupplerImages { get; set; }
    }
}
