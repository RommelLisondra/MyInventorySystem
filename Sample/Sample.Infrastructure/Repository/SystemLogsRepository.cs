using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class SystemLogsRepository : ISystemLogsRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SystemLogsRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SystemLog>> GetAllAsync()
        {
            var efSystemLog = await GetAllSystemLogsRawAsync();

            if (efSystemLog == null || !efSystemLog.Any())
                return Enumerable.Empty<entities.SystemLog>();

            var systemLogs = efSystemLog
                .Select(SystemLogMapper.MapToEntity)
                .ToList();

            return systemLogs;
        }

        public async Task<PagedResult<entities.SystemLog>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var systemLogs = await GetAllSystemLogsRawAsync();

            if (systemLogs == null || !systemLogs.Any())
                return new PagedResult<entities.SystemLog>
                {
                    Data = Enumerable.Empty<entities.SystemLog>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = systemLogs.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = systemLogs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SystemLogMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SystemLog>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SystemLog> GetByIdAsync(int id)
        {
            var efSystemLog = await GetAllSystemLogsRawAsync();

            if (efSystemLog == null || !efSystemLog.Any())
                return null;

            var SystemLogs = efSystemLog
                .Select(SystemLogMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return SystemLogs;
        }

        public async Task AddAsync(entities.SystemLog? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SystemLog entity cannot be null.");

            var SystemLog = SystemLogMapper.MapToEntityFramework(entity, false);

            await _dbContext.SystemLog.AddAsync(SystemLog);
        }

        public async Task UpdateAsync(entities.SystemLog? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SystemLog entity cannot be null.");

            var toUpdate = await _dbContext.SystemLog.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"SystemLog with ID {entity.id} not found in database.");

            var updatedValues = SystemLogMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.SystemLog>> FindAsync(Expression<Func<entities.SystemLog, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SystemLog, entityframework.SystemLog>(predicate);

            var efSystemLog = await _dbContext.SystemLog
                .Where(efPredicate)
                .ToListAsync();

            var result = efSystemLog.Select(SystemLogMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SystemLog ID.", nameof(id));

            var SystemLog = await _dbContext.SystemLog
                .FirstOrDefaultAsync(e => e.Id == id);

            if (SystemLog == null)
                throw new InvalidOperationException($"SystemLog with ID {id} not found.");

            _dbContext.SystemLog.Remove(SystemLog);
        }

        public async Task<IEnumerable<entityframework.SystemLog>> GetAllSystemLogsRawAsync()
        {
            return await _dbContext.SystemLog
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
