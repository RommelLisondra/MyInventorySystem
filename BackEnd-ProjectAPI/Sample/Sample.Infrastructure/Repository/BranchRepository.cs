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
    public class BranchRepository : IBranchRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public BranchRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Branch>> GetAllAsync()
        {
            var efBranch = await GetAllBranchsRawAsync();

            if (efBranch == null || !efBranch.Any())
                return Enumerable.Empty<entities.Branch>();

            var branch = efBranch
                .Select(BranchMapper.MapToEntity)
                .ToList();

            return branch;
        }

        public async Task<PagedResult<entities.Branch>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efbranch = await GetAllBranchsRawAsync();

            if (efbranch == null || !efbranch.Any())
                return new PagedResult<entities.Branch>
                {
                    Data = Enumerable.Empty<entities.Branch>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efbranch.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efbranch
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(BranchMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Branch>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Branch> GetByIdAsync(int id)
        {
            var efBranch = await GetAllBranchsRawAsync();

            if (efBranch == null || !efBranch.Any())
                return null;

            var branch = efBranch
                .Select(BranchMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return branch;
        }

        public async Task AddAsync(entities.Branch? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Branch entity cannot be null.");

            var branch = BranchMapper.MapToEntityFramework(entity, false);

            await _dbContext.Branch.AddAsync(branch);
        }

        public async Task UpdateAsync(entities.Branch? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Branch entity cannot be null.");

            var toUpdate = await _dbContext.Branch.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Branch with ID {entity.id} not found in database.");

            var updatedValues = BranchMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Branch>> FindAsync(Expression<Func<entities.Branch, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Branch, entityframework.Branch>(predicate);

            var efBranch = await _dbContext.Branch
                .Where(efPredicate)
                .ToListAsync();

            var result = efBranch.Select(BranchMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Branch ID.", nameof(id));

            var branch = await _dbContext.Branch
                .FirstOrDefaultAsync(e => e.Id == id);

            if (branch == null)
                throw new InvalidOperationException($"Branch with ID {id} not found.");

            _dbContext.Branch.Remove(branch);
        }

        public async Task<IEnumerable<entityframework.Branch>> GetAllBranchsRawAsync()
        {
            return await _dbContext.Branch.Where(e => e.IsActive).ToListAsync();
        }
    }
}
