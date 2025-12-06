using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IPurchaseOrderService : IDisposable
    {
        Task<IEnumerable<PurchaseOrderMasterDto>> GetAll();
        Task<PurchaseOrderMasterDto> GetById(int id);
        Task CreatePurchaseOrderAsync(PurchaseOrderMasterDto dto, string createdBy);
        Task UpdatePurchaseOrderAsync(PurchaseOrderMasterDto dto, string editedBy);
        Task<IEnumerable<PurchaseOrderMasterDto>> GetPurchaseOrderByPrNoAsync(string prNo);
        Task<PurchaseOrderMasterDto?> GetPurchaseOrderByIdAsync(int id);
        Task<IEnumerable<PurchaseOrderMasterDto>> SearchPurchaseOrderByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<PurchaseOrderMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
