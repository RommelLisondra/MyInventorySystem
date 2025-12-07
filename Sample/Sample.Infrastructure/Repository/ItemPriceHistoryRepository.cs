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
    public class ItemPriceHistoryRepository : IItemPriceHistoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemPriceHistoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemPriceHistory>> GetAllAsync()
        {
            var efItemPriceHistory = await GetAllItemPriceHistorysRawAsync();

            if (efItemPriceHistory == null || !efItemPriceHistory.Any())
                return Enumerable.Empty<entities.ItemPriceHistory>();

            var itemPriceHistory = efItemPriceHistory
                .Select(ItemPriceHistoryMapper.MapToEntity)
                .ToList();

            return itemPriceHistory;
        }

        public async Task<PagedResult<entities.ItemPriceHistory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemPriceHistory = await GetAllItemPriceHistorysRawAsync();

            if (itemPriceHistory == null || !itemPriceHistory.Any())
                return new PagedResult<entities.ItemPriceHistory>
                {
                    Data = Enumerable.Empty<entities.ItemPriceHistory>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemPriceHistory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemPriceHistory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemPriceHistoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ItemPriceHistory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemPriceHistory> GetByIdAsync(int id)
        {
            var efItemPriceHistory = await GetAllItemPriceHistorysRawAsync();

            if (efItemPriceHistory == null || !efItemPriceHistory.Any())
                return null;

            var itemPriceHistory = efItemPriceHistory
                .Select(ItemPriceHistoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return itemPriceHistory;
        }

        public async Task AddAsync(entities.ItemPriceHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemPriceHistory entity cannot be null.");

            var itemPriceHistory = ItemPriceHistoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.ItemPriceHistory.AddAsync(itemPriceHistory);
        }

        public async Task UpdateAsync(entities.ItemPriceHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemPriceHistory entity cannot be null.");

            var toUpdate = await _dbContext.ItemPriceHistory.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemPriceHistory with ID {entity.id} not found in database.");

            var updatedValues = ItemPriceHistoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemPriceHistory>> FindAsync(Expression<Func<entities.ItemPriceHistory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemPriceHistory, entityframework.ItemPriceHistory>(predicate);

            var efItemPriceHistory = await _dbContext.ItemPriceHistory
                .Where(efPredicate)
                .ToListAsync();

            var result = efItemPriceHistory.Select(ItemPriceHistoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ItemPriceHistory ID.", nameof(id));

            var itemPriceHistory = await _dbContext.ItemPriceHistory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemPriceHistory == null)
                throw new InvalidOperationException($"ItemPriceHistory with ID {id} not found.");

            _dbContext.ItemPriceHistory.Remove(itemPriceHistory);
        }

        public async Task<IEnumerable<entityframework.ItemPriceHistory>> GetAllItemPriceHistorysRawAsync()
        {
            return await _dbContext.ItemPriceHistory.ToListAsync();
        }
    }
}
