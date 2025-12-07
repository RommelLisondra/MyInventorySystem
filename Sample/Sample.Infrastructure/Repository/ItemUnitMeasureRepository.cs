using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
//using Customer = Sample.Domain.Entities.Customer;
//using Customers = Sample.Infrastructure.EntityFramework.Customer;

using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class ItemUnitMeasureRepository : IItemUnitMeasureRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemUnitMeasureRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemUnitMeasure>> GetAllAsync()
        {
            var efItemUnitMeasure = await GetAllItemUnitMeasureRawAsync();

            if (efItemUnitMeasure == null || !efItemUnitMeasure.Any())
                return Enumerable.Empty<entities.ItemUnitMeasure>();

            var itemUnitMeasures = efItemUnitMeasure
                .Select(ItemUnitMeasureMapper.MapToEntity)
                .ToList();

            return itemUnitMeasures;
        }

        public async Task<PagedResult<entities.ItemUnitMeasure>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemUnitMeasures = await GetAllItemUnitMeasureRawAsync();

            if (itemUnitMeasures == null || !itemUnitMeasures.Any())
                return new PagedResult<entities.ItemUnitMeasure>
                {
                    Data = Enumerable.Empty<entities.ItemUnitMeasure>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemUnitMeasures.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemUnitMeasures
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemUnitMeasureMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ItemUnitMeasure>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemUnitMeasure> GetByIdAsync(int id)
        {
            var efItemUnitMeasure = await GetAllItemUnitMeasureRawAsync();

            if (efItemUnitMeasure == null || !efItemUnitMeasure.Any())
                return null;

            var itemUnitMeasures = efItemUnitMeasure
                .Select(ItemUnitMeasureMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return itemUnitMeasures;
        }

        public async Task AddAsync(entities.ItemUnitMeasure? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemUnitMeasure entity cannot be null.");

            var itemUnitMeasures = ItemUnitMeasureMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.ItemUnitMeasure.AddAsync(itemUnitMeasures);
        }

        public async Task UpdateAsync(entities.ItemUnitMeasure? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemUnitMeasure entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemUnitMeasure with ID {entity.id} not found in database.");

            var updatedValues = ItemUnitMeasureMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemUnitMeasure>> FindAsync(Expression<Func<entities.ItemUnitMeasure, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemUnitMeasure, entityframework.ItemUnitMeasure>(predicate);

            var itemUnitMeasures = await _dbContext.ItemUnitMeasure
                .Where(efPredicate)
                .ToListAsync();

            var result = itemUnitMeasures.Select(ItemUnitMeasureMapper.MapToEntity);

            return result;
        }

        public async Task<IEnumerable<entities.ItemUnitMeasure>> SearchAsync(string? keyword, string searchBy)
        {
            keyword ??= string.Empty;
            searchBy = searchBy?.ToLower() ?? string.Empty;

            return searchBy switch
            {
                "itemdetailcode" => await FindAsync(e =>
                    EF.Functions.Like(e.ItemDetailCode ?? string.Empty, $"%{keyword}%")),

                "unitcode" => await FindAsync(e =>
                    EF.Functions.Like(e.UnitCode ?? string.Empty, $"%{keyword}%")),

                _ => await FindAsync(e => true) // default: return all
            };
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ItemUnitMeasure detailed ID.", nameof(id));

            var itemUnitMeasures = await _dbContext.ItemUnitMeasure
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemUnitMeasures == null)
                throw new InvalidOperationException($"ItemUnitMeasure detailed with ID {id} not found.");

            _dbContext.ItemUnitMeasure.Remove(itemUnitMeasures);
        }

        public async Task<IEnumerable<entityframework.ItemUnitMeasure>> GetAllItemUnitMeasureRawAsync()
        {
            return await _dbContext.ItemUnitMeasure
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
