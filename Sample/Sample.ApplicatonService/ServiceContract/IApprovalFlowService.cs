using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IApprovalFlowService : IDisposable
    {
        Task<IEnumerable<ApprovalFlowDto>> GetAll();
        Task<ApprovalFlowDto> GetById(int id);
        Task CreateApprovalFlow(ApprovalFlowDto ApprovalFlowDto, string createdBy);
        Task UpdateApprovalFlow(ApprovalFlowDto ApprovalFlowDto, string editedBy);
        Task<IEnumerable<ApprovalFlowDto>> SearchApprovalFlowsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<ApprovalFlowDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
