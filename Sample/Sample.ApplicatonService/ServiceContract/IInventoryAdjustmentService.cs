using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IInventoryAdjustmentService : IDisposable
    {
        Task<IEnumerable<InventoryAdjustmentDto>> GetAll();
        Task<InventoryAdjustmentDto> GetById(int id);
        Task CreateInventoryAdjustment(InventoryAdjustmentDto InventoryAdjustmentDto, string createdBy);
        Task UpdateInventoryAdjustment(InventoryAdjustmentDto InventoryAdjustmentDto, string editedBy);
        Task<IEnumerable<InventoryAdjustmentDto>> SearchInventoryAdjustmentsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<InventoryAdjustmentDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
