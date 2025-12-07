
namespace Sample.WebAPI.Model
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            SubCategories = new HashSet<SubCategoryModel>();
        }

        public int id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public int BrandId { get; set; }         // FK to Brand
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        // Navigation Properties
        public BrandModel Brand { get; set; } = null!;
        public ICollection<SubCategoryModel> SubCategories { get; set; }
    }
}
