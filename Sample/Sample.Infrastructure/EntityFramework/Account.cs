using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Account
    {
        public int Id { get; set; }                  // Primary key
        public string AccountCode { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }

        // Optional: Navigation to inventory transactions
        public virtual ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
    }
}
