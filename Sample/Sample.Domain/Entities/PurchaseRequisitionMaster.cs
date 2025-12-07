using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class PurchaseRequisitionMaster : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Prmno { get; set; } = null!;
        public virtual DateTime? DateRequest { get; set; }
        public virtual DateTime? DateNeeded { get; set; }
        public virtual string RequestedBy { get; set; } = null!;
        public virtual string ApprovedBy { get; set; } = null!;
        public virtual string? Remarks { get; set; }
        public virtual string? Comments { get; set; }
        public virtual string? TermsAndCondition { get; set; }
        public virtual string? FooterText { get; set; }
        public virtual string? Recuring { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee ApprByNavigation { get; set; } = null!;
        public virtual Employee PreparedByNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseRequisitionDetail>? PurchaseRequisitionDetailFile { get; set; }

        public static PurchaseRequisitionMaster Create(PurchaseRequisitionMaster requisition, string createdBy)
        {
            //Place your Business logic here
            requisition.Created_by = createdBy;
            requisition.Date_created = DateTime.Now;
            return requisition;
        }

        public static PurchaseRequisitionMaster Update(PurchaseRequisitionMaster requisition, string editedBy)
        {
            //Place your Business logic here
            requisition.Edited_by = editedBy;
            requisition.Date_edited = DateTime.Now;
            return requisition;
        } 
    }
}
