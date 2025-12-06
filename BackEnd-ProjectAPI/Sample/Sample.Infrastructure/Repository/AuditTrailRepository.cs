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
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public AuditTrailRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.AuditTrail>> GetAllAsync()
        {
            var efAuditTrail = await GetAllAuditTrailsRawAsync();

            if (efAuditTrail == null || !efAuditTrail.Any())
                return Enumerable.Empty<entities.AuditTrail>();

            var auditTrails = efAuditTrail
                .Select(AuditTrailMapper.MapToEntity)
                .ToList();

            return auditTrails;
        }

        public async Task<PagedResult<entities.AuditTrail>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efauditTrails = await GetAllAuditTrailsRawAsync();

            if (efauditTrails == null || !efauditTrails.Any())
                return new PagedResult<entities.AuditTrail>
                {
                    Data = Enumerable.Empty<entities.AuditTrail>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efauditTrails.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efauditTrails
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(AuditTrailMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.AuditTrail>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.AuditTrail> GetByIdAsync(int id)
        {
            var efAuditTrail = await GetAllAuditTrailsRawAsync();

            if (efAuditTrail == null || !efAuditTrail.Any())
                return null;

            var auditTrails = efAuditTrail
                .Select(AuditTrailMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return auditTrails;
        }

        public async Task AddAsync(entities.AuditTrail? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "AuditTrail entity cannot be null.");

            var auditTrail = AuditTrailMapper.MapToEntityFramework(entity, false);

            await _dbContext.AuditTrail.AddAsync(auditTrail);
        }

        public async Task UpdateAsync(entities.AuditTrail? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "AuditTrail entity cannot be null.");

            var toUpdate = await _dbContext.AuditTrail.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"AuditTrail with ID {entity.id} not found in database.");

            var updatedValues = AuditTrailMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.AuditTrail>> FindAsync(Expression<Func<entities.AuditTrail, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.AuditTrail, entityframework.AuditTrail>(predicate);

            var efAuditTrail = await _dbContext.AuditTrail
                .Where(efPredicate)
                .ToListAsync();

            var result = efAuditTrail.Select(AuditTrailMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid AuditTrail ID.", nameof(id));

            var AuditTrail = await _dbContext.AuditTrail
                .FirstOrDefaultAsync(e => e.Id == id);

            if (AuditTrail == null)
                throw new InvalidOperationException($"AuditTrail with ID {id} not found.");

            _dbContext.AuditTrail.Remove(AuditTrail);
        }

        public async Task<IEnumerable<entityframework.AuditTrail>> GetAllAuditTrailsRawAsync()
        {
            return await _dbContext.AuditTrail
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
