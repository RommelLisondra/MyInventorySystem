using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class EmployeeDelivered : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string EmpIdno { get; set; } = null!;
        public virtual string? RecStatus { get; set; }
        public virtual Employee EmpIdnoNavigation { get; set; } = null!;

        public static EmployeeDelivered Create(EmployeeDelivered employeeDelivered, string createdBy)
        {
            //Place your Business logic here
            employeeDelivered.Created_by = createdBy;
            employeeDelivered.Date_created = DateTime.Now;
            return employeeDelivered;
        }

        public static EmployeeDelivered Update(EmployeeDelivered employeeDelivered, string editedBy)
        {
            //Place your Business logic here
            employeeDelivered.Edited_by = editedBy;
            employeeDelivered.Date_edited = DateTime.Now;
            return employeeDelivered;
        }
    }
}
