

namespace Sample.WebAPI.Model
{
    public class ClassificationModel
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public ICollection<ItemModel> Items { get; set; } = new HashSet<ItemModel>();
    }
}
