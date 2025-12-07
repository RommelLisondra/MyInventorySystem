using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }      // FK to Category
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Property
        public virtual Category Category { get; set; } = null!;
    }
}
