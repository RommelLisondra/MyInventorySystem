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
    public class DocumentReferenceRepository : IDocumentReferenceRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public DocumentReferenceRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.DocumentReference>> GetAllAsync()
        {
            var efDocumentReference = await GetAllDocumentReferencesRawAsync();

            if (efDocumentReference == null || !efDocumentReference.Any())
                return Enumerable.Empty<entities.DocumentReference>();

            var documentReferences = efDocumentReference
                .Select(DocumentReferenceMapper.MapToEntity)
                .ToList();

            return documentReferences;
        }

        public async Task<PagedResult<entities.DocumentReference>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var documentReferences = await GetAllDocumentReferencesRawAsync();

            if (documentReferences == null || !documentReferences.Any())
                return new PagedResult<entities.DocumentReference>
                {
                    Data = Enumerable.Empty<entities.DocumentReference>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = documentReferences.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = documentReferences
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(DocumentReferenceMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.DocumentReference>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.DocumentReference> GetByIdAsync(int id)
        {
            var efDocumentReference = await GetAllDocumentReferencesRawAsync();

            if (efDocumentReference == null || !efDocumentReference.Any())
                return null;

            var documentReferences = efDocumentReference
                .Select(DocumentReferenceMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return documentReferences;
        }

        public async Task AddAsync(entities.DocumentReference? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "DocumentReference entity cannot be null.");

            var documentReference = DocumentReferenceMapper.MapToEntityFramework(entity, false);

            await _dbContext.DocumentReference.AddAsync(documentReference);
        }

        public async Task UpdateAsync(entities.DocumentReference? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "DocumentReference entity cannot be null.");

            var toUpdate = await _dbContext.DocumentReference.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"DocumentReference with ID {entity.id} not found in database.");

            var updatedValues = DocumentReferenceMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.DocumentReference>> FindAsync(Expression<Func<entities.DocumentReference, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.DocumentReference, entityframework.DocumentReference>(predicate);

            var efDocumentReference = await _dbContext.DocumentReference
                .Where(efPredicate)
                .ToListAsync();

            var result = efDocumentReference.Select(DocumentReferenceMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid DocumentReference ID.", nameof(id));

            var documentReference = await _dbContext.DocumentReference
                .FirstOrDefaultAsync(e => e.Id == id);

            if (documentReference == null)
                throw new InvalidOperationException($"DocumentReference with ID {id} not found.");

            _dbContext.DocumentReference.Remove(documentReference);
        }

        public async Task<IEnumerable<entityframework.DocumentReference>> GetAllDocumentReferencesRawAsync()
        {
            return await _dbContext.DocumentReference
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
