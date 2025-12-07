using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public int BrandId { get; set; }         // FK to Brand
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Properties
        public virtual Brand Brand { get; set; } = null!;
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
