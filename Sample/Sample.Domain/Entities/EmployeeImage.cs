using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class EmployeeImage : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string EmpIdno { get; set; } = null!;
        public virtual string? FilePath { get; set; }

        public virtual Employee EmpIdnoNavigation { get; set; } = null!;

        public static EmployeeImage Create(EmployeeImage employeeImage, string createdBy)
        {
            //Place your Business logic here
            employeeImage.Created_by = createdBy;
            employeeImage.Date_created = DateTime.Now;
            return employeeImage;
        }

        public static EmployeeImage Update(EmployeeImage employeeImage, string editedBy)
        {
            //Place your Business logic here
            employeeImage.Edited_by = editedBy;
            employeeImage.Date_edited = DateTime.Now;
            return employeeImage;
        }
    }
}
