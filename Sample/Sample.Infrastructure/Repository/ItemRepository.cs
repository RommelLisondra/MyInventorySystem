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
    public class ItemRepository : IItemRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Item>> GetAllAsync()
        {
            var efItem = await GetAllItemRawAsync();

            if (efItem == null || !efItem.Any())
                return Enumerable.Empty<entities.Item>();

            var item = efItem
                .Select(ItemMapper.MapToItem)
                .ToList();

            return item;
        }

        public async Task<PagedResult<entities.Item>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var item = await GetAllItemRawAsync();

            if (item == null || !item.Any())
                return new PagedResult<entities.Item>
                {
                    Data = Enumerable.Empty<entities.Item>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = item.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = item
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemMapper.MapToItem)
                .ToList();

            return new PagedResult<entities.Item>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Item> GetByIdAsync(int id)
        {
            var efItem = await GetAllItemRawAsync();

            if (efItem == null || !efItem.Any())
                return null;

            var item = efItem
                .Select(ItemMapper.MapToItem).Where(e => e.id == id)
                .FirstOrDefault();

            return item;
        }

        public async Task AddAsync(entities.Item? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item entity cannot be null.");

            var item = ItemMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Item.AddAsync(item);
        }

        public async Task UpdateAsync(entities.Item? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Item with ID {entity.id} not found in database.");

            var updatedValues = ItemMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Item>> FindAsync(Expression<Func<entities.Item, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Item, entityframework.Item>(predicate);

            var efitem = await _dbContext.Item
                .Where(efPredicate)
                .ToListAsync();

            var result = efitem.Select(ItemMapper.MapToItem);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Item detailed ID.", nameof(id));

            var item = await _dbContext.Item
                .FirstOrDefaultAsync(e => e.Id == id);

            if (item == null)
                throw new InvalidOperationException($"Item detailed with ID {id} not found.");

            _dbContext.Item.Remove(item);
        }

        public async Task<IEnumerable<entityframework.Item>> GetAllItemRawAsync()
        {
            return await _dbContext.Item
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
