using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISupplierImageService : IDisposable
    {
        Task<PagedResponse<IEnumerable<SupplierImageDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<SupplierImageDto> GetById(int id);
        Task CreateSupplierImage(SupplierImageDto supplierImageDto, string createdBy);
        Task UpdateSupplierImage(SupplierImageDto supplierImageDto, string editedBy);
        Task<IEnumerable<SupplierImageDto>> SearchSupplierImageByKeywordAsync(string? keyword);
    }
}
