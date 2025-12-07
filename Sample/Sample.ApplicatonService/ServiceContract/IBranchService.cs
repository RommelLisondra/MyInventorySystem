using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IBranchService : IDisposable
    {
        Task<IEnumerable<BranchDto>> GetAll();
        Task<PagedResponse<IEnumerable<BranchDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<BranchDto> GetById(int id);
        Task CreateBranch(BranchDto branchDto, string createdBy);
        Task UpdateBranch(BranchDto branchDto, string editedBy);
        Task<IEnumerable<BranchDto>> SearchBranchsByKeywordAsync(string? keyword);
    }
}
