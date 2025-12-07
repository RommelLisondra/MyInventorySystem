using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.DTOs
{
    public class BranchDto
    {
        public int id { get; set; }
        public int CompanyId { get; set; }               // FK → Company
        public string BranchCode { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public CompanyDto Company { get; set; } = null!;
        public ICollection<WarehouseDto> Warehouses { get; set; } = new List<WarehouseDto>();
        public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public ICollection<ItemWarehouseMappingDto> ItemWarehouseMappings { get; set; } = new List<ItemWarehouseMappingDto>();

    }
}
