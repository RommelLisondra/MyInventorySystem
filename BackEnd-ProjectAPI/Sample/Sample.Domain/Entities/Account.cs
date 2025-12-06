using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class Account : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }                  // Primary key
        public virtual string AccountCode { get; set; } = null!;
        public virtual string AccountName { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }

        // Optional: Navigation to inventory transactions
        public virtual ICollection<InventoryTransaction>? InventoryTransactions { get; set; }

        public static Account Create(Account account, string createdBy)
        {
            //Place your Business logic here
            account.Created_by = createdBy;
            account.Date_created = DateTime.Now;
            return account;
        }

        public static Account Update(Account account, string editedBy)
        {
            //Place your Business logic here
            account.Edited_by = editedBy;
            account.Date_edited = DateTime.Now;
            return account;
        }
    }
}
