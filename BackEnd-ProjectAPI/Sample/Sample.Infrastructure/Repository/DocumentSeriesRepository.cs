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
    public class DocumentSeriesRepository : IDocumentSeriesRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public DocumentSeriesRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.DocumentSeries>> GetAllAsync()
        {
            var efDocumentSeries = await GetAllDocumentSeriessRawAsync();

            if (efDocumentSeries == null || !efDocumentSeries.Any())
                return Enumerable.Empty<entities.DocumentSeries>();

            var documentSeries = efDocumentSeries
                .Select(DocumentSeriesMapper.MapToEntity)
                .ToList();

            return documentSeries;
        }

        public async Task<PagedResult<entities.DocumentSeries>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var documentSeries = await GetAllDocumentSeriessRawAsync();

            if (documentSeries == null || !documentSeries.Any())
                return new PagedResult<entities.DocumentSeries>
                {
                    Data = Enumerable.Empty<entities.DocumentSeries>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = documentSeries.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = documentSeries
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(DocumentSeriesMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.DocumentSeries>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.DocumentSeries> GetByIdAsync(int id)
        {
            var efDocumentSeries = await GetAllDocumentSeriessRawAsync();

            if (efDocumentSeries == null || !efDocumentSeries.Any())
                return null;

            var documentSeries = efDocumentSeries
                .Select(DocumentSeriesMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return documentSeries;
        }

        public async Task AddAsync(entities.DocumentSeries? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "DocumentSeries entity cannot be null.");

            var documentSeries = DocumentSeriesMapper.MapToEntityFramework(entity, false);

            await _dbContext.DocumentSeries.AddAsync(documentSeries);
        }

        public async Task UpdateAsync(entities.DocumentSeries? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "DocumentSeries entity cannot be null.");

            var toUpdate = await _dbContext.DocumentSeries.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"DocumentSeries with ID {entity.id} not found in database.");

            var updatedValues = DocumentSeriesMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.DocumentSeries>> FindAsync(Expression<Func<entities.DocumentSeries, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.DocumentSeries, entityframework.DocumentSeries>(predicate);

            var efDocumentSeries = await _dbContext.DocumentSeries
                .Where(efPredicate)
                .ToListAsync();

            var result = efDocumentSeries.Select(DocumentSeriesMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid DocumentSeries ID.", nameof(id));

            var documentSeries = await _dbContext.DocumentSeries
                .FirstOrDefaultAsync(e => e.Id == id);

            if (documentSeries == null)
                throw new InvalidOperationException($"DocumentSeries with ID {id} not found.");

            _dbContext.DocumentSeries.Remove(documentSeries);
        }

        public async Task<IEnumerable<entityframework.DocumentSeries>> GetAllDocumentSeriessRawAsync()
        {
            return await _dbContext.DocumentSeries
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
