using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class EmployeeSalesRef : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string EmpIdno { get; set; } = null!;
        public virtual string? RecStatus { get; set; }
        public virtual Employee EmpIdnoNavigation { get; set; } = null!;

        public static EmployeeSalesRef Create(EmployeeSalesRef employeeSalesRef, string createdBy)
        {
            //Place your Business logic here
            employeeSalesRef.Created_by = createdBy;
            employeeSalesRef.Date_created = DateTime.Now;
            return employeeSalesRef;
        }

        public static EmployeeSalesRef Update(EmployeeSalesRef employeeSalesRef, string editedBy)
        {
            //Place your Business logic here
            employeeSalesRef.Edited_by = editedBy;
            employeeSalesRef.Date_edited = DateTime.Now;
            return employeeSalesRef;
        }
    }
}
