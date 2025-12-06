using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IClassificationService : IDisposable
    {
        Task<IEnumerable<ClassificationDto>> GetAll();
        Task<PagedResponse<IEnumerable<ClassificationDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ClassificationDto> GetById(int id);
        Task CreateClassification(ClassificationDto ClassificationDto, string createdBy);
        Task UpdateClassification(ClassificationDto ClassificationDto, string editedBy);
        Task<IEnumerable<ClassificationDto>> SearchClassificationsByKeywordAsync(string? keyword);
    }
}
