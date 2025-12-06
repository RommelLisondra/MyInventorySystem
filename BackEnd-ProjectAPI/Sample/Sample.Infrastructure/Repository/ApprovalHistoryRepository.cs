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
    public class ApprovalHistoryRepository : IApprovalHistoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ApprovalHistoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ApprovalHistory>> GetAllAsync()
        {
            var efApprovalHistory = await GetAllApprovalHistorysRawAsync();

            if (efApprovalHistory == null || !efApprovalHistory.Any())
                return Enumerable.Empty<entities.ApprovalHistory>();

            var approvalHistory = efApprovalHistory
                .Select(ApprovalHistoryMapper.MapToEntity)
                .ToList();

            return approvalHistory;
        }

        public async Task<PagedResult<entities.ApprovalHistory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efApprovalHistory = await GetAllApprovalHistorysRawAsync();

            if (efApprovalHistory == null || !efApprovalHistory.Any())
                return new PagedResult<entities.ApprovalHistory>
                {
                    Data = Enumerable.Empty<entities.ApprovalHistory>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efApprovalHistory.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efApprovalHistory
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ApprovalHistoryMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ApprovalHistory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ApprovalHistory> GetByIdAsync(int id)
        {
            var efApprovalHistory = await GetAllApprovalHistorysRawAsync();

            if (efApprovalHistory == null || !efApprovalHistory.Any())
                return null;

            var approvalHistory = efApprovalHistory
                .Select(ApprovalHistoryMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return approvalHistory;
        }

        public async Task AddAsync(entities.ApprovalHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ApprovalHistory entity cannot be null.");

            var approvalHistory = ApprovalHistoryMapper.MapToEntityFramework(entity, false);

            await _dbContext.ApprovalHistory.AddAsync(approvalHistory);
        }

        public async Task UpdateAsync(entities.ApprovalHistory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ApprovalHistory entity cannot be null.");

            var toUpdate = await _dbContext.ApprovalHistory.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ApprovalHistory with ID {entity.id} not found in database.");

            var updatedValues = ApprovalHistoryMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ApprovalHistory>> FindAsync(Expression<Func<entities.ApprovalHistory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ApprovalHistory, entityframework.ApprovalHistory>(predicate);

            var efApprovalHistory = await _dbContext.ApprovalHistory
                .Where(efPredicate)
                .ToListAsync();

            var result = efApprovalHistory.Select(ApprovalHistoryMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ApprovalHistory ID.", nameof(id));

            var approvalHistory = await _dbContext.ApprovalHistory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (approvalHistory == null)
                throw new InvalidOperationException($"ApprovalHistory with ID {id} not found.");

            _dbContext.ApprovalHistory.Remove(approvalHistory);
        }

        public async Task<IEnumerable<entityframework.ApprovalHistory>> GetAllApprovalHistorysRawAsync()
        {
            return await _dbContext.ApprovalHistory
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
