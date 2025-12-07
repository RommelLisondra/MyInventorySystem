using Sample.Domain.Entities;


namespace Sample.Domain.Contracts
{
    public interface IInventoryBalanceRepository : IRepository<InventoryBalance>
    {
        Task<InventoryBalance> GetByitemDetailNoandWarehouseIdAsync(string itemDetailNo, int warehouseId);
    }
}
