using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string? CustNo { get; set; }
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
        public string? LastSono { get; set; }
        public string? LastSino { get; set; }
        public string? LastDrno { get; set; }
        public string? LastOr { get; set; }
        public string? LastSrno { get; set; }
        public string? RecStatus { get; set; }

        public DeliveryReceiptMasterModel? LastDrnoNavigation { get; set; }
        public OfficialReceiptMasterModel? LastOrNavigation { get; set; }
        public SalesInvoiceMasterModel? LastSinoNavigation { get; set; }
        public SalesOrderMasterModel? LastSonoNavigation { get; set; }
        public SalesReturnMasterModel? LastSrnoNavigation { get; set; }
        public ICollection<DeliveryReceiptMasterModel>? DeliveryReceiptMaster { get; set; }
        public ICollection<OfficialReceiptMasterModel>? OfficialReceiptMaster { get; set; }
        public ICollection<SalesInvoiceMasterModel>? SalesInvoiceMaster { get; set; }
        public ICollection<SalesOrderMasterModel>? SalesOrderMaster { get; set; }
        public ICollection<SalesReturnMasterModel>? SalesReturnMaster { get; set; }
    }
}
