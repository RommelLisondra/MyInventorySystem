using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IPurchaseRequisitionService : IDisposable
    {
        Task<IEnumerable<PurchaseRequisitionMasterDto>> GetAll();
        Task<PurchaseRequisitionMasterDto> GetById(int id);
        Task CreatePurchaseRequisitionAsync(PurchaseRequisitionMasterDto dto, string createdBy);
        Task UpdatePurchaseRequisitionAsync(PurchaseRequisitionMasterDto dto, string editedBy);
        Task<IEnumerable<PurchaseRequisitionMasterDto>> GetPurchaseRequisitionByPrNoAsync(string prNo);
        Task<PurchaseRequisitionMasterDto?> GetPurchaseRequisitionByIdAsync(int id);
        Task<IEnumerable<PurchaseRequisitionMasterDto>> SearchPurchaseRequisitionByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<PurchaseRequisitionMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
