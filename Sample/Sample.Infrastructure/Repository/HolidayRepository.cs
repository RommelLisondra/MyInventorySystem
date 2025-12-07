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
    public class HolidayRepository : IHolidayRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public HolidayRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Holiday>> GetAllAsync()
        {
            var efHoliday = await GetAllHolidaysRawAsync();

            if (efHoliday == null || !efHoliday.Any())
                return Enumerable.Empty<entities.Holiday>();

            var holiday = efHoliday
                .Select(HolidayMapper.MapToEntity)
                .ToList();

            return holiday;
        }

        public async Task<PagedResult<entities.Holiday>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var holiday = await GetAllHolidaysRawAsync();

            if (holiday == null || !holiday.Any())
                return new PagedResult<entities.Holiday>
                {
                    Data = Enumerable.Empty<entities.Holiday>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = holiday.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = holiday
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(HolidayMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Holiday>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Holiday> GetByIdAsync(int id)
        {
            var efHoliday = await GetAllHolidaysRawAsync();

            if (efHoliday == null || !efHoliday.Any())
                return null;

            var holiday = efHoliday
                .Select(HolidayMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return holiday;
        }

        public async Task AddAsync(entities.Holiday? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Holiday entity cannot be null.");

            var holiday = HolidayMapper.MapToEntityFramework(entity, false);

            await _dbContext.Holiday.AddAsync(holiday);
        }

        public async Task UpdateAsync(entities.Holiday? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Holiday entity cannot be null.");

            var toUpdate = await _dbContext.Holiday.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Holiday with ID {entity.id} not found in database.");

            var updatedValues = HolidayMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Holiday>> FindAsync(Expression<Func<entities.Holiday, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Holiday, entityframework.Holiday>(predicate);

            var efHoliday = await _dbContext.Holiday
                .Where(efPredicate)
                .ToListAsync();

            var result = efHoliday.Select(HolidayMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Holiday ID.", nameof(id));

            var holiday = await _dbContext.Holiday
                .FirstOrDefaultAsync(e => e.Id == id);

            if (holiday == null)
                throw new InvalidOperationException($"Holiday with ID {id} not found.");

            _dbContext.Holiday.Remove(holiday);
        }

        public async Task<IEnumerable<entityframework.Holiday>> GetAllHolidaysRawAsync()
        {
            return await _dbContext.Holiday.ToListAsync();
        }
    }
}
