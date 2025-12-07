namespace Sample.ApplicationService.DTOs
{
    public class ClassificationDto
    {
        public int id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public ICollection<ItemDto> Items { get; set; } = new HashSet<ItemDto>();
    }
}
