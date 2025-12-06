using Sample.ApplicationService.DTOs;

namespace Sample.WebAPI.Model
{
    public class BranchModel
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
        public CompanyModel Company { get; set; } = null!;
        public ICollection<WarehouseModel> Warehouses { get; set; } = new List<WarehouseModel>();
        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        public ICollection<ItemWarehouseMappingModel> ItemWarehouseMappings { get; set; } = new List<ItemWarehouseMappingModel>();

    }
}
