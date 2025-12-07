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
    public class EmployeeDeliveredRepository : IEmployeeDeliveredRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeDeliveredRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.EmployeeDelivered>> GetAllAsync()
        {
            var efdeliverer = await GetAllCheckerRawAsync();

            if (efdeliverer == null || !efdeliverer.Any())
                return Enumerable.Empty<entities.EmployeeDelivered>();

            var deliverer = efdeliverer
                .Select(EmployeeMapper.MapToDelivered)
                .ToList();

            return deliverer;
        }

        public async Task<PagedResult<entities.EmployeeDelivered>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var deliverer = await GetAllCheckerRawAsync();

            if (deliverer == null || !deliverer.Any())
                return new PagedResult<entities.EmployeeDelivered>
                {
                    Data = Enumerable.Empty<entities.EmployeeDelivered>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = deliverer.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = deliverer
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToDelivered)
                .ToList();

            return new PagedResult<entities.EmployeeDelivered>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.EmployeeDelivered> GetByIdAsync(int id)
        {
            var efdeliverer = await GetAllCheckerRawAsync();

            if (efdeliverer == null || !efdeliverer.Any())
                return null;

            var deliverer = efdeliverer
                .Select(EmployeeMapper.MapToDelivered).Where(e => e.id == id)
                .FirstOrDefault();

            return deliverer;
        }

        public async Task AddAsync(entities.EmployeeDelivered? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Deliverer entity cannot be null.");

            var deliverer = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Deliverer.AddAsync(deliverer);
        }

        public async Task UpdateAsync(entities.EmployeeDelivered? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Deliverer entity cannot be null.");

            var toUpdate = await _dbContext.Deliverer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Deliverer with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.EmployeeDelivered>> FindAsync(Expression<Func<entities.EmployeeDelivered, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.EmployeeDelivered, entityframework.Deliverer>(predicate);

            var efdeliverer = await _dbContext.Deliverer
                .Where(efPredicate)
                .ToListAsync();

            var result = efdeliverer.Select(EmployeeMapper.MapToDelivered);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Checker ID.", nameof(id));

            var delierer = await _dbContext.Deliverer
                .FirstOrDefaultAsync(e => e.Id == id);

            if (delierer == null)
                throw new InvalidOperationException($"Deliverer with ID {id} not found.");

            _dbContext.Deliverer.Remove(delierer);
        }

        public async Task<IEnumerable<entityframework.Deliverer>> GetAllCheckerRawAsync()
        {
            return await _dbContext.Deliverer
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
