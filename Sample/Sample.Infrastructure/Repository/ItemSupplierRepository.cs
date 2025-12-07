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
    public class ItemSupplierRepository : IItemSupplierRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemSupplierRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemSupplier>> GetAllAsync()
        {
            var efItemSupplier = await GetAllItemSupplierRawAsync();

            if (efItemSupplier == null || !efItemSupplier.Any())
                return Enumerable.Empty<entities.ItemSupplier>();

            var itemSuppliers = efItemSupplier
                .Select(ItemSupplierMapper.MapToEntity)
                .ToList();

            return itemSuppliers;
        }

        public async Task<PagedResult<entities.ItemSupplier>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemSuppliers = await GetAllItemSupplierRawAsync();

            if (itemSuppliers == null || !itemSuppliers.Any())
                return new PagedResult<entities.ItemSupplier>
                {
                    Data = Enumerable.Empty<entities.ItemSupplier>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemSuppliers.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemSuppliers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemSupplierMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ItemSupplier>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemSupplier> GetByIdAsync(int id)
        {
            var efItemSupplier = await GetAllItemSupplierRawAsync();

            if (efItemSupplier == null || !efItemSupplier.Any())
                return null;

            var itemSuppliers = efItemSupplier
                .Select(ItemSupplierMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return itemSuppliers;
        }

        public async Task AddAsync(entities.ItemSupplier? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemSupplier entity cannot be null.");

            var itemSuppliers = ItemSupplierMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.ItemSupplier.AddAsync(itemSuppliers);
        }

        public async Task UpdateAsync(entities.ItemSupplier? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemSupplier entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemSupplier with ID {entity.id} not found in database.");

            var updatedValues = ItemSupplierMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemSupplier>> FindAsync(Expression<Func<entities.ItemSupplier, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemSupplier, entityframework.ItemSupplier>(predicate);

            var efitemSuppliers = await _dbContext.ItemSupplier
                .Where(efPredicate)
                .ToListAsync();

            var result = efitemSuppliers.Select(ItemSupplierMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ItemSupplier detailed ID.", nameof(id));

            var efitemSuppliers = await _dbContext.ItemSupplier
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efitemSuppliers == null)
                throw new InvalidOperationException($"ItemSupplier detailed with ID {id} not found.");

            _dbContext.ItemSupplier.Remove(efitemSuppliers);
        }

        public async Task<IEnumerable<entityframework.ItemSupplier>> GetAllItemSupplierRawAsync()
        {
            return await _dbContext.ItemSupplier
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
