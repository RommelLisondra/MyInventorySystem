using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.DTOs
{
    public class AccountDto
    {
        public int id { get; set; }                  // Primary key
        public string AccountCode { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }

        // Optional: Navigation to inventory transactions
        public ICollection<InventoryTransactionDto>? InventoryTransactions { get; set; }

    }
}
