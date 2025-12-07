namespace Sample.ApplicationService.DTOs
{
    public class ExpenseCategoryDto
    {
        public int id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<ExpenseDto> Expenses { get; set; } = new HashSet<ExpenseDto>();
    }
}
