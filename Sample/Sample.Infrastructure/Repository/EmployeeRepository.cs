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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Employee>> GetAllAsync()
        {
            var efemployee = await GetAllEmployeeRawAsync();

            if (efemployee == null || !efemployee.Any())
                return Enumerable.Empty<entities.Employee>();

            var employee = efemployee
                .Select(EmployeeMapper.MapToEntity)
                .ToList();

            return employee;
        }

        public async Task<PagedResult<entities.Employee>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var employee = await GetAllEmployeeRawAsync();

            if (employee == null || !employee.Any())
                return new PagedResult<entities.Employee>
                {
                    Data = Enumerable.Empty<entities.Employee>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = employee.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = employee
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Employee>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Employee> GetByIdAsync(int id)
        {
            var efemployee = await GetAllEmployeeRawAsync();

            if (efemployee == null || !efemployee.Any())
                return null;

            var employee = efemployee
                .Select(EmployeeMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return employee;
        }

        public async Task AddAsync(entities.Employee? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            var employee = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Employee.AddAsync(employee);
        }

        public async Task UpdateAsync(entities.Employee? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            var toUpdate = await _dbContext.Employee.FirstOrDefaultAsync(u => u.Id == entity.id) ?? throw new InvalidOperationException($"Employee with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Employee>> FindAsync(Expression<Func<entities.Employee, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Employee, entityframework.Employee>(predicate);

            var efEmployees = await _dbContext.Employee
                .Where(efPredicate)
                .ToListAsync();

            var result = efEmployees.Select(EmployeeMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid employee ID.", nameof(id));

            var employee = await _dbContext.Employee
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                throw new InvalidOperationException($"Employee with ID {id} not found.");

            _dbContext.Employee.Remove(employee);
        }

        public async Task<IEnumerable<entityframework.Employee>> GetAllEmployeeRawAsync()
        {
            return await _dbContext.Employee
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
