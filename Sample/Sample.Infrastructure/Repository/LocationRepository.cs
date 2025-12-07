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
    public class LocationRepository : ILocationRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public LocationRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Location>> GetAllAsync()
        {
            var efLocation = await GetAllLocationRawAsync();

            if (efLocation == null || !efLocation.Any())
                return Enumerable.Empty<entities.Location>();

            var location = efLocation
                .Select(LocationMapper.MapToEntity)
                .ToList();

            return location;
        }

        public async Task<PagedResult<entities.Location>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var location = await GetAllLocationRawAsync();

            if (location == null || !location.Any())
                return new PagedResult<entities.Location>
                {
                    Data = Enumerable.Empty<entities.Location>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = location.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = location
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(LocationMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Location>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Location> GetByIdAsync(int id)
        {
            var efLocation = await GetAllLocationRawAsync();

            if (efLocation == null || !efLocation.Any())
                return null;

            var location = efLocation
                .Select(LocationMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return location;
        }

        public async Task<entities.Location> GetByLocationCodeAsync(string LocationCode)
        {
            var efLocation = await GetAllLocationRawAsync();

            if (efLocation == null || !efLocation.Any())
                return null;

            var location = efLocation
                .Select(LocationMapper.MapToEntity).Where(e => e.LocationCode == LocationCode)
                .FirstOrDefault();

            return location;
        }

        public async Task AddAsync(entities.Location? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Location entity cannot be null.");

            var location = LocationMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Location.AddAsync(location);
        }

        public async Task UpdateAsync(entities.Location? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Location entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Location with ID {entity.id} not found in database.");

            var updatedValues = LocationMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Location>> FindAsync(Expression<Func<entities.Location, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Location cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Location, entityframework.Location>(predicate);

            var eflocation = await _dbContext.Location
                .Where(efPredicate)
                .ToListAsync();

            var result = eflocation.Select(LocationMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid location ID.", nameof(id));

            var location = await _dbContext.Location
                .FirstOrDefaultAsync(e => e.Id == id);

            if (location == null)
                throw new InvalidOperationException($"Location with ID {id} not found.");

            _dbContext.Location.Remove(location);
        }

        public async Task<IEnumerable<entityframework.Location>> GetAllLocationRawAsync()
        {
            return await _dbContext.Location
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }

}
