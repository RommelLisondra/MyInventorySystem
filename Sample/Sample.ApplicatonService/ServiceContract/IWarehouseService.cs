using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;
namespace Sample.ApplicationService.ServiceContract
{
    public interface IWarehouseService : IDisposable
    {
        Task<IEnumerable<WarehouseDto>> GetAll();
        Task<WarehouseDto> GetById(int id);
        Task CreateWarehouse(WarehouseDto WarehouseDto, string createdBy);
        Task UpdateWarehouse(WarehouseDto WarehouseDto, string editedBy);
        Task<IEnumerable<WarehouseDto>> GetWarehousesByCustNoAsync(string warehouseCode);
        Task<WarehouseDto?> GetWarehouseByIdAsync(int id);
        Task<IEnumerable<WarehouseDto>> SearchWarehousesAsync(string? keyword);
        Task<PagedResponse<IEnumerable<WarehouseDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
