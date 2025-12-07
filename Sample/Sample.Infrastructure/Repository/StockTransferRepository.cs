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
    public class StockTransferRepository : IStockTransferRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public StockTransferRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.StockTransfer>> GetAllAsync()
        {
            var efStockTransfer = await GetAllStockTransfersRawAsync();

            if (efStockTransfer == null || !efStockTransfer.Any())
                return Enumerable.Empty<entities.StockTransfer>();

            var stockTransfer = efStockTransfer
                .Select(StockTransferMapper.MapToEntity)
                .ToList();

            return stockTransfer;
        }

        public async Task<PagedResult<entities.StockTransfer>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var stockTransfer = await GetAllStockTransfersRawAsync();

            if (stockTransfer == null || !stockTransfer.Any())
                return new PagedResult<entities.StockTransfer>
                {
                    Data = Enumerable.Empty<entities.StockTransfer>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = stockTransfer.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = stockTransfer
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(StockTransferMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.StockTransfer>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.StockTransfer> GetByIdAsync(int id)
        {
            var efStockTransfer = await GetAllStockTransfersRawAsync();

            if (efStockTransfer == null || !efStockTransfer.Any())
                return null;

            var stockTransfer = efStockTransfer
                .Select(StockTransferMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return stockTransfer;
        }

        public async Task AddAsync(entities.StockTransfer? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "StockTransfer entity cannot be null.");

            var StockTransfer = StockTransferMapper.MapToEntityFramework(entity, false);

            await _dbContext.StockTransfer.AddAsync(StockTransfer);
        }

        public async Task UpdateAsync(entities.StockTransfer? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "StockTransfer entity cannot be null.");

            var toUpdate = await _dbContext.StockTransfer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"StockTransfer with ID {entity.id} not found in database.");

            var updatedValues = StockTransferMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.StockTransfer>> FindAsync(Expression<Func<entities.StockTransfer, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.StockTransfer, entityframework.StockTransfer>(predicate);

            var efStockTransfer = await _dbContext.StockTransfer
                .Where(efPredicate)
                .ToListAsync();

            var result = efStockTransfer.Select(StockTransferMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid StockTransfer ID.", nameof(id));

            var stockTransfer = await _dbContext.StockTransfer
                .FirstOrDefaultAsync(e => e.Id == id);

            if (stockTransfer == null)
                throw new InvalidOperationException($"StockTransfer with ID {id} not found.");

            _dbContext.StockTransfer.Remove(stockTransfer);
        }

        public async Task<IEnumerable<entityframework.StockTransfer>> GetAllStockTransfersRawAsync()
        {
            return await _dbContext.StockTransfer
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
