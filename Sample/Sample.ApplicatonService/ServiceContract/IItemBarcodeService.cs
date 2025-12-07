using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.Services
{
    public interface IItemBarcodeService : IDisposable
    {
        Task<IEnumerable<ItemBarcodeDto>> GetAll();
        Task<PagedResponse<IEnumerable<ItemBarcodeDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ItemBarcodeDto> GetById(int id);
        Task CreateItemBarcode(ItemBarcodeDto ItemBarcodeDto, string createdBy);
        Task UpdateItemBarcode(ItemBarcodeDto ItemBarcodeDto, string editedBy);
        Task<IEnumerable<ItemBarcodeDto>> SearchItemBarcodesByKeywordAsync(string? keyword);
    }
}
