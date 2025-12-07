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
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ExpenseCategoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ExpenseCategory>> GetAllAsync()
        {
            var efExpenseCategory = await GetAllExpenseCategorysRawAsync();

            if (efExpenseCategory == null || !efExpenseCategory.Any())
                return Enumerable.Empty<entities.ExpenseCategory>();

            var expenseCategory = efExpenseCategory
                .Select(ExpenseCategoryMapper.MapToEntity)
                .ToList();

            return expenseCategory;
        }

        public async Task<PagedResult<entities.ExpenseCategory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var expenseCategory = await GetAllExpenseCategorysRawAsync();

            if (expenseCategory == null || !expenseCategory.Any())
                return new PagedResult<entities.ExpenseCategory>
                {
                    Data = Enumerable.Empty<entities.ExpenseCategory>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = expenseCategory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = expenseCategory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ExpenseCategoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ExpenseCategory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ExpenseCategory> GetByIdAsync(int id)
        {
            var efExpenseCategory = await GetAllExpenseCategorysRawAsync();

            if (efExpenseCategory == null || !efExpenseCategory.Any())
                return null;

            var expenseCategory = efExpenseCategory
                .Select(ExpenseCategoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return expenseCategory;
        }

        public async Task AddAsync(entities.ExpenseCategory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ExpenseCategory entity cannot be null.");

            var expenseCategory = ExpenseCategoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.ExpenseCategory.AddAsync(expenseCategory);
        }

        public async Task UpdateAsync(entities.ExpenseCategory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ExpenseCategory entity cannot be null.");

            var toUpdate = await _dbContext.ExpenseCategory.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ExpenseCategory with ID {entity.id} not found in database.");

            var updatedValues = ExpenseCategoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ExpenseCategory>> FindAsync(Expression<Func<entities.ExpenseCategory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ExpenseCategory, entityframework.ExpenseCategory>(predicate);

            var efExpenseCategory = await _dbContext.ExpenseCategory
                .Where(efPredicate)
                .ToListAsync();

            var result = efExpenseCategory.Select(ExpenseCategoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ExpenseCategory ID.", nameof(id));

            var expenseCategory = await _dbContext.ExpenseCategory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expenseCategory == null)
                throw new InvalidOperationException($"ExpenseCategory with ID {id} not found.");

            _dbContext.ExpenseCategory.Remove(expenseCategory);
        }

        public async Task<IEnumerable<entityframework.ExpenseCategory>> GetAllExpenseCategorysRawAsync()
        {
            return await _dbContext.ExpenseCategory
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
