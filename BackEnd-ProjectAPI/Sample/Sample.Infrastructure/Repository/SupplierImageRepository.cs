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
    public class SupplierImageRepository : ISupplierImageRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SupplierImageRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SupplierImage>> GetAllAsync()
        {
            var efSupplierImage = await GetAllSupplierImagesRawAsync();

            if (efSupplierImage == null || !efSupplierImage.Any())
                return Enumerable.Empty<entities.SupplierImage>();

            var supplierImages = efSupplierImage
                .Select(SupplierImageMapper.MapToEntity)
                .ToList();

            return supplierImages;
        }

        public async Task<PagedResult<entities.SupplierImage>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var supplierImages = await GetAllSupplierImagesRawAsync();

            if (supplierImages == null || !supplierImages.Any())
                return new PagedResult<entities.SupplierImage>
                {
                    Data = Enumerable.Empty<entities.SupplierImage>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = supplierImages.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = supplierImages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SupplierImageMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SupplierImage>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SupplierImage> GetByIdAsync(int id)
        {
            var efSupplierImage = await GetAllSupplierImagesRawAsync();

            if (efSupplierImage == null || !efSupplierImage.Any())
                return null;

            var supplierImages = efSupplierImage
                .Select(SupplierImageMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return supplierImages;
        }

        public async Task AddAsync(entities.SupplierImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SupplierImage entity cannot be null.");

            var supplierImage = SupplierImageMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.SupplierImage.AddAsync(supplierImage);
        }

        public async Task UpdateAsync(entities.SupplierImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SupplierImage entity cannot be null.");

            var toUpdate = await _dbContext.SupplierImage.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"SupplierImage with ID {entity.id} not found in database.");

            var updatedValues = SupplierImageMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.SupplierImage>> FindAsync(Expression<Func<entities.SupplierImage, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SupplierImage, entityframework.SupplierImage>(predicate);

            var efSupplierImage = await _dbContext.SupplierImage
                .Where(efPredicate)
                .ToListAsync();

            var result = efSupplierImage.Select(SupplierImageMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SupplierImage ID.", nameof(id));

            var supplierImage = await _dbContext.SupplierImage
                .FirstOrDefaultAsync(e => e.Id == id);

            if (supplierImage == null)
                throw new InvalidOperationException($"SupplierImage with ID {id} not found.");

            _dbContext.SupplierImage.Remove(supplierImage);
        }

        public async Task<IEnumerable<entityframework.SupplierImage>> GetAllSupplierImagesRawAsync()
        {
            return await _dbContext.SupplierImage
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
