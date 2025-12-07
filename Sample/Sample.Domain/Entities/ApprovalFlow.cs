using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class ApprovalFlow : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ModuleName { get; set; } = null!;
        public virtual int Level { get; set; }
        public virtual int ApproverId { get; set; }
        public virtual bool IsFinalLevel { get; set; }
        public virtual string? RecStatus { get; set; }
        public string? ApprovalId { get; set; }

        public static ApprovalFlow Create(ApprovalFlow approval, string createdBy)
        {
            //Place your Business logic here
            approval.Created_by = createdBy;
            approval.Date_created = DateTime.Now;
            return approval;
        }

        public static ApprovalFlow Update(ApprovalFlow approval, string editedBy)
        {
            //Place your Business logic here
            approval.Edited_by = editedBy;
            approval.Date_edited = DateTime.Now;
            return approval;
        }
    }
}
