namespace Sample.WebAPI.Model
{
    public class ExpenseCategoryModel
    {
        public int id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<ExpenseModel> Expenses { get; set; } = new HashSet<ExpenseModel>();
    }
}
