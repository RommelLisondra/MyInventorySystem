using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
//using Warehouse = Sample.Domain.Entities.Warehouse;
//using Warehouses = Sample.Infrastructure.EntityFramework.Warehouse;

using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class UserAcountRepository : IUserAcountRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public UserAcountRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.UserAccount>> GetAllAsync()
        {
            var efUserAccount = await GetAllUserAccountsRawAsync();

            if (efUserAccount == null || !efUserAccount.Any())
                return Enumerable.Empty<entities.UserAccount>();

            var userAccounts = efUserAccount
                .Select(UserAccountMapper.MapToEntity)
                .ToList();

            return userAccounts;
        }

        public async Task<PagedResult<entities.UserAccount>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var userAccounts = await GetAllUserAccountsRawAsync();

            if (userAccounts == null || !userAccounts.Any())
                return new PagedResult<entities.UserAccount>
                {
                    Data = Enumerable.Empty<entities.UserAccount>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = userAccounts.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = userAccounts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(UserAccountMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.UserAccount>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.UserAccount> GetByIdAsync(int id)
        {
            var efUserAccount = await GetAllUserAccountsRawAsync();

            if (efUserAccount == null || !efUserAccount.Any())
                return null;

            var userAccounts = efUserAccount
                .Select(UserAccountMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return userAccounts;
        }

        public async Task<entities.UserAccount> GetLoginUserAccount(string username, string passwordHash)
        {
            var efUserAccount = await GetAllUserAccountsRawAsync();

            if (efUserAccount == null || !efUserAccount.Any())
                return null;

            var userAccounts = efUserAccount
                .Select(UserAccountMapper.MapToEntity).Where(e => e.Username == username && e.PasswordHash == passwordHash)
                .FirstOrDefault();

            return userAccounts;
        }

        public async Task AddAsync(entities.UserAccount? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "UserAccount entity cannot be null.");

            var userAccounts = UserAccountMapper.MapToEntityFramework(entity, false);

            await _dbContext.UserAccount.AddAsync(userAccounts);
        }

        public async Task UpdateAsync(entities.UserAccount? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "UserAccount entity cannot be null.");

            var toUpdate = await _dbContext.UserAccount.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"UserAccount with ID {entity.id} not found in database.");

            var updatedValues = UserAccountMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.UserAccount>> FindAsync(Expression<Func<entities.UserAccount, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.UserAccount, entityframework.UserAccount>(predicate);

            var efUserAccount = await _dbContext.UserAccount
                .Where(efPredicate)
                .ToListAsync();

            var result = efUserAccount.Select(UserAccountMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid UserAccount ID.", nameof(id));

            var userAccounts = await _dbContext.UserAccount
                .FirstOrDefaultAsync(e => e.Id == id);

            if (userAccounts == null)
                throw new InvalidOperationException($"UserAccount with ID {id} not found.");

            _dbContext.UserAccount.Remove(userAccounts);
        }

        public async Task<IEnumerable<entityframework.UserAccount>> GetAllUserAccountsRawAsync()
        {
            return await _dbContext.UserAccount
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
