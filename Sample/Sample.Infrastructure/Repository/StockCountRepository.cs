//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;

using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

using System.Threading.Tasks;

namespace Sample.Infrastructure.Repository
{
    public class StockCountRepository : IStockCountRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public StockCountRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.StockCountMaster>> GetAllAsync()
        {
            var efStockCountMaster = await GetAllStockCountMastersRawAsync();

            if (efStockCountMaster == null || !efStockCountMaster.Any())
                return Enumerable.Empty<entities.StockCountMaster>();

            var StockCountMaster = efStockCountMaster
                .Select(StockCountMapper.MapToEntity)
                .ToList();

            return StockCountMaster;
        }

        public async Task<PagedResult<entities.StockCountMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var StockCountMaster = await GetAllStockCountMastersRawAsync();

            if (StockCountMaster == null || !StockCountMaster.Any())
                return new PagedResult<entities.StockCountMaster>
                {
                    Data = Enumerable.Empty<entities.StockCountMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = StockCountMaster.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = StockCountMaster
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(StockCountMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.StockCountMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.StockCountMaster> GetByIdAsync(int id)
        {
            var efStockCountMaster = await GetAllStockCountMastersRawAsync();

            if (efStockCountMaster == null || !efStockCountMaster.Any())
                return null;

            var StockCountMaster = efStockCountMaster
                .Select(StockCountMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return StockCountMaster;
        }

        public async Task AddAsync(entities.StockCountMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "StockCount entity cannot be null.");

            var StockCountMaster = StockCountMapper.MapToEntityFramework(entity, false);

            await _dbContext.StockCountMaster.AddAsync(StockCountMaster);
        }

        public async Task UpdateAsync(entities.StockCountMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "StockCount entity cannot be null.");

            var toUpdate = await _dbContext.StockCountMaster.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"StockCount with ID {entity.id} not found in database.");

            var updatedValues = StockCountMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.StockCountMaster>> FindAsync(Expression<Func<entities.StockCountMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.StockCountMaster, entityframework.StockCountMaster>(predicate);

            var efStockCountMaster = await _dbContext.StockCountMaster
                .Where(efPredicate)
                .ToListAsync();

            var result = efStockCountMaster.Select(StockCountMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid StockCount ID.", nameof(id));

            var StockCountMaster = await _dbContext.StockCountMaster
                .FirstOrDefaultAsync(e => e.Id == id);

            if (StockCountMaster == null)
                throw new InvalidOperationException($"StockCount with ID {id} not found.");

            _dbContext.StockCountMaster.Remove(StockCountMaster);
        }

        public async Task<IEnumerable<entityframework.StockCountMaster>> GetAllStockCountMastersRawAsync()
        {
            return await _dbContext.StockCountMaster
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
