using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IPurchaseReturnService : IDisposable
    {
        Task<IEnumerable<PurchaseReturnMasterDto>> GetAll();
        Task<PurchaseReturnMasterDto> GetById(int id);
        Task CreatePurchaseReturnAsync(PurchaseReturnMasterDto dto, string createdBy);
        Task UpdatePurchaseRetunrAsync(PurchaseReturnMasterDto dto, string editedBy);
        Task<IEnumerable<PurchaseReturnMasterDto>> GetPurchaseReturnByPrNoAsync(string prNo);
        Task<PurchaseReturnMasterDto?> GetPurchaseReturnByIdAsync(int id);
        Task<IEnumerable<PurchaseReturnMasterDto>> SearchPurchaseReturnByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<PurchaseReturnMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
