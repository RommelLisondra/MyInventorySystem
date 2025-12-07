using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Category : EntityBase, IAggregateRoot
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public virtual int id { get; set; }
        public virtual string CategoryName { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual int BrandId { get; set; }         // FK to Brand
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        // Navigation Properties
        public virtual Brand Brand { get; set; } = null!;
        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public static Category Create(Category brand, string createdBy)
        {
            //Place your Business logic here
            brand.Created_by = createdBy;
            brand.Date_created = DateTime.Now;
            return brand;
        }

        public static Category Update(Category brand, string editedBy)
        {
            //Place your Business logic here
            brand.Edited_by = editedBy;
            brand.Date_edited = DateTime.Now;
            return brand;
        }
    }
}
