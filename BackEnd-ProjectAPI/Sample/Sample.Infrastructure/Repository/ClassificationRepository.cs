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
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ClassificationRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Classification>> GetAllAsync()
        {
            var efClassification = await GetAllClassificationsRawAsync();

            if (efClassification == null || !efClassification.Any())
                return Enumerable.Empty<entities.Classification>();

            var classifications = efClassification
                .Select(ClassificationMapper.MapToEntity)
                .ToList();

            return classifications;
        }

        public async Task<PagedResult<entities.Classification>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efClassification = await GetAllClassificationsRawAsync();

            if (efClassification == null || !efClassification.Any())
                return new PagedResult<entities.Classification>
                {
                    Data = Enumerable.Empty<entities.Classification>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efClassification.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efClassification
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ClassificationMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Classification>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Classification> GetByIdAsync(int id)
        {
            var efClassification = await GetAllClassificationsRawAsync();

            if (efClassification == null || !efClassification.Any())
                return null;

            var classifications = efClassification
                .Select(ClassificationMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return classifications;
        }

        public async Task AddAsync(entities.Classification? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Classification entity cannot be null.");

            var classification = ClassificationMapper.MapToEntityFramework(entity, false);

            await _dbContext.Classification.AddAsync(classification);
        }

        public async Task UpdateAsync(entities.Classification? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Classification entity cannot be null.");

            var toUpdate = await _dbContext.Classification.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Classification with ID {entity.id} not found in database.");

            var updatedValues = ClassificationMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Classification>> FindAsync(Expression<Func<entities.Classification, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Classification, entityframework.Classification>(predicate);

            var efClassification = await _dbContext.Classification
                .Where(efPredicate)
                .ToListAsync();

            var result = efClassification.Select(ClassificationMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Classification ID.", nameof(id));

            var classification = await _dbContext.Classification
                .FirstOrDefaultAsync(e => e.Id == id);

            if (classification == null)
                throw new InvalidOperationException($"Classification with ID {id} not found.");

            _dbContext.Classification.Remove(classification);
        }

        public async Task<IEnumerable<entityframework.Classification>> GetAllClassificationsRawAsync()
        {
            return await _dbContext.Classification
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
