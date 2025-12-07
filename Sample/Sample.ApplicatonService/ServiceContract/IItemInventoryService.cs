using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemInventoryService : IDisposable
    {
        Task<IEnumerable<ItemInventoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<ItemInventoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ItemInventoryDto> GetById(int id);
        Task CreateItemInventory(ItemInventoryDto ItemInventoryDto, string createdBy);
        Task UpdateItemInventory(ItemInventoryDto ItemInventoryDto, string editedBy);
        Task<IEnumerable<ItemInventoryDto>> SearchItemInventorysByKeywordAsync(string? keyword);
    }
}
