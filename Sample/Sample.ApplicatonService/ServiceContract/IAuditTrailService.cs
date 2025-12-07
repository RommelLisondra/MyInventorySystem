using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IAuditTrailService : IDisposable
    {
        Task<IEnumerable<AuditTrailDto>> GetAll();
        Task<AuditTrailDto> GetById(int id);
        Task CreateAuditTrail(AuditTrailDto AuditTrailDto, string createdBy);
        Task UpdateAuditTrail(AuditTrailDto AuditTrailDto, string editedBy);
        Task<IEnumerable<AuditTrailDto>> SearchAuditTrailsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<AuditTrailDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
