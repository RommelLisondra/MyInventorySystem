using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IItemPriceHistoryService : IDisposable
    {
        Task<IEnumerable<ItemPriceHistoryDto>> GetAll();
        Task<PagedResponse<IEnumerable<ItemPriceHistoryDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<ItemPriceHistoryDto> GetById(int id);
        Task CreateItemPriceHistory(ItemPriceHistoryDto ItemPriceHistoryDto, string createdBy);
        Task UpdateItemPriceHistory(ItemPriceHistoryDto ItemPriceHistoryDto, string editedBy);
        Task<IEnumerable<ItemPriceHistoryDto>> SearchItemPriceHistorysByKeywordAsync(string? keyword);
    }
}
