using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IDocumentReferenceService : IDisposable
    {
        Task<IEnumerable<DocumentReferenceDto>> GetAll();
        Task<DocumentReferenceDto> GetById(int id);
        Task CreateDocumentReference(DocumentReferenceDto DocumentReferenceDto, string createdBy);
        Task UpdateDocumentReference(DocumentReferenceDto DocumentReferenceDto, string editedBy);
        Task<IEnumerable<DocumentReferenceDto>> SearchDocumentReferencesByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<DocumentReferenceDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
