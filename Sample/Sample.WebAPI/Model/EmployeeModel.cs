using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string? EmpId { get; set; }
        public string EmpIdno { get; set; } = null!;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Mstatus { get; set; }
        public string? Religion { get; set; }
        public string? EduAttentment { get; set; }
        public DateTime? DateHired { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? ContactNo { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? EmailAddress { get; set; }
        public string? Fax { get; set; }
        public string? MobileNo { get; set; }
        public string? City { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public EmployeeApproverModel? EmployeeApprover { get; set; }
        public EmployeeCheckerModel? EmployeeChecker { get; set; }
        public EmployeeDeliveredModel? EmployeeDelivered { get; set; }
        public EmployeeImageModel? EmployeeImage { get; set; }
        public EmployeeSalesRefModel? EmployeeSalesRef { get; set; }
        public PurchaseOrderMasterModel? PurchaseOrderMasterFileApprByNavigation { get; set; }
        public PurchaseOrderMasterModel? PurchaseOrderMasterFilePrepByNavigation { get; set; }
        public PurchaseRequisitionMasterModel? PurchaseRequisitionMasterFileApprByNavigation { get; set; }
        public PurchaseRequisitionMasterModel? PurchaseRequisitionMasterFilePreparedByNavigation { get; set; }
        public PurchaseReturnMasterModel? PurchaseReturnMasterFileApprByNavigation { get; set; }
        public PurchaseReturnMasterModel? PurchaseReturnMasterFilePrepByNavigation { get; set; }
        public ReceivingReportMasterModel? ReceivingReportMasterFile { get; set; }
        public SalesOrderMasterModel? SalesOrderMasterFileApprByNavigation { get; set; }
        public SalesOrderMasterModel? SalesOrderMasterFilePrepByNavigation { get; set; }
        public ICollection<DeliveryReceiptMasterModel>? DeliveryReceiptMaster { get; set; }
        public ICollection<DeliveryReceiptMasterModel>? DeliveryReceiptMasterFileApprByNavigations { get; set; }
        public ICollection<DeliveryReceiptMasterModel>? DeliveryReceiptMasterFilePrepByNavigations { get; set; }
        public ICollection<OfficialReceiptMasterModel>? OfficialReceiptMasterFiles { get; set; }
        public ICollection<SalesInvoiceMasterModel>? SalesInvoiceMasterFileApprByNavigations { get; set; }
        public ICollection<SalesInvoiceMasterModel>? SalesInvoiceMasterFilePrepByNavigations { get; set; }
        public ICollection<SalesReturnMasterModel>? SalesReturnMasterFileApprByNavigations { get; set; }
        public ICollection<SalesReturnMasterModel>? SalesReturnMasterFilePrepByNavigations { get; set; }
    }
}
