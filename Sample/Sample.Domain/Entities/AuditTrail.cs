using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class AuditTrail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string TableName { get; set; } = null!;
        public virtual string PrimaryKey { get; set; } = null!;
        public virtual string Action { get; set; } = null!;
        public virtual string ChangedBy { get; set; } = null!;
        public virtual DateTime ChangedDate { get; set; }
        public virtual string? OldValue { get; set; }
        public virtual string? NewValue { get; set; }

        public static AuditTrail Create(AuditTrail auditTrail, string createdBy)
        {
            //Place your Business logic here
            auditTrail.Created_by = createdBy;
            auditTrail.Date_created = DateTime.Now;
            return auditTrail;
        }

        public static AuditTrail Update(AuditTrail auditTrail, string editedBy)
        {
            //Place your Business logic here
            auditTrail.Edited_by = editedBy;
            auditTrail.Date_edited = DateTime.Now;
            return auditTrail;
        }
    }
}
