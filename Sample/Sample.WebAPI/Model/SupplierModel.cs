using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class SupplierModel 
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

        public PurchaseOrderMasterModel? LastPonoNavigation { get; set; }
        public ICollection<ItemDetailModel>? ItemDetails { get; set; }
        public ICollection<ItemSupplierModel>? ItemSuppliers { get; set; }
        public ICollection<PurchaseOrderMasterModel>? PurchaseOrderMasterFiles { get; set; }
        public ICollection<PurchaseReturnMasterModel>? PurchaseReturnMasterFiles { get; set; }
        public ICollection<ReceivingReportMasterModel>? ReceivingReportMasterFiles { get; set; }
        public ICollection<SupplierImageModel>? SupplerImages { get; set; }
    }
}
