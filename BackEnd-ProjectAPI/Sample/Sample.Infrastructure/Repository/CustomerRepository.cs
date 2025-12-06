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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public CustomerRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Customer>> GetAllAsync()
        {
            var efCustomer = await GetAllCustomersRawAsync();

            if (efCustomer == null || !efCustomer.Any())
                return Enumerable.Empty<entities.Customer>();

            var customers = efCustomer
                .Select(CustomerMapper.MapToEntity)
                .ToList();

            return customers;
        }

        public async Task<PagedResult<entities.Customer>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var customers = await GetAllCustomersRawAsync();

            if (customers == null || !customers.Any())
                return new PagedResult<entities.Customer>
                {
                    Data = Enumerable.Empty<entities.Customer>(),
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
                .Select(CustomerMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Customer>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Customer> GetByIdAsync(int id)
        {
            var efCustomer = await GetAllCustomersRawAsync();

            if (efCustomer == null || !efCustomer.Any())
                return null;

            var customers = efCustomer
                .Select(CustomerMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return customers;
        }

        public async Task<entities.Customer> GetByCustNoAsync(string custNo)
        {
            var efCustomer = await GetAllCustomersRawAsync();

            if (efCustomer == null || !efCustomer.Any())
                return null;

            var customers = efCustomer
                .Select(CustomerMapper.MapToEntity).Where(e => e.CustNo == custNo)
                .FirstOrDefault();

            return customers;
        }

        public async Task AddAsync(entities.Customer? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var customer = CustomerMapper.MapToEntityFramework(entity, false);

            await _dbContext.Customer.AddAsync(customer);
        }

        public async Task UpdateAsync(entities.Customer? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Customer with ID {entity.id} not found in database.");

            var updatedValues = CustomerMapper.MapToEntityFramework(entity, true);

            updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task UpdateFieldAsync<TProperty>(int customerId, string propertyName, TProperty newValue)
        {
            var customer = await _dbContext.Customer.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null)
                throw new InvalidOperationException($"Customer {customerId} not found.");

            var entry = _dbContext.Entry(customer);
            entry.Property(propertyName).CurrentValue = newValue;
            customer.ModifiedDateTime = DateTime.UtcNow;
        }

        public async Task<IEnumerable<entities.Customer>> FindAsync(Expression<Func<entities.Customer, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Customer, entityframework.Customer>(predicate);

            var efcustomer = await _dbContext.Customer
                .Where(efPredicate)
                .ToListAsync();

            var result = efcustomer.Select(CustomerMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Customer ID.", nameof(id));

            var customer = await _dbContext.Customer
                .FirstOrDefaultAsync(e => e.Id == id);

            if (customer == null)
                throw new InvalidOperationException($"Customer with ID {id} not found.");

            _dbContext.Customer.Remove(customer);
        }

        public async Task<IEnumerable<entityframework.Customer>> GetAllCustomersRawAsync()
        {
            return await _dbContext.Customer
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
