using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class ExpenseCategory : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Code { get; set; } = null!;
        public virtual string Name { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

        public static ExpenseCategory Create(ExpenseCategory expenseCategory, string createdBy)
        {
            //Place your Business logic here
            expenseCategory.Created_by = createdBy;
            expenseCategory.Date_created = DateTime.Now;
            return expenseCategory;
        }

        public static ExpenseCategory Update(ExpenseCategory expenseCategory, string editedBy)
        {
            //Place your Business logic here
            expenseCategory.Edited_by = editedBy;
            expenseCategory.Date_edited = DateTime.Now;
            return expenseCategory;
        }
    }
}
