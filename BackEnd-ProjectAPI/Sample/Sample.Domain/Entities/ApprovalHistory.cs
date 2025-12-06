using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class ApprovalHistory : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int ApprovalId { get; set; }
        public virtual string ModuleName { get; set; } = null!;
        public virtual string RefNo { get; set; } = null!;
        public virtual string? ApprovedBy { get; set; }
        public virtual DateTime? DateApproved { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual Employee? ApprovedByNavigation { get; set; }

        public static ApprovalHistory Create(ApprovalHistory approval, string createdBy)
        {
            //Place your Business logic here
            approval.Created_by = createdBy;
            approval.Date_created = DateTime.Now;
            return approval;
        }

        public static ApprovalHistory Update(ApprovalHistory approval, string editedBy)
        {
            //Place your Business logic here
            approval.Edited_by = editedBy;
            approval.Date_edited = DateTime.Now;
            return approval;
        }
    }
}
