using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Brand : EntityBase, IAggregateRoot
    {
        public Brand()
        {
            Categories = new HashSet<Category>();
        }

        public virtual int id { get; set; }
        public virtual string BrandName { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual string? RecStatus { get; set; }

        // Navigation Property → Brand can have multiple Categories
        public virtual ICollection<Category> Categories { get; set; }

        public static Brand Create(Brand brand, string createdBy)
        {
            //Place your Business logic here
            brand.Created_by = createdBy;
            brand.Date_created = DateTime.Now;
            return brand;
        }

        public static Brand Update(Brand brand, string editedBy)
        {
            //Place your Business logic here
            brand.Edited_by = editedBy;
            brand.Date_edited = DateTime.Now;
            return brand;
        }
    }
}
