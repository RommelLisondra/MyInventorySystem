using System.Linq.Expressions;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Sample.Infrastructure.Mapper;
using Sample.Domain.Contracts;

namespace Sample.Infrastructure.Repository
{
    public class CostingHistoryRepository : ICostingHistoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public CostingHistoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.CostingHistory>> GetAllAsync()
        {
            var efCostingHistory = await GetAllCostingHistorysRawAsync();

            if (efCostingHistory == null || !efCostingHistory.Any())
                return Enumerable.Empty<entities.CostingHistory>();

            var costingHistory = efCostingHistory
                .Select(CostingHistoryMapper.MapToEntity)
                .ToList();

            return costingHistory;
        }

        public async Task<PagedResult<entities.CostingHistory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var costingHistory = await GetAllCostingHistorysRawAsync();

            if (costingHistory == null || !costingHistory.Any())
                return new PagedResult<entities.CostingHistory>
                {
                    Data = Enumerable.Empty<entities.CostingHistory>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = costingHistory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = costingHistory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CostingHistoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.CostingHistory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.CostingHistory> GetByIdAsync(int id)
        {
            var efCostingHistory = await GetAllCostingHistorysRawAsync();

            if (efCostingHistory == null || !efCostingHistory.Any())
                return null;

            var costingHistory = efCostingHistory
                .Select(CostingHistoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return costingHistory;
        }

        public async Task AddAsync(entities.CostingHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "CostingHistory entity cannot be null.");

            var costingHistory = CostingHistoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.CostingHistory.AddAsync(costingHistory);
        }

        public async Task UpdateAsync(entities.CostingHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "CostingHistory entity cannot be null.");

            var toUpdate = await _dbContext.CostingHistory.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"CostingHistory with ID {entity.id} not found in database.");

            var updatedValues = CostingHistoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.CostingHistory>> FindAsync(Expression<Func<entities.CostingHistory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.CostingHistory, entityframework.CostingHistory>(predicate);

            var efCostingHistory = await _dbContext.CostingHistory
                .Where(efPredicate)
                .ToListAsync();

            var result = efCostingHistory.Select(CostingHistoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid CostingHistory ID.", nameof(id));

            var costingHistory = await _dbContext.CostingHistory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (costingHistory == null)
                throw new InvalidOperationException($"CostingHistory with ID {id} not found.");

            _dbContext.CostingHistory.Remove(costingHistory);
        }

        public async Task<IEnumerable<entityframework.CostingHistory>> GetAllCostingHistorysRawAsync()
        {
            return await _dbContext.CostingHistory.ToListAsync();
        }
    }
}
