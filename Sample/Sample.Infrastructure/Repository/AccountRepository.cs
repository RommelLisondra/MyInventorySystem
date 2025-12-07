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
    public class AccountRepository : IAccountRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public AccountRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Account>> GetAllAsync()
        {
            var efAccount = await GetAllAccountsRawAsync();

            if (efAccount == null || !efAccount.Any())
                return Enumerable.Empty<entities.Account>();

            var Accounts = efAccount
                .Select(AccountMapper.MapToEntity)
                .ToList();

            return Accounts;
        }

        public async Task<PagedResult<entities.Account>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efWarehouse = await GetAllAccountsRawAsync();

            if (efWarehouse == null || !efWarehouse.Any())
                return new PagedResult<entities.Account>
                {
                    Data = Enumerable.Empty<entities.Account>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efWarehouse.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efWarehouse
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(AccountMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Account>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Account> GetByIdAsync(int id)
        {
            var efAccount = await GetAllAccountsRawAsync();

            if (efAccount == null || !efAccount.Any())
                return null;

            var Accounts = efAccount
                .Select(AccountMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return Accounts;
        }

        public async Task AddAsync(entities.Account? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Account entity cannot be null.");

            var Account = AccountMapper.MapToEntityFramework(entity, false);

            await _dbContext.Account.AddAsync(Account);
        }

        public async Task UpdateAsync(entities.Account? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Account entity cannot be null.");

            var toUpdate = await _dbContext.Account.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Account with ID {entity.id} not found in database.");

            var updatedValues = AccountMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Account>> FindAsync(Expression<Func<entities.Account, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Account, entityframework.Account>(predicate);

            var efAccount = await _dbContext.Account
                .Where(efPredicate)
                .ToListAsync();

            var result = efAccount.Select(AccountMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Account ID.", nameof(id));

            var Account = await _dbContext.Account
                .FirstOrDefaultAsync(e => e.Id == id);

            if (Account == null)
                throw new InvalidOperationException($"Account with ID {id} not found.");

            _dbContext.Account.Remove(Account);
        }

        public async Task<IEnumerable<entityframework.Account>> GetAllAccountsRawAsync()
        {
            return await _dbContext.Account.Where(e => e.IsActive).ToListAsync();
        }
    }
}
