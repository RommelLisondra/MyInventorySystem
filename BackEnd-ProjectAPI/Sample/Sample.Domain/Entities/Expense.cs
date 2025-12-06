using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Expense : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ExpenseNo { get; set; } = null!;
        public virtual DateTime ExpenseDate { get; set; }
        public virtual int ExpenseCategoryId { get; set; }

        public virtual decimal Amount { get; set; }
        public virtual string? Vendor { get; set; }
        public virtual string? ReferenceNo { get; set; }
        public virtual string? Notes { get; set; }
        public virtual string? AttachmentPath { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; } = null!;

        public static Expense Create(Expense expense, string createdBy)
        {
            //Place your Business logic here
            expense.Created_by = createdBy;
            expense.Date_created = DateTime.Now;
            return expense;
        }

        public static Expense Update(Expense expense, string editedBy)
        {
            //Place your Business logic here
            expense.Edited_by = editedBy;
            expense.Date_edited = DateTime.Now;
            return expense;
        }
    }
}
