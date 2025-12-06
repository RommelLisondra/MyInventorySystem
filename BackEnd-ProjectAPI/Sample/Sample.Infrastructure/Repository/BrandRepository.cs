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
    public class BrandRepository : IBrandRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public BrandRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Brand>> GetAllAsync()
        {
            var efBrand = await GetAllBrandsRawAsync();

            if (efBrand == null || !efBrand.Any())
                return Enumerable.Empty<entities.Brand>();

            var brand = efBrand
                .Select(BrandMapper.MapToEntity)
                .ToList();

            return brand;
        }

        public async Task<PagedResult<entities.Brand>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efbrand = await GetAllBrandsRawAsync();

            if (efbrand == null || !efbrand.Any())
                return new PagedResult<entities.Brand>
                {
                    Data = Enumerable.Empty<entities.Brand>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efbrand.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efbrand
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(BrandMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Brand>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Brand> GetByIdAsync(int id)
        {
            var efBrand = await GetAllBrandsRawAsync();

            if (efBrand == null || !efBrand.Any())
                return null;

            var brand = efBrand
                .Select(BrandMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return brand;
        }

        public async Task AddAsync(entities.Brand? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var brand = BrandMapper.MapToEntityFramework(entity, false);

            await _dbContext.Brand.AddAsync(brand);
        }

        public async Task UpdateAsync(entities.Brand? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Brand entity cannot be null.");

            var toUpdate = await _dbContext.Brand.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Brand with ID {entity.id} not found in database.");

            var updatedValues = BrandMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Brand>> FindAsync(Expression<Func<entities.Brand, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Brand, entityframework.Brand>(predicate);

            var efBrand = await _dbContext.Brand
                .Where(efPredicate)
                .ToListAsync();

            var result = efBrand.Select(BrandMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Brand ID.", nameof(id));

            var brand = await _dbContext.Brand
                .FirstOrDefaultAsync(e => e.Id == id);

            if (brand == null)
                throw new InvalidOperationException($"Brand with ID {id} not found.");

            _dbContext.Brand.Remove(brand);
        }

        public async Task<IEnumerable<entityframework.Brand>> GetAllBrandsRawAsync()
        {
            return await _dbContext.Brand
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
