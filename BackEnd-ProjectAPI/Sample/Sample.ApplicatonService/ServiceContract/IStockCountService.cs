using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IStockCountService : IDisposable
    {
        Task<IEnumerable<StockCountMasterDto>> GetAll();
        Task<PagedResponse<IEnumerable<StockCountMasterDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<StockCountMasterDto> GetById(int id);
        Task CreateStockCount(StockCountMasterDto StockCountMasterDto, string createdBy);
        Task UpdateStockCount(StockCountMasterDto StockCountMasterDto, string editedBy);
        Task<IEnumerable<StockCountMasterDto>> SearchStockCountMastersByKeywordAsync(string? keyword);
    }
}
