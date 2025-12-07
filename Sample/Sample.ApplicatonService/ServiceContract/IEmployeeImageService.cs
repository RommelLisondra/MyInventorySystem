using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IEmployeeImageService : IDisposable
    {
        Task<PagedResponse<IEnumerable<EmployeeImageDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<EmployeeImageDto> GetById(int id);
        Task CreateEmployeeImage(EmployeeImageDto EmployeeImageDto, string createdBy);
        Task UpdateEmployeeImage(EmployeeImageDto EmployeeImageDto, string editedBy);
        Task<IEnumerable<EmployeeImageDto>> SearchEmployeeByKeywordAsync(string? keyword);
    }
}
