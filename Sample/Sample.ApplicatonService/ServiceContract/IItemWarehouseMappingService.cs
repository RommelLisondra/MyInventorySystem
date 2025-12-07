using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemWarehouseMappingService : IDisposable
    {
        Task<IEnumerable<ItemWarehouseMappingDto>> GetAll();
        Task<PagedResponse<IEnumerable<ItemWarehouseMappingDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ItemWarehouseMappingDto> GetById(int id);
        Task CreateItemWarehouseMapping(ItemWarehouseMappingDto itemWarehouseMappingDto, string createdBy);
        Task UpdateItemWarehouseMapping(ItemWarehouseMappingDto itemWarehouseMappingDto, string editedBy);
        Task<IEnumerable<ItemWarehouseMappingDto>> SearchItemWarehouseMappingsByKeywordAsync(string? keyword);
    }
}
