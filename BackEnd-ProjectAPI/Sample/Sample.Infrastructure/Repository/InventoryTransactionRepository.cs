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
    internal class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public InventoryTransactionRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.InventoryTransaction>> GetAllAsync()
        {
            var efInventoryTransaction = await GetAllInventoryTransactionsRawAsync();

            if (efInventoryTransaction == null || !efInventoryTransaction.Any())
                return Enumerable.Empty<entities.InventoryTransaction>();

            var InventoryTransaction = efInventoryTransaction
                .Select(InventoryTransactionMapper.MapToEntity)
                .ToList();

            return InventoryTransaction;
        }

        public async Task<PagedResult<entities.InventoryTransaction>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var InventoryTransaction = await GetAllInventoryTransactionsRawAsync();

            if (InventoryTransaction == null || !InventoryTransaction.Any())
                return new PagedResult<entities.InventoryTransaction>
                {
                    Data = Enumerable.Empty<entities.InventoryTransaction>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = InventoryTransaction.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = InventoryTransaction
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(InventoryTransactionMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.InventoryTransaction>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.InventoryTransaction> GetByIdAsync(int id)
        {
            var efInventoryTransaction = await GetAllInventoryTransactionsRawAsync();

            if (efInventoryTransaction == null || !efInventoryTransaction.Any())
                return null;

            var InventoryTransaction = efInventoryTransaction
                .Select(InventoryTransactionMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return InventoryTransaction;
        }

        public async Task AddAsync(entities.InventoryTransaction? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryTransaction entity cannot be null.");

            var InventoryTransaction = InventoryTransactionMapper.MapToEntityFramework(entity, false);

            await _dbContext.InventoryTransaction.AddAsync(InventoryTransaction);
        }

        public async Task UpdateAsync(entities.InventoryTransaction? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryTransaction entity cannot be null.");

            var toUpdate = await _dbContext.InventoryTransaction.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"InventoryTransaction with ID {entity.id} not found in database.");

            var updatedValues = InventoryTransactionMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.InventoryTransaction>> FindAsync(Expression<Func<entities.InventoryTransaction, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.InventoryTransaction, entityframework.InventoryTransaction>(predicate);

            var efInventoryTransaction = await _dbContext.InventoryTransaction
                .Where(efPredicate)
                .ToListAsync();

            var result = efInventoryTransaction.Select(InventoryTransactionMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid InventoryTransaction ID.", nameof(id));

            var InventoryTransaction = await _dbContext.InventoryTransaction
                .FirstOrDefaultAsync(e => e.Id == id);

            if (InventoryTransaction == null)
                throw new InvalidOperationException($"InventoryTransaction with ID {id} not found.");

            _dbContext.InventoryTransaction.Remove(InventoryTransaction);
        }

        public async Task<IEnumerable<entityframework.InventoryTransaction>> GetAllInventoryTransactionsRawAsync()
        {
            return await _dbContext.InventoryTransaction
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
