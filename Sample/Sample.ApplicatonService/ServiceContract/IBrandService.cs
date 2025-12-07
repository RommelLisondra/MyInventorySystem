using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IBrandService : IDisposable
    {
        Task<IEnumerable<BrandDto>> GetAll();
        Task<PagedResponse<IEnumerable<BrandDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<BrandDto> GetById(int id);
        Task CreateBrand(BrandDto BrandDto, string createdBy);
        Task UpdateBrand(BrandDto BrandDto, string editedBy);
        Task<IEnumerable<BrandDto>> SearchBrandsByKeywordAsync(string? keyword);
    }
}
