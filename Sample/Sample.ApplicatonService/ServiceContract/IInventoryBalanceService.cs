using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IInventoryBalanceService : IDisposable
    {
        Task<IEnumerable<InventoryBalanceDto>> GetAll();
        Task<PagedResponse<IEnumerable<InventoryBalanceDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<InventoryBalanceDto> GetById(int id);
        Task CreateInventoryBalance(InventoryBalanceDto inventoryBalanceDto, string createdBy);
        Task UpdateInventoryBalance(InventoryBalanceDto inventoryBalanceDto, string editedBy);
        Task<IEnumerable<InventoryBalanceDto>> SearchInventoryBalancesByKeywordAsync(string? keyword);
    }
}
