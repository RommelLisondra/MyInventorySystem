using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class InventoryBalanceRepository : IInventoryBalanceRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public InventoryBalanceRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.InventoryBalance>> GetAllAsync()
        {
            var efInventoryBalance = await GetAllInventoryBalancesRawAsync();

            if (efInventoryBalance == null || !efInventoryBalance.Any())
                return Enumerable.Empty<entities.InventoryBalance>();

            var InventoryBalance = efInventoryBalance
                .Select(InventoryBalanceMapper.MapToEntity)
                .ToList();

            return InventoryBalance;
        }

        public async Task<PagedResult<entities.InventoryBalance>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var InventoryBalance = await GetAllInventoryBalancesRawAsync();

            if (InventoryBalance == null || !InventoryBalance.Any())
                return new PagedResult<entities.InventoryBalance>
                {
                    Data = Enumerable.Empty<entities.InventoryBalance>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = InventoryBalance.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = InventoryBalance
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(InventoryBalanceMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.InventoryBalance>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.InventoryBalance> GetByIdAsync(int id)
        {
            var efInventoryBalance = await GetAllInventoryBalancesRawAsync();

            if (efInventoryBalance == null || !efInventoryBalance.Any())
                return null;

            var inventoryBalance = efInventoryBalance
                .Select(InventoryBalanceMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return inventoryBalance;
        }

        public async Task<entities.InventoryBalance> GetByitemDetailNoandWarehouseIdAsync(string itemDetailNo, int warehouseId)
        {
            var efInventoryBalance = await GetAllInventoryBalancesRawAsync();

            if (efInventoryBalance == null || !efInventoryBalance.Any())
                return null;

            var inventoryBalance = efInventoryBalance
                .Select(InventoryBalanceMapper.MapToEntity).Where(e => e.ItemDetailNo == itemDetailNo && e.WarehouseId == warehouseId)
                .FirstOrDefault();

            return inventoryBalance;
        }

        public async Task AddAsync(entities.InventoryBalance? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryBalance entity cannot be null.");

            var inventoryBalance = InventoryBalanceMapper.MapToEntityFramework(entity, false);

            await _dbContext.InventoryBalance.AddAsync(inventoryBalance);
        }

        public async Task UpdateAsync(entities.InventoryBalance? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryBalance entity cannot be null.");

            var toUpdate = await _dbContext.InventoryBalance.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"InventoryBalance with ID {entity.id} not found in database.");

            var updatedValues = InventoryBalanceMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.InventoryBalance>> FindAsync(Expression<Func<entities.InventoryBalance, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.InventoryBalance, entityframework.InventoryBalance>(predicate);

            var efInventoryBalance = await _dbContext.InventoryBalance
                .Where(efPredicate)
                .ToListAsync();

            var result = efInventoryBalance.Select(InventoryBalanceMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid InventoryBalance ID.", nameof(id));

            var inventoryBalance = await _dbContext.InventoryBalance
                .FirstOrDefaultAsync(e => e.Id == id);

            if (inventoryBalance == null)
                throw new InvalidOperationException($"InventoryBalance with ID {id} not found.");

            _dbContext.InventoryBalance.Remove(inventoryBalance);
        }

        public async Task<IEnumerable<entityframework.InventoryBalance>> GetAllInventoryBalancesRawAsync()
        {
            return await _dbContext.InventoryBalance
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
