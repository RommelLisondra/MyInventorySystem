using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ISalesOrderService : IDisposable
    {
        Task<IEnumerable<SalesOrderMasterDto>> GetAll();
        Task<SalesOrderMasterDto> GetById(int id);
        Task CreateSalesOrderAsync(SalesOrderMasterDto dto, string createdBy);
        Task UpdateSalesOrderAsync(SalesOrderMasterDto dto, string editedBy);
        Task<IEnumerable<SalesOrderMasterDto>> GetItemsByCustNoAsync(string custNo);
        Task<SalesOrderMasterDto?> GetItemByIdAsync(int id);
        Task<IEnumerable<SalesOrderMasterDto>> SearchSalesOrderByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<SalesOrderMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
