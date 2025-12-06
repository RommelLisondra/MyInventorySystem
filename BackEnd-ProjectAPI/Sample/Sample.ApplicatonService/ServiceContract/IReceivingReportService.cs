using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IReceivingReportService : IDisposable
    {
        Task<IEnumerable<ReceivingReportMasterDto>> GetAll();
        Task<ReceivingReportMasterDto> GetById(int id);
        Task CreateReceivingReportAsync(ReceivingReportMasterDto dto, string createdBy);
        Task UpdateReceivingReportAsync(ReceivingReportMasterDto dto, string editedBy);
        Task<IEnumerable<ReceivingReportMasterDto>> GetReceivingReportByRrNoAsync(string rrNo);
        Task<ReceivingReportMasterDto?> GetReceivingReportByIdAsync(int id);
        Task<IEnumerable<ReceivingReportMasterDto>> SearchReceivingReportByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<ReceivingReportMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
