using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IInventoryTransactionService : IDisposable
    {
        Task<IEnumerable<InventoryTransactionDto>> GetAll();
        Task<PagedResponse<IEnumerable<InventoryTransactionDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<InventoryTransactionDto> GetById(int id);
        Task CreateInventoryTransaction(InventoryTransactionDto inventoryTransactionDto, string createdBy);
        Task UpdateInventoryTransaction(InventoryTransactionDto InventoryTransactionDto, string editedBy);
        Task<IEnumerable<InventoryTransactionDto>> SearchInventoryTransactionsByKeywordAsync(string? keyword);
    }
}
