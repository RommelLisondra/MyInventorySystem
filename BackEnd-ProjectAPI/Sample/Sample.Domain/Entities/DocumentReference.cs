using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class DocumentReference : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int DocRefId { get; set; }
        public virtual string RefNo { get; set; } = null!;
        public virtual string ModuleName { get; set; } = null!;
        public virtual DateTime? DateReferenced { get; set; }
        public virtual string? RecStatus { get; set; }

        public static DocumentReference Create(DocumentReference document, string createdBy)
        {
            //Place your Business logic here
            document.Created_by = createdBy;
            document.Date_created = DateTime.Now;
            return document;
        }

        public static DocumentReference Update(DocumentReference document, string editedBy)
        {
            //Place your Business logic here
            document.Edited_by = editedBy;
            document.Date_edited = DateTime.Now;
            return document;
        }
    }
}
