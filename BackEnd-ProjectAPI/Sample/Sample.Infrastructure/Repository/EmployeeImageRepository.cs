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
    public class EmployeeImageRepository : IEmployeeImageRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public EmployeeImageRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.EmployeeImage>> GetAllAsync()
        {
            var efemployeeImage = await GetAllEmployeeImageRawAsync();

            if (efemployeeImage == null || !efemployeeImage.Any())
                return Enumerable.Empty<entities.EmployeeImage>();

            var employeeImage = efemployeeImage
                .Select(EmployeeMapper.MapToEmployeeImage)
                .ToList();

            return employeeImage;
        }

        public async Task<PagedResult<entities.EmployeeImage>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var employeeImage = await GetAllEmployeeImageRawAsync();

            if (employeeImage == null || !employeeImage.Any())
                return new PagedResult<entities.EmployeeImage>
                {
                    Data = Enumerable.Empty<entities.EmployeeImage>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = employeeImage.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = employeeImage
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(EmployeeMapper.MapToEmployeeImage)
                .ToList();

            return new PagedResult<entities.EmployeeImage>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.EmployeeImage> GetByIdAsync(int id)
        {
            var efemployeeImage = await GetAllEmployeeImageRawAsync();

            if (efemployeeImage == null || !efemployeeImage.Any())
                return null;

            var employeeImage = efemployeeImage
                .Select(EmployeeMapper.MapToEmployeeImage).Where(e => e.id == id)
                .FirstOrDefault();

            return employeeImage;
        }

        public async Task AddAsync(entities.EmployeeImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var employeeImage = EmployeeMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.EmployeeImage.AddAsync(employeeImage);
        }

        public async Task UpdateAsync(entities.EmployeeImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var toUpdate = await _dbContext.EmployeeImage.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Checker with ID {entity.id} not found in database.");

            var updatedValues = EmployeeMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.EmployeeImage>> FindAsync(Expression<Func<entities.EmployeeImage, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.EmployeeImage, entityframework.EmployeeImage>(predicate);

            var efemployeeImage = await _dbContext.EmployeeImage
                .Where(efPredicate)
                .ToListAsync();

            var result = efemployeeImage.Select(EmployeeMapper.MapToEmployeeImage);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Checker ID.", nameof(id));

            var employeeImage = await _dbContext.EmployeeImage
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employeeImage == null)
                throw new InvalidOperationException($"Checker with ID {id} not found.");

            _dbContext.EmployeeImage.Remove(employeeImage);
        }

        public async Task<IEnumerable<entityframework.EmployeeImage>> GetAllEmployeeImageRawAsync()
        {
            return await _dbContext.EmployeeImage
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
