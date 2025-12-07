using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ICategoryService : IDisposable
    {
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<CategoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<CategoryDto> GetById(int id);
        Task CreateCategory(CategoryDto CategoryDto, string createdBy);
        Task UpdateCategory(CategoryDto CategoryDto, string editedBy);
        Task<IEnumerable<CategoryDto>> SearchCategorysByKeywordAsync(string? keyword);
    }
}
