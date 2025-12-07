using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemImageService : IDisposable
    {
        Task<IEnumerable<ItemImageDto>> GetAll();
        Task<ItemImageDto> GetById(int id);
        Task CreateItemImage(ItemImageDto ItemImageDto, string createdBy);
        Task UpdateItemImage(ItemImageDto ItemImageDto, string editedBy);
        Task<ItemImageDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemImageDto>> SearchItemsAsync(string? itemDetailCode);
        Task<PagedResponse<IEnumerable<ItemImageDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
