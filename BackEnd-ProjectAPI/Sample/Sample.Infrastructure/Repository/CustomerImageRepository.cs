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
    public class CustomerImageRepository : ICustomerImageRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public CustomerImageRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.CustomerImage>> GetAllAsync()
        {
            var efCustomer = await GetAllCustomerImagesRawAsync();

            if (efCustomer == null || !efCustomer.Any())
                return Enumerable.Empty<entities.CustomerImage>();

            var customers = efCustomer
                .Select(CustomerMapper.CustomerImageMapToEntity)
                .ToList();

            return customers;
        }

        public async Task<PagedResult<entities.CustomerImage>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var customers = await GetAllCustomerImagesRawAsync();

            if (customers == null || !customers.Any())
                return new PagedResult<entities.CustomerImage>
                {
                    Data = Enumerable.Empty<entities.CustomerImage>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = customers.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CustomerMapper.CustomerImageMapToEntity)
                .ToList();

            return new PagedResult<entities.CustomerImage>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.CustomerImage> GetByIdAsync(int id)
        {
            var efCustomer = await GetAllCustomerImagesRawAsync();

            if (efCustomer == null || !efCustomer.Any())
                return null;

            var customers = efCustomer
                .Select(CustomerMapper.CustomerImageMapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return customers;

        }

        public async Task AddAsync(entities.CustomerImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer Image entity cannot be null.");

            var customerimage = CustomerMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.CustomerImage.AddAsync(customerimage);
        }

        public async Task UpdateAsync(entities.CustomerImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer Image entity cannot be null.");

            var toUpdate = await _dbContext.CustomerImage.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Customer Image with ID {entity.id} not found in database.");

            var customerimage = CustomerMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(customerimage);
        }

        public async Task<IEnumerable<entities.CustomerImage>> FindAsync(Expression<Func<entities.CustomerImage, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.CustomerImage, entityframework.CustomerImage>(predicate);

            var efEmployees = await _dbContext.CustomerImage
                .Where(efPredicate)
                .ToListAsync();

            var result = efEmployees.Select(CustomerMapper.CustomerImageMapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Customer Image ID.", nameof(id));

            var customerImage = await _dbContext.CustomerImage
                .FirstOrDefaultAsync(e => e.Id == id);

            if (customerImage == null)
                throw new InvalidOperationException($"Customer Image with ID {id} not found.");

            _dbContext.CustomerImage.Remove(customerImage);
        }

        public async Task<IEnumerable<entityframework.CustomerImage>> GetAllCustomerImagesRawAsync()
        {
            return await _dbContext.CustomerImage
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
