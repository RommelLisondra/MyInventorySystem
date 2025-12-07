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
    public class EmployeeCheckerRepository : IEmployeeCheckerRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeCheckerRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.EmployeeChecker>> GetAllAsync()
        {
            var efChecker = await GetAllCheckerRawAsync();

            if (efChecker == null || !efChecker.Any())
                return Enumerable.Empty<entities.EmployeeChecker>();

            var checker = efChecker
                .Select(EmployeeMapper.MapToChecker)
                .ToList();

            return checker;
        }

        public async Task<PagedResult<entities.EmployeeChecker>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var checker = await GetAllCheckerRawAsync();

            if (checker == null || !checker.Any())
                return new PagedResult<entities.EmployeeChecker>
                {
                    Data = Enumerable.Empty<entities.EmployeeChecker>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = checker.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = checker
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToChecker)
                .ToList();

            return new PagedResult<entities.EmployeeChecker>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.EmployeeChecker> GetByIdAsync(int id)
        {
            var efChecker = await GetAllCheckerRawAsync();

            if (efChecker == null || !efChecker.Any())
                return null;

            var checker = efChecker
                .Select(EmployeeMapper.MapToChecker).Where(e => e.id == id)
                .FirstOrDefault();

            return checker;
        }

        public async Task AddAsync(entities.EmployeeChecker? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var customer = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Checker.AddAsync(customer);
        }

        public async Task UpdateAsync(entities.EmployeeChecker? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var toUpdate = await _dbContext.Checker.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Checker with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.EmployeeChecker>> FindAsync(Expression<Func<entities.EmployeeChecker, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.EmployeeChecker, entityframework.Checker>(predicate);

            var efcustomer = await _dbContext.Checker
                .Where(efPredicate)
                .ToListAsync();

            var result = efcustomer.Select(EmployeeMapper.MapToChecker);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Checker ID.", nameof(id));

            var checker = await _dbContext.Checker
                .FirstOrDefaultAsync(e => e.Id == id);

            if (checker == null)
                throw new InvalidOperationException($"Checker with ID {id} not found.");

            _dbContext.Checker.Remove(checker);
        }

        public async Task<IEnumerable<entityframework.Checker>> GetAllCheckerRawAsync()
        {
            return await _dbContext.Checker
                //Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
