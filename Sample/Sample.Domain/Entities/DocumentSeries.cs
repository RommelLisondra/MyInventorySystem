using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class DocumentSeries : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int SeriesId { get; set; }
        public virtual int BranchId { get; set; }
        public virtual string DocumentType { get; set; } = null!;
        public virtual string? Prefix { get; set; }
        public virtual int NextNumber { get; set; }
        public virtual int Year { get; set; }
        public virtual string? Suffix { get; set; }
        public virtual string? RecStatus { get; set; }

        public static DocumentSeries Create(DocumentSeries document, string createdBy)
        {
            //Place your Business logic here
            document.Created_by = createdBy;
            document.Date_created = DateTime.Now;
            return document;
        }

        public static DocumentSeries Update(DocumentSeries document, string editedBy)
        {
            //Place your Business logic here
            document.Edited_by = editedBy;
            document.Date_edited = DateTime.Now;
            return document;
        }
    }
}
