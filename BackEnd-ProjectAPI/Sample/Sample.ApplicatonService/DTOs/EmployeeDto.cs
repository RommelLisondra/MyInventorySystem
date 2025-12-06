using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class EmployeeDto
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

        public EmployeeApproverDto? EmployeeApprover { get; set; }
        public EmployeeCheckerDto? EmployeeChecker { get; set; }
        public EmployeeDeliveredDto? EmployeeDelivered { get; set; }
        public EmployeeImageDto? EmployeeImage { get; set; }
        public EmployeeSalesRefDto? EmployeeSalesRef { get; set; }
        public PurchaseOrderMasterDto? PurchaseOrderMasterFileApprByNavigation { get; set; }
        public PurchaseOrderMasterDto? PurchaseOrderMasterFilePrepByNavigation { get; set; }
        public PurchaseRequisitionMasterDto? PurchaseRequisitionMasterFileApprByNavigation { get; set; }
        public PurchaseRequisitionMasterDto? PurchaseRequisitionMasterFilePreparedByNavigation { get; set; }
        public PurchaseReturnMasterDto? PurchaseReturnMasterFileApprByNavigation { get; set; }
        public PurchaseReturnMasterDto? PurchaseReturnMasterFilePrepByNavigation { get; set; }
        public ReceivingReportMasterDto? ReceivingReportMasterFile { get; set; }
        public SalesOrderMasterDto? SalesOrderMasterFileApprByNavigation { get; set; }
        public SalesOrderMasterDto? SalesOrderMasterFilePrepByNavigation { get; set; }
        public ICollection<DeliveryReceiptMasterDto>? DeliveryReceiptMaster { get; set; }
        public ICollection<DeliveryReceiptMasterDto>? DeliveryReceiptMasterFileApprByNavigations { get; set; }
        public ICollection<DeliveryReceiptMasterDto>? DeliveryReceiptMasterFilePrepByNavigations { get; set; }
        public ICollection<OfficialReceiptMasterDto>? OfficialReceiptMasterFiles { get; set; }
        public ICollection<SalesInvoiceMasterDto>? SalesInvoiceMasterFileApprByNavigations { get; set; }
        public ICollection<SalesInvoiceMasterDto>? SalesInvoiceMasterFilePrepByNavigations { get; set; }
        public ICollection<SalesReturnMasterDto>? SalesReturnMasterFileApprByNavigations { get; set; }
        public ICollection<SalesReturnMasterDto>? SalesReturnMasterFilePrepByNavigations { get; set; }
    }
}
