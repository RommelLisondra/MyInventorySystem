using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class SubCategory : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string SubCategoryName { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual int CategoryId { get; set; }      // FK to Category
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        // Navigation Property
        public virtual Category Category { get; set; } = null!;

        public static SubCategory Create(SubCategory subCategory, string createdBy)
        {
            //Place your Business logic here
            subCategory.Created_by = createdBy;
            subCategory.Date_created = DateTime.Now;
            return subCategory;
        }

        public static SubCategory Update(SubCategory subCategory, string editedBy)
        {
            //Place your Business logic here
            subCategory.Edited_by = editedBy;
            subCategory.Date_edited = DateTime.Now;
            return subCategory;
        }
    }
}
