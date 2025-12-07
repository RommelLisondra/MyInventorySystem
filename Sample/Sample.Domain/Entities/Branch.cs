using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Branch : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int CompanyId { get; set; }               // FK → Company
        public virtual string BranchCode { get; set; } = null!;
        public virtual string BranchName { get; set; } = null!;
        public virtual string? Address { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<ItemWarehouseMapping> ItemWarehouseMappings { get; set; } = new List<ItemWarehouseMapping>();

        public static Branch Create(Branch branch, string createdBy)
        {
            //Place your Business logic here
            branch.Created_by = createdBy;
            branch.Date_created = DateTime.Now;
            return branch;
        }

        public static Branch Update(Branch branch, string editedBy)
        {
            //Place your Business logic here
            branch.Edited_by = editedBy;
            branch.Date_edited = DateTime.Now;
            return branch;
        }
    }
}
