using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IStockTransferService : IDisposable
    {
        Task<IEnumerable<StockTransferDto>> GetAll();
        Task<StockTransferDto> GetById(int id);
        Task CreateStockTransfer(StockTransferDto StockTransferDto, string createdBy);
        Task UpdateStockTransfer(StockTransferDto StockTransferDto, string editedBy);
        Task<IEnumerable<StockTransferDto>> SearchStockTransfersByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<StockTransferDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
