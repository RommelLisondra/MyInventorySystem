using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Customer
    {
        public Customer()
        {
            DeliveryReceiptMasterFile = new HashSet<DeliveryReceiptMasterFile>();
            OfficialReceiptMasterFile = new HashSet<OfficialReceiptMasterFile>();
            SalesInvoiceMasterFile = new HashSet<SalesInvoiceMasterFile>();
            SalesOrderMasterFile = new HashSet<SalesOrderMasterFile>();
            SalesReturnMasterFile = new HashSet<SalesReturnMasterFile>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string CustNo { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? EmailAddress { get; set; }
        public string? Fax { get; set; }
        public string? MobileNo { get; set; }
        public string? AcountNo { get; set; }
        public string? CreditCardNo { get; set; }
        public string? CreditCardType { get; set; }
        public string? CreditCardName { get; set; }
        public string? CreditCardExpiry { get; set; }
        public string? ContactNo { get; set; }
        public string? ContactPerson { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? Balance { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? LastSono { get; set; }
        public string? LastSino { get; set; }
        public string? LastDrno { get; set; }
        public string? LastOr { get; set; }
        public string? LastSrno { get; set; }
        public string? RecStatus { get; set; }

        public virtual ICollection<DeliveryReceiptMasterFile> DeliveryReceiptMasterFile { get; set; }
        public virtual ICollection<OfficialReceiptMasterFile> OfficialReceiptMasterFile { get; set; }
        public virtual ICollection<SalesInvoiceMasterFile> SalesInvoiceMasterFile { get; set; }
        public virtual ICollection<SalesOrderMasterFile> SalesOrderMasterFile { get; set; }
        public virtual ICollection<SalesReturnMasterFile> SalesReturnMasterFile { get; set; }
    }
}
