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
    public class EmployeeApproverRepository : IEmployeeApproverRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeApproverRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.EmployeeApprover>> GetAllAsync()
        {
            var efApprovers = await GetAllApproversRawAsync();

            if (efApprovers == null || !efApprovers.Any())
                return Enumerable.Empty<entities.EmployeeApprover>();

            var employees = efApprovers
                .Select(EmployeeMapper.MapToApprover)
                .ToList();

            return employees;
        }

        public async Task<PagedResult<entities.EmployeeApprover>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var employees = await GetAllApproversRawAsync();

            if (employees == null || !employees.Any())
                return new PagedResult<entities.EmployeeApprover>
                {
                    Data = Enumerable.Empty<entities.EmployeeApprover>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = employees.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToApprover)
                .ToList();

            return new PagedResult<entities.EmployeeApprover>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.EmployeeApprover> GetByIdAsync(int id)
        {
            var efApprovers = await GetAllApproversRawAsync();

            if (efApprovers == null || !efApprovers.Any())
                return null;

            var employees = efApprovers
                .Select(EmployeeMapper.MapToApprover).Where(e => e.id == id)
                .FirstOrDefault();

            return employees;
        }

        public async Task AddAsync(entities.EmployeeApprover? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            var employee = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Approver.AddAsync(employee);
        }

        public async Task UpdateAsync(entities.EmployeeApprover? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            var toUpdate = await _dbContext.Approver.FirstOrDefaultAsync(u => u.Id == entity.id) ?? throw new InvalidOperationException($"Employee with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.EmployeeApprover>> FindAsync(Expression<Func<entities.EmployeeApprover, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.EmployeeApprover, entityframework.Approver>(predicate);

            var efEmployees = await _dbContext.Approver
                .Where(efPredicate)
                .ToListAsync();

            var result = efEmployees.Select(EmployeeMapper.MapToApprover);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid employee ID.", nameof(id));

            var employee = await _dbContext.Approver
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                throw new InvalidOperationException($"Employee with ID {id} not found.");

            _dbContext.Approver.Remove(employee);
        }

        public async Task<IEnumerable<entityframework.Approver>> GetAllApproversRawAsync()
        {
            return await _dbContext.Approver
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
