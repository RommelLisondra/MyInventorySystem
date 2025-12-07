using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class EmployeeApprover : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string EmpIdno { get; set; } = null!;

        public virtual Employee EmpIdnoNavigation { get; set; } = null!;

        public static EmployeeApprover Create(EmployeeApprover employeeApprover, string createdBy)
        {
            //Place your Business logic here
            employeeApprover.Created_by = createdBy;
            employeeApprover.Date_created = DateTime.Now;
            return employeeApprover;
        }

        public static EmployeeApprover Update(EmployeeApprover employeeApprover, string editedBy)
        {
            //Place your Business logic here
            employeeApprover.Edited_by = editedBy;
            employeeApprover.Date_edited = DateTime.Now;
            return employeeApprover;
        }
    }
}
