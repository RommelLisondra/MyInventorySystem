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
    public class EmployeeSalesRefRepository : IEmployeeSalesRefRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeSalesRefRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.EmployeeSalesRef>> GetAllAsync()
        {
            var efsalesRef = await GetAllCheckerRawAsync();

            if (efsalesRef == null || !efsalesRef.Any())
                return Enumerable.Empty<entities.EmployeeSalesRef>();

            var salesRef = efsalesRef
                .Select(EmployeeMapper.MapToSalesRefEntity)
                .ToList();

            return salesRef;
        }

        public async Task<PagedResult<entities.EmployeeSalesRef>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var salesRef = await GetAllCheckerRawAsync();

            if (salesRef == null || !salesRef.Any())
                return new PagedResult<entities.EmployeeSalesRef>
                {
                    Data = Enumerable.Empty<entities.EmployeeSalesRef>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = salesRef.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = salesRef
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToSalesRefEntity)
                .ToList();

            return new PagedResult<entities.EmployeeSalesRef>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.EmployeeSalesRef> GetByIdAsync(int id)
        {
            var efsalesRef = await GetAllCheckerRawAsync();

            if (efsalesRef == null || !efsalesRef.Any())
                return null;

            var salesRef = efsalesRef
                .Select(EmployeeMapper.MapToSalesRefEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return salesRef;
        }

        public async Task AddAsync(entities.EmployeeSalesRef? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee SalesRef entity cannot be null.");

            var salesRef = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.SalesRef.AddAsync(salesRef);
        }

        public async Task UpdateAsync(entities.EmployeeSalesRef? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee SalesRef entity cannot be null.");

            var toUpdate = await _dbContext.SalesRef.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"SalesRef with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.EmployeeSalesRef>> FindAsync(Expression<Func<entities.EmployeeSalesRef, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.EmployeeSalesRef, entityframework.SalesRef>(predicate);

            var efsalesRef = await _dbContext.SalesRef
                .Where(efPredicate)
                .ToListAsync();

            var result = efsalesRef.Select(EmployeeMapper.MapToSalesRefEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Checker ID.", nameof(id));

            var salesRef = await _dbContext.SalesRef
                .FirstOrDefaultAsync(e => e.Id == id);

            if (salesRef == null)
                throw new InvalidOperationException($"SalesRef with ID {id} not found.");

            _dbContext.SalesRef.Remove(salesRef);
        }

        public async Task<IEnumerable<entityframework.SalesRef>> GetAllCheckerRawAsync()
        {
            return await _dbContext.SalesRef
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
