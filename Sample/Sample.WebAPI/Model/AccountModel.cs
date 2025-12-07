using Sample.ApplicationService.DTOs;

namespace Sample.WebAPI.Model
{
    public class AccountModel
    {
        public int id { get; set; }                  // Primary key
        public string AccountCode { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }

        // Optional: Navigation to inventory transactions
        public ICollection<InventoryTransactionModel>? InventoryTransactions { get; set; }

    }
}
