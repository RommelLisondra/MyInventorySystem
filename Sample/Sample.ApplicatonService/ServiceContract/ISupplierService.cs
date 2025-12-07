using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISupplierService : IDisposable
    {
        Task<IEnumerable<SupplierDto>> GetAll();
        Task<SupplierDto> GetById(int id);
        Task CreateSupplier(SupplierDto SupplierDto, string createdBy);
        Task UpdateSupplier(SupplierDto SupplierDto, string editedBy);
        Task<IEnumerable<SupplierDto>> GetSuppliersBysupplierNoAsync(string supplierNo);
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task<IEnumerable<SupplierDto>> SearchSuppliersByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SupplierDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
