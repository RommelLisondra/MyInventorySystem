namespace Sample.ApplicationService.DTOs
{
    public class BrandDto
    {
        public BrandDto()
        {
            Categories = new HashSet<CategoryDto>();
        }

        public int id { get; set; }
        public string BrandName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Property → Brand can have multiple Categories
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
