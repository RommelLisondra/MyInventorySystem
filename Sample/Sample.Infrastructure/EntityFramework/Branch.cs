using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Branch
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }               // FK → Company
        public string BranchCode { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<ItemWarehouseMapping> ItemWarehouseMappings { get; set; } = new List<ItemWarehouseMapping>();
    }
}
