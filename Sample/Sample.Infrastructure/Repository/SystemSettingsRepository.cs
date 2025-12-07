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
    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SystemSettingsRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SystemSetting>> GetAllAsync()
        {
            var efSystemSetting = await GetAllSystemSettingsRawAsync();

            if (efSystemSetting == null || !efSystemSetting.Any())
                return Enumerable.Empty<entities.SystemSetting>();

            var systemSetting = efSystemSetting
                .Select(SystemSettingMapper.MapToEntity)
                .ToList();

            return systemSetting;
        }

        public async Task<PagedResult<entities.SystemSetting>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var systemSetting = await GetAllSystemSettingsRawAsync();

            if (systemSetting == null || !systemSetting.Any())
                return new PagedResult<entities.SystemSetting>
                {
                    Data = Enumerable.Empty<entities.SystemSetting>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = systemSetting.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = systemSetting
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SystemSettingMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SystemSetting>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SystemSetting> GetByIdAsync(int id)
        {
            var efSystemSetting = await GetAllSystemSettingsRawAsync();

            if (efSystemSetting == null || !efSystemSetting.Any())
                return null;

            var SystemSettings = efSystemSetting
                .Select(SystemSettingMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return SystemSettings;
        }

        public async Task AddAsync(entities.SystemSetting? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SystemSetting entity cannot be null.");

            var SystemSetting = SystemSettingMapper.MapToEntityFramework(entity, false);

            await _dbContext.SystemSetting.AddAsync(SystemSetting);
        }

        public async Task UpdateAsync(entities.SystemSetting? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "SystemSetting entity cannot be null.");

            var toUpdate = await _dbContext.SystemSetting.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"SystemSetting with ID {entity.id} not found in database.");

            var updatedValues = SystemSettingMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.SystemSetting>> FindAsync(Expression<Func<entities.SystemSetting, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SystemSetting, entityframework.SystemSetting>(predicate);

            var efSystemSetting = await _dbContext.SystemSetting
                .Where(efPredicate)
                .ToListAsync();

            var result = efSystemSetting.Select(SystemSettingMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SystemSetting ID.", nameof(id));

            var SystemSetting = await _dbContext.SystemSetting
                .FirstOrDefaultAsync(e => e.Id == id);

            if (SystemSetting == null)
                throw new InvalidOperationException($"SystemSetting with ID {id} not found.");

            _dbContext.SystemSetting.Remove(SystemSetting);
        }

        public async Task<IEnumerable<entityframework.SystemSetting>> GetAllSystemSettingsRawAsync()
        {
            return await _dbContext.SystemSetting
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
