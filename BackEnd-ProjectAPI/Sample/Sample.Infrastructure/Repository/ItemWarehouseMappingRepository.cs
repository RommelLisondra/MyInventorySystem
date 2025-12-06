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
    public class ItemWarehouseMappingRepository : IItemWarehouseMappingRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemWarehouseMappingRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemWarehouseMapping>> GetAllAsync()
        {
            var efItemWarehouseMapping = await GetAllItemWarehouseMappingsRawAsync();

            if (efItemWarehouseMapping == null || !efItemWarehouseMapping.Any())
                return Enumerable.Empty<entities.ItemWarehouseMapping>();

            var itemWarehouseMapping = efItemWarehouseMapping
                .Select(ItemWarehouseMappingMapper.MapToEntity)
                .ToList();

            return itemWarehouseMapping;
        }

        public async Task<PagedResult<entities.ItemWarehouseMapping>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemWarehouseMapping = await GetAllItemWarehouseMappingsRawAsync();

            if (itemWarehouseMapping == null || !itemWarehouseMapping.Any())
                return new PagedResult<entities.ItemWarehouseMapping>
                {
                    Data = Enumerable.Empty<entities.ItemWarehouseMapping>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemWarehouseMapping.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemWarehouseMapping
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemWarehouseMappingMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ItemWarehouseMapping>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemWarehouseMapping> GetByIdAsync(int id)
        {
            var efItemWarehouseMapping = await GetAllItemWarehouseMappingsRawAsync();

            if (efItemWarehouseMapping == null || !efItemWarehouseMapping.Any())
                return null;

            var itemWarehouseMapping = efItemWarehouseMapping
                .Select(ItemWarehouseMappingMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return itemWarehouseMapping;
        }

        public async Task AddAsync(entities.ItemWarehouseMapping? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemWarehouseMapping entity cannot be null.");

            var ItemWarehouseMapping = ItemWarehouseMappingMapper.MapToEntityFramework(entity, false);

            await _dbContext.ItemWarehouseMapping.AddAsync(ItemWarehouseMapping);
        }

        public async Task UpdateAsync(entities.ItemWarehouseMapping? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemWarehouseMapping entity cannot be null.");

            var toUpdate = await _dbContext.ItemWarehouseMapping.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemWarehouseMapping with ID {entity.id} not found in database.");

            var updatedValues = ItemWarehouseMappingMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemWarehouseMapping>> FindAsync(Expression<Func<entities.ItemWarehouseMapping, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemWarehouseMapping, entityframework.ItemWarehouseMapping>(predicate);

            var efItemWarehouseMapping = await _dbContext.ItemWarehouseMapping
                .Where(efPredicate)
                .ToListAsync();

            var result = efItemWarehouseMapping.Select(ItemWarehouseMappingMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ItemWarehouseMapping ID.", nameof(id));

            var itemWarehouseMapping = await _dbContext.ItemWarehouseMapping
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemWarehouseMapping == null)
                throw new InvalidOperationException($"ItemWarehouseMapping with ID {id} not found.");

            _dbContext.ItemWarehouseMapping.Remove(itemWarehouseMapping);
        }

        public async Task<IEnumerable<entityframework.ItemWarehouseMapping>> GetAllItemWarehouseMappingsRawAsync()
        {
            return await _dbContext.ItemWarehouseMapping
                .Where(e => e.IsActive)
                .ToListAsync();
        }
    }
}
