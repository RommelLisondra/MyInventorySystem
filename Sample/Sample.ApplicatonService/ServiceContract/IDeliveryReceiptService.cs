using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IDeliveryReceiptService : IDisposable
    {
        Task<IEnumerable<DeliveryReceiptMasterDto>> GetAll();
        Task<DeliveryReceiptMasterDto> GetById(int id);
        Task CreateDeliveryReceiptAsync(DeliveryReceiptMasterDto dto, string createdBy);
        Task UpdateDeliveryReceiptAsync(DeliveryReceiptMasterDto dto, string editedBy);
        Task<IEnumerable<DeliveryReceiptMasterDto>> GetItemsByCustNoAsync(string custNo);
        Task<DeliveryReceiptMasterDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<DeliveryReceiptMasterDto>> SearchDeliveryReceiptByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<DeliveryReceiptMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
