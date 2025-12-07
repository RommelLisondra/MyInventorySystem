namespace Sample.ApplicationService.DTOs
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            SubCategories = new HashSet<SubCategoryDto>();
        }

        public int id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public int BrandId { get; set; }         // FK to Brand
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Properties
        public BrandDto Brand { get; set; } = null!;
        public ICollection<SubCategoryDto> SubCategories { get; set; }
    }
}
