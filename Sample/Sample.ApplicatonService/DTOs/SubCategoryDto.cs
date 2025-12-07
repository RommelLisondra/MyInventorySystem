using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class SubCategoryDto
    {
        public int id { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }      // FK to Category
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Property
        public CategoryDto Category { get; set; } = null!;
    }
}
