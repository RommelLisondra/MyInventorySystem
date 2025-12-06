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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SupplierRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Supplier>> GetAllAsync()
        {
            var efSupplier = await GetAllSuppliersRawAsync();

            if (efSupplier == null || !efSupplier.Any())
                return Enumerable.Empty<entities.Supplier>();

            var supplier = efSupplier
                .Select(SupplierMapper.MapToEntity)
                .ToList();

            return supplier;
        }

        public async Task<PagedResult<entities.Supplier>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var supplier = await GetAllSuppliersRawAsync();

            if (supplier == null || !supplier.Any())
                return new PagedResult<entities.Supplier>
                {
                    Data = Enumerable.Empty<entities.Supplier>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = supplier.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = supplier
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SupplierMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Supplier>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Supplier> GetByIdAsync(int id)
        {
            var efSupplier = await GetAllSuppliersRawAsync();

            if (efSupplier == null || !efSupplier.Any())
                return null;

            var supplier = efSupplier
                .Select(SupplierMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return supplier;
        }

        public async Task<entities.Supplier> GetBySupplierNoAsync(string supplierNo)
        {
            var efSupplier = await GetAllSuppliersRawAsync();

            if (efSupplier == null || !efSupplier.Any())
                return null;

            var supplier = efSupplier
                .Select(SupplierMapper.MapToEntity).Where(e => e.SupplierNo == supplierNo)
                .FirstOrDefault();

            return supplier;
        }

        public async Task AddAsync(entities.Supplier? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Supplier entity cannot be null.");

            var supplier = SupplierMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Supplier.AddAsync(supplier);
        }

        public async Task UpdateAsync(entities.Supplier? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Supplier entity cannot be null.");

            var toUpdate = await _dbContext.Supplier.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Supplier with ID {entity.id} not found in database.");

            var updatedValues = SupplierMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Supplier>> FindAsync(Expression<Func<entities.Supplier, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Supplier, entityframework.Supplier>(predicate);

            var supplier = await _dbContext.Supplier
                .Where(efPredicate)
                .ToListAsync();

            var result = supplier.Select(SupplierMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Supplier ID.", nameof(id));

            var supplier = await _dbContext.Supplier
                .FirstOrDefaultAsync(e => e.Id == id);

            if (supplier == null)
                throw new InvalidOperationException($"Supplier with ID {id} not found.");

            _dbContext.Supplier.Remove(supplier);
        }

        public async Task<IEnumerable<entityframework.Supplier>> GetAllSuppliersRawAsync()
        {
            return await _dbContext.Supplier
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
