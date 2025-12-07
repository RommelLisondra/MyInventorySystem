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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ExpenseRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Expense>> GetAllAsync()
        {
            var efExpense = await GetAllExpensesRawAsync();

            if (efExpense == null || !efExpense.Any())
                return Enumerable.Empty<entities.Expense>();

            var expenses = efExpense
                .Select(ExpenseMapper.MapToEntity)
                .ToList();

            return expenses;
        }

        public async Task<PagedResult<entities.Expense>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var expenses = await GetAllExpensesRawAsync();

            if (expenses == null || !expenses.Any())
                return new PagedResult<entities.Expense>
                {
                    Data = Enumerable.Empty<entities.Expense>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = expenses.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = expenses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ExpenseMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Expense>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Expense> GetByIdAsync(int id)
        {
            var efExpense = await GetAllExpensesRawAsync();

            if (efExpense == null || !efExpense.Any())
                return null;

            var expenses = efExpense
                .Select(ExpenseMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return expenses;
        }

        public async Task AddAsync(entities.Expense? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Expense entity cannot be null.");

            var expense = ExpenseMapper.MapToEntityFramework(entity, false);

            await _dbContext.Expense.AddAsync(expense);
        }

        public async Task UpdateAsync(entities.Expense? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Expense entity cannot be null.");

            var toUpdate = await _dbContext.Expense.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Expense with ID {entity.id} not found in database.");

            var updatedValues = ExpenseMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Expense>> FindAsync(Expression<Func<entities.Expense, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Expense, entityframework.Expense>(predicate);

            var efExpense = await _dbContext.Expense
                .Where(efPredicate)
                .ToListAsync();

            var result = efExpense.Select(ExpenseMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Expense ID.", nameof(id));

            var expense = await _dbContext.Expense
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
                throw new InvalidOperationException($"Expense with ID {id} not found.");

            _dbContext.Expense.Remove(expense);
        }

        public async Task<IEnumerable<entityframework.Expense>> GetAllExpensesRawAsync()
        {
            return await _dbContext.Expense
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
