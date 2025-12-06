using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemService : IDisposable
    {
        Task<IEnumerable<ItemDto>> GetAll();
        Task<ItemDto> GetById(int id);
        Task CreateItem(ItemDto ItemDto, string createdBy);
        Task UpdateItem(ItemDto ItemDto, string editedBy);
        Task<IEnumerable<ItemDto>> GetItemsByitemNoAsync(string itemNo);
        Task<ItemDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDto>> SearchItemsAsync(string? brandName);
        Task<PagedResponse<IEnumerable<ItemDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
