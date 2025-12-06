using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ICostingHistoryService : IDisposable
    {
        Task<IEnumerable<CostingHistoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<CostingHistoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<CostingHistoryDto> GetById(int id);
        Task CreateCostingHistory(CostingHistoryDto CostingHistoryDto, string createdBy);
        Task UpdateCostingHistory(CostingHistoryDto CostingHistoryDto, string editedBy);
        Task<IEnumerable<CostingHistoryDto>> SearchCostingHistorysByKeywordAsync(string? keyword);
    }
}
