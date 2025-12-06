using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Company : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string CompanyCode { get; set; } = null!;
        public virtual string CompanyName { get; set; } = null!;
        public virtual string? Address { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

        public static Company Create(Company company, string createdBy)
        {
            //Place your Business logic here
            company.Created_by = createdBy;
            company.Date_created = DateTime.Now;
            return company;
        }

        public static Company Update(Company company, string editedBy)
        {
            //Place your Business logic here
            company.Edited_by = editedBy;
            company.Date_edited = DateTime.Now;
            return company;
        }
    }
}
