using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IDocumentSeriesService : IDisposable
    {
        Task<IEnumerable<DocumentSeriesDto>> GetAll();
        Task<DocumentSeriesDto> GetById(int id);
        Task CreateDocumentSeries(DocumentSeriesDto DocumentSeriesDto, string createdBy);
        Task UpdateDocumentSeries(DocumentSeriesDto DocumentSeriesDto, string editedBy);
        Task<IEnumerable<DocumentSeriesDto>> SearchDocumentSeriessByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<DocumentSeriesDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
