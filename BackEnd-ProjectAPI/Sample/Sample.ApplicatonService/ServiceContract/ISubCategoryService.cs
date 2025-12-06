using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISubCategoryService : IDisposable
    {
        Task<IEnumerable<SubCategoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<SubCategoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<SubCategoryDto> GetById(int id);
        Task CreateSubCategory(SubCategoryDto SubCategoryDto, string createdBy);
        Task UpdateSubCategory(SubCategoryDto SubCategoryDto, string editedBy);
        Task<IEnumerable<SubCategoryDto>> SearchSubCategorysByKeywordAsync(string? keyword);
    }
}
