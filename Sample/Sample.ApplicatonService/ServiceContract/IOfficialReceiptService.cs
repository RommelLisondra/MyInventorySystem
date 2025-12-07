using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IOfficialReceiptService : IDisposable
    {
        Task<IEnumerable<OfficialReceiptMasterDto>> GetAll();
        Task<OfficialReceiptMasterDto> GetById(int id);
        Task CreateOfficialReceiptAsync(OfficialReceiptMasterDto dto, string createdBy);
        Task UpdateOfficialReceiptAsync(OfficialReceiptMasterDto dto, string editedBy);
        Task<IEnumerable<OfficialReceiptMasterDto>> GetItemsByCustNoAsync(string custNo);
        Task<OfficialReceiptMasterDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<OfficialReceiptMasterDto>> SearchOfficialReceiptByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<OfficialReceiptMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
