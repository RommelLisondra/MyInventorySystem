using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Brand
    {
        public Brand()
        {
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string BrandName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Property → Brand can have multiple Categories
        public virtual ICollection<Category> Categories { get; set; }
    }
}
