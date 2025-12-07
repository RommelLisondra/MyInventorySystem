using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Classification : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();

        public static Classification Create(Classification classification, string createdBy)
        {
            //Place your Business logic here
            classification.Created_by = createdBy;
            classification.Date_created = DateTime.Now;
            return classification;
        }

        public static Classification Update(Classification classification, string editedBy)
        {
            //Place your Business logic here
            classification.Edited_by = editedBy;
            classification.Date_edited = DateTime.Now;
            return classification;
        }
    }
}
