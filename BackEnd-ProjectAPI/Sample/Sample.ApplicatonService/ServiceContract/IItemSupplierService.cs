using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemSupplierService : IDisposable
    {
        Task<IEnumerable<ItemSupplierDto>> GetAll();
        Task<ItemSupplierDto> GetById(int id);
        Task CreateItemSupplier(ItemSupplierDto ItemSupplierDto, string createdBy);
        Task UpdateItemSupplier(ItemSupplierDto ItemSupplierDto, string editedBy);
        Task<IEnumerable<ItemSupplierDto>> GetItemsByitemNoAsync(string itemNo);
        Task<ItemSupplierDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemSupplierDto>> SearchItemSupplierKeywordAsync(string? keyword);
        Task<IEnumerable<ItemSupplierDto>> SearchItemSupplierItemCodeAsync(string? itemDetailCode);
        Task<PagedResponse<IEnumerable<ItemSupplierDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
