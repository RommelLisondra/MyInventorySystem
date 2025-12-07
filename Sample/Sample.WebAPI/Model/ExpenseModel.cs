namespace Sample.WebAPI.Model
{
    public class ExpenseModel
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
        public ExpenseCategoryModel ExpenseCategory { get; set; } = null!;
    }
}
