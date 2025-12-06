using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class EmployeeChecker : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string EmpIdno { get; set; } = null!;
        public virtual string? RecStatus { get; set; }

        public virtual Employee EmpIdnoNavigation { get; set; } = null!;

        public static EmployeeChecker Create(EmployeeChecker employeeChecker, string createdBy)
        {
            //Place your Business logic here
            employeeChecker.Created_by = createdBy;
            employeeChecker.Date_created = DateTime.Now;
            return employeeChecker;
        }

        public static EmployeeChecker Update(EmployeeChecker employeeChecker, string editedBy)
        {
            //Place your Business logic here
            employeeChecker.Edited_by = editedBy;
            employeeChecker.Date_edited = DateTime.Now;
            return employeeChecker;
        }
    }
}
