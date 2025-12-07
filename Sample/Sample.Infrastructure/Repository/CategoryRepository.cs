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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public CategoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Category>> GetAllAsync()
        {
            var efCategory = await GetAllCategorysRawAsync();

            if (efCategory == null || !efCategory.Any())
                return Enumerable.Empty<entities.Category>();

            var category = efCategory
                .Select(CategoryMapper.MapToEntity)
                .ToList();

            return category;
        }

        public async Task<PagedResult<entities.Category>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efcategory = await GetAllCategorysRawAsync();

            if (efcategory == null || !efcategory.Any())
                return new PagedResult<entities.Category>
                {
                    Data = Enumerable.Empty<entities.Category>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efcategory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efcategory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(CategoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Category>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Category> GetByIdAsync(int id)
        {
            var efCategory = await GetAllCategorysRawAsync();

            if (efCategory == null || !efCategory.Any())
                return null;

            var category = efCategory
                .Select(CategoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return category;
        }

        public async Task AddAsync(entities.Category? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Category entity cannot be null.");

            var category = CategoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.Category.AddAsync(category);
        }

        public async Task UpdateAsync(entities.Category? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Category entity cannot be null.");

            var toUpdate = await _dbContext.Category.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Category with ID {entity.id} not found in database.");

            var updatedValues = CategoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Category>> FindAsync(Expression<Func<entities.Category, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Category, entityframework.Category>(predicate);

            var efCategory = await _dbContext.Category
                .Where(efPredicate)
                .ToListAsync();

            var result = efCategory.Select(CategoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Category ID.", nameof(id));

            var category = await _dbContext.Category
                .FirstOrDefaultAsync(e => e.Id == id);

            if (category == null)
                throw new InvalidOperationException($"Category with ID {id} not found.");

            _dbContext.Category.Remove(category);
        }

        public async Task<IEnumerable<entityframework.Category>> GetAllCategorysRawAsync()
        {
            return await _dbContext.Category
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
