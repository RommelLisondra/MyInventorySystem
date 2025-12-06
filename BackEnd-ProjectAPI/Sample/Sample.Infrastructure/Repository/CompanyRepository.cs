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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public CompanyRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Company>> GetAllAsync()
        {
            var efCompany = await GetAllCompanysRawAsync();

            if (efCompany == null || !efCompany.Any())
                return Enumerable.Empty<entities.Company>();

            var company = efCompany
                .Select(CompanyMapper.MapToEntity)
                .ToList();

            return company;
        }

        public async Task<PagedResult<entities.Company>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efcompany = await GetAllCompanysRawAsync();

            if (efcompany == null || !efcompany.Any())
                return new PagedResult<entities.Company>
                {
                    Data = Enumerable.Empty<entities.Company>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efcompany.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efcompany
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CompanyMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Company>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Company> GetByIdAsync(int id)
        {
            var efCompany = await GetAllCompanysRawAsync();

            if (efCompany == null || !efCompany.Any())
                return null;

            var company = efCompany
                .Select(CompanyMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return company;
        }

        public async Task AddAsync(entities.Company? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Company entity cannot be null.");

            var company = CompanyMapper.MapToEntityFramework(entity, false);

            await _dbContext.Company.AddAsync(company);
        }

        public async Task UpdateAsync(entities.Company? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Company entity cannot be null.");

            var toUpdate = await _dbContext.Company.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Company with ID {entity.id} not found in database.");

            var updatedValues = CompanyMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Company>> FindAsync(Expression<Func<entities.Company, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Company, entityframework.Company>(predicate);

            var efCompany = await _dbContext.Company
                .Where(efPredicate)
                .ToListAsync();

            var result = efCompany.Select(CompanyMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Company ID.", nameof(id));

            var company = await _dbContext.Company
                .FirstOrDefaultAsync(e => e.Id == id);

            if (company == null)
                throw new InvalidOperationException($"Company with ID {id} not found.");

            _dbContext.Company.Remove(company);
        }

        public async Task<IEnumerable<entityframework.Company>> GetAllCompanysRawAsync()
        {
            return await _dbContext.Company.Where(e => e.IsActive).ToListAsync();
        }
    }
}
