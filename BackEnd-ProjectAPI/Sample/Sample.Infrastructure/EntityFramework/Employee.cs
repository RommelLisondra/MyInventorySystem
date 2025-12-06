using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Employee
    {
        public Employee()
        {
            ApprovalHistory = new HashSet<ApprovalHistory>();
            DeliveryReceiptMasterFileApprByNavigation = new HashSet<DeliveryReceiptMasterFile>();
            DeliveryReceiptMasterFilePrepByNavigation = new HashSet<DeliveryReceiptMasterFile>();
            OfficialReceiptMasterFileApprovedByNavigation = new HashSet<OfficialReceiptMasterFile>();
            OfficialReceiptMasterFilePreparedByNavigation = new HashSet<OfficialReceiptMasterFile>();
            PurchaseOrderMasterFileApprovedByNavigation = new HashSet<PurchaseOrderMasterFile>();
            PurchaseOrderMasterFilePreparedByNavigation = new HashSet<PurchaseOrderMasterFile>();
            PurchaseRequisitionMasterFileApprovedByNavigation = new HashSet<PurchaseRequisitionMasterFile>();
            PurchaseRequisitionMasterFileRequestedByNavigation = new HashSet<PurchaseRequisitionMasterFile>();
            PurchaseReturnMasterFileApprovedByNavigation = new HashSet<PurchaseReturnMasterFile>();
            PurchaseReturnMasterFilePreparedByNavigation = new HashSet<PurchaseReturnMasterFile>();
            ReceivingReportMasterFileReceivedByNavigation = new HashSet<ReceivingReportMasterFile>();
            ReceivingReportMasterFileVerifiedByNavigation = new HashSet<ReceivingReportMasterFile>();
            SalesInvoiceMasterFileApprovedByNavigation = new HashSet<SalesInvoiceMasterFile>();
            SalesInvoiceMasterFilePreparedByNavigation = new HashSet<SalesInvoiceMasterFile>();
            SalesOrderMasterFileApprovedByNavigation = new HashSet<SalesOrderMasterFile>();
            SalesOrderMasterFilePreparedByNavigation = new HashSet<SalesOrderMasterFile>();
            SalesReturnMasterFileApprovedByNavigation = new HashSet<SalesReturnMasterFile>();
            SalesReturnMasterFilePreparedByNavigation = new HashSet<SalesReturnMasterFile>();
            StockCountMaster = new HashSet<StockCountMaster>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
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
        // FK → Branch
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Approver? Approver { get; set; }
        public virtual Checker? Checker { get; set; }
        public virtual Deliverer? Deliverer { get; set; }
        public virtual EmployeeImage? EmployeeImage { get; set; }
        public virtual SalesRef? SalesRef { get; set; }
        public virtual ICollection<ApprovalHistory> ApprovalHistory { get; set; }
        public virtual ICollection<DeliveryReceiptMasterFile> DeliveryReceiptMasterFileApprByNavigation { get; set; }
        public virtual ICollection<DeliveryReceiptMasterFile> DeliveryReceiptMasterFilePrepByNavigation { get; set; }
        public virtual ICollection<OfficialReceiptMasterFile> OfficialReceiptMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<OfficialReceiptMasterFile> OfficialReceiptMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<PurchaseOrderMasterFile> PurchaseOrderMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<PurchaseOrderMasterFile> PurchaseOrderMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<PurchaseRequisitionMasterFile> PurchaseRequisitionMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<PurchaseRequisitionMasterFile> PurchaseRequisitionMasterFileRequestedByNavigation { get; set; }
        public virtual ICollection<PurchaseReturnMasterFile> PurchaseReturnMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<PurchaseReturnMasterFile> PurchaseReturnMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<ReceivingReportMasterFile> ReceivingReportMasterFileReceivedByNavigation { get; set; }
        public virtual ICollection<ReceivingReportMasterFile> ReceivingReportMasterFileVerifiedByNavigation { get; set; }
        public virtual ICollection<SalesInvoiceMasterFile> SalesInvoiceMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<SalesInvoiceMasterFile> SalesInvoiceMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<SalesOrderMasterFile> SalesOrderMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<SalesOrderMasterFile> SalesOrderMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<SalesReturnMasterFile> SalesReturnMasterFileApprovedByNavigation { get; set; }
        public virtual ICollection<SalesReturnMasterFile> SalesReturnMasterFilePreparedByNavigation { get; set; }
        public virtual ICollection<StockCountMaster> StockCountMaster { get; set; }
    }
}
