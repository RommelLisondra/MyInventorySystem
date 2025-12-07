namespace Sample.ApplicationService.DTOs
{
    public class ExpenseDto
    {
        public int id { get; set; }
        public string ExpenseNo { get; set; } = null!;
        public DateTime ExpenseDate { get; set; }
        public int ExpenseCategoryId { get; set; }

        public decimal Amount { get; set; }
        public string? Vendor { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ExpenseCategoryDto ExpenseCategory { get; set; } = null!;
    }
}
