using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Holiday : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }                     // Primary key
        public virtual string HolidayName { get; set; } = null!; // Name of the holiday (e.g., Christmas)
        public virtual DateTime HolidayDate { get; set; }        // The date of the holiday
        public virtual int? CompanyId { get; set; }             // Optional: if holiday is company-specific
        public virtual int? BranchId { get; set; }              // Optional: if holiday is branch-specific
        public virtual bool IsRecurring { get; set; } = true;   // True if the holiday occurs every year
        public virtual string? Description { get; set; }        // Optional notes
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }

        // Navigation properties
        public virtual Company? Company { get; set; }
        public virtual Branch? Branch { get; set; }

        public static Holiday Create(Holiday holiday, string createdBy)
        {
            //Place your Business logic here
            holiday.Created_by = createdBy;
            holiday.Date_created = DateTime.Now;
            return holiday;
        }

        public static Holiday Update(Holiday holiday, string editedBy)
        {
            //Place your Business logic here
            holiday.Edited_by = editedBy;
            holiday.Date_edited = DateTime.Now;
            return holiday;
        }
    }
}
