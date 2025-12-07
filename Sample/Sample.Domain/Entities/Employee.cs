using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class Employee : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual string? EmpId { get; set; }
        public virtual string EmpIdno { get; set; } = null!;
        public virtual string? LastName { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? MiddleName { get; set; }
        public virtual string? Address { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual int? Age { get; set; }
        public virtual string? Gender { get; set; }
        public virtual string? Mstatus { get; set; }
        public virtual string? Religion { get; set; }
        public virtual string? EduAttentment { get; set; }
        public virtual DateTime? DateHired { get; set; }
        public virtual string? Department { get; set; }
        public virtual string? Position { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? PostalCode { get; set; }
        public virtual string? Country { get; set; }
        public virtual string? State { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? Fax { get; set; }
        public virtual string? MobileNo { get; set; }
        public virtual string? City { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual EmployeeApprover? EmployeeApprover { get; set; }
        public virtual EmployeeChecker? EmployeeChecker { get; set; }
        public virtual EmployeeDelivered? EmployeeDelivered { get; set; }
        public virtual EmployeeImage? EmployeeImage { get; set; }
        public virtual EmployeeSalesRef? EmployeeSalesRef { get; set; }
        public virtual PurchaseOrderMaster? PurchaseOrderMasterFileApprByNavigation { get; set; }
        public virtual PurchaseOrderMaster? PurchaseOrderMasterFilePrepByNavigation { get; set; }
        public virtual PurchaseRequisitionMaster? PurchaseRequisitionMasterFileApprByNavigation { get; set; }
        public virtual PurchaseRequisitionMaster? PurchaseRequisitionMasterFilePreparedByNavigation { get; set; }
        public virtual PurchaseReturnMaster? PurchaseReturnMasterFileApprByNavigation { get; set; }
        public virtual PurchaseReturnMaster? PurchaseReturnMasterFilePrepByNavigation { get; set; }
        public virtual ReceivingReportMaster? ReceivingReportMasterFile { get; set; }
        public virtual SalesOrderMaster? SalesOrderMasterFileApprByNavigation { get; set; }
        public virtual SalesOrderMaster? SalesOrderMasterFilePrepByNavigation { get; set; }
        public virtual ICollection<DeliveryReceiptMaster>? DeliveryReceiptMaster { get; set; }
        public virtual ICollection<DeliveryReceiptMaster>? DeliveryReceiptMasterFileApprByNavigations { get; set; }
        public virtual ICollection<DeliveryReceiptMaster>? DeliveryReceiptMasterFilePrepByNavigations { get; set; }
        public virtual ICollection<OfficialReceiptMaster>? OfficialReceiptMasterFiles { get; set; }
        public virtual ICollection<SalesInvoiceMaster>? SalesInvoiceMasterFileApprByNavigations { get; set; }
        public virtual ICollection<SalesInvoiceMaster>? SalesInvoiceMasterFilePrepByNavigations { get; set; }
        public virtual ICollection<SalesReturnMaster>? SalesReturnMasterFileApprByNavigations { get; set; }
        public virtual ICollection<SalesReturnMaster>? SalesReturnMasterFilePrepByNavigations { get; set; }

        public static Employee Create(Employee employee, string createdBy)
        {
            //Place your Business logic here
            employee.Created_by = createdBy;
            employee.Date_created = DateTime.Now;
            return employee;
        }

        public static Employee Update(Employee employee, string editedBy)
        {
            //Place your Business logic here
            employee.Edited_by = editedBy;
            employee.Date_edited = DateTime.Now;
            return employee;
        }
    }
}
