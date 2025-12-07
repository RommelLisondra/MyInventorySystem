using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemDetailService : IDisposable
    {
        Task<IEnumerable<ItemDetailDto>> GetAll();
        Task<ItemDetailDto> GetById(int id);
        Task CreateItem(ItemDetailDto ItemDetailDto, string createdBy);
        Task UpdateItem(ItemDetailDto ItemDetailDto, string editedBy);
        Task<IEnumerable<ItemDetailDto>> GetItemsByCustNoAsync(string itemNo);
        Task<ItemDetailDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<ItemDetailDto>> SearchItemsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<ItemDetailDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
