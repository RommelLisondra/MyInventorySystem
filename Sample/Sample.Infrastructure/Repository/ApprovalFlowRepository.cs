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
    public class ApprovalFlowRepository : IApprovalFlowRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ApprovalFlowRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ApprovalFlow>> GetAllAsync()
        {
            var efApprovalFlow = await GetAllApprovalFlowsRawAsync();

            if (efApprovalFlow == null || !efApprovalFlow.Any())
                return Enumerable.Empty<entities.ApprovalFlow>();

            var approvalFlows = efApprovalFlow
                .Select(ApprovalFlowMapper.MapToEntity)
                .ToList();

            return approvalFlows;
        }

        public async Task<PagedResult<entities.ApprovalFlow>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efApprovalFlow = await GetAllApprovalFlowsRawAsync();

            if (efApprovalFlow == null || !efApprovalFlow.Any())
                return new PagedResult<entities.ApprovalFlow>
                {
                    Data = Enumerable.Empty<entities.ApprovalFlow>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efApprovalFlow.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efApprovalFlow
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ApprovalFlowMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ApprovalFlow>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ApprovalFlow> GetByIdAsync(int id)
        {
            var efApprovalFlow = await GetAllApprovalFlowsRawAsync();

            if (efApprovalFlow == null || !efApprovalFlow.Any())
                return null;

            var approvalFlows = efApprovalFlow
                .Select(ApprovalFlowMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return approvalFlows;
        }

        public async Task AddAsync(entities.ApprovalFlow? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ApprovalFlow entity cannot be null.");

            var ApprovalFlow = ApprovalFlowMapper.MapToEntityFramework(entity, false);

            await _dbContext.ApprovalFlow.AddAsync(ApprovalFlow);
        }

        public async Task UpdateAsync(entities.ApprovalFlow? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ApprovalFlow entity cannot be null.");

            var toUpdate = await _dbContext.ApprovalFlow.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ApprovalFlow with ID {entity.id} not found in database.");

            var updatedValues = ApprovalFlowMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ApprovalFlow>> FindAsync(Expression<Func<entities.ApprovalFlow, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ApprovalFlow, entityframework.ApprovalFlow>(predicate);

            var efApprovalFlow = await _dbContext.ApprovalFlow
                .Where(efPredicate)
                .ToListAsync();

            var result = efApprovalFlow.Select(ApprovalFlowMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ApprovalFlow ID.", nameof(id));

            var approvalFlow = await _dbContext.ApprovalFlow
                .FirstOrDefaultAsync(e => e.Id == id);

            if (approvalFlow == null)
                throw new InvalidOperationException($"ApprovalFlow with ID {id} not found.");

            _dbContext.ApprovalFlow.Remove(approvalFlow);
        }

        public async Task<IEnumerable<entityframework.ApprovalFlow>> GetAllApprovalFlowsRawAsync()
        {
            return await _dbContext.ApprovalFlow
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
