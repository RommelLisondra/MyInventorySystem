using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IApprovalHistoryService : IDisposable
    {
        Task<IEnumerable<ApprovalHistoryDto>> GetAll();
        Task<ApprovalHistoryDto> GetById(int id);
        Task CreateApprovalHistory(ApprovalHistoryDto ApprovalHistoryDto, string createdBy);
        Task UpdateApprovalHistory(ApprovalHistoryDto ApprovalHistoryDto, string editedBy);
        Task<IEnumerable<ApprovalHistoryDto>> SearchApprovalHistorysByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<ApprovalHistoryDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
