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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SubCategoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SubCategory>> GetAllAsync()
        {
            var efSubCategory = await GetAllSubCategorysRawAsync();

            if (efSubCategory == null || !efSubCategory.Any())
                return Enumerable.Empty<entities.SubCategory>();

            var subCategory = efSubCategory
                .Select(SubCategoryMapper.MapToEntity)
                .ToList();

            return subCategory;
        }

        public async Task<PagedResult<entities.SubCategory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var subCategory = await GetAllSubCategorysRawAsync();

            if (subCategory == null || !subCategory.Any())
                return new PagedResult<entities.SubCategory>
                {
                    Data = Enumerable.Empty<entities.SubCategory>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = subCategory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = subCategory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SubCategoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SubCategory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SubCategory> GetByIdAsync(int id)
        {
            var efSubCategory = await GetAllSubCategorysRawAsync();

            if (efSubCategory == null || !efSubCategory.Any())
                return null;

            var subCategory = efSubCategory
                .Select(SubCategoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return subCategory;
        }

        public async Task AddAsync(entities.SubCategory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SubCategory entity cannot be null.");

            var subCategory = SubCategoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.SubCategory.AddAsync(subCategory);
        }

        public async Task UpdateAsync(entities.SubCategory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SubCategory entity cannot be null.");

            var toUpdate = await _dbContext.SubCategory.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"SubCategory with ID {entity.id} not found in database.");

            var updatedValues = SubCategoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.SubCategory>> FindAsync(Expression<Func<entities.SubCategory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SubCategory, entityframework.SubCategory>(predicate);

            var efSubCategory = await _dbContext.SubCategory
                .Where(efPredicate)
                .ToListAsync();

            var result = efSubCategory.Select(SubCategoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SubCategory ID.", nameof(id));

            var subCategory = await _dbContext.SubCategory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (subCategory == null)
                throw new InvalidOperationException($"SubCategory with ID {id} not found.");

            _dbContext.SubCategory.Remove(subCategory);
        }

        public async Task<IEnumerable<entityframework.SubCategory>> GetAllSubCategorysRawAsync()
        {
            return await _dbContext.SubCategory
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
