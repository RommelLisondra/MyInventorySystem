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
    public class InventoryAdjustmentRepository : IInventoryAdjustmentRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public InventoryAdjustmentRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.InventoryAdjustment>> GetAllAsync()
        {
            var efInventoryAdjustment = await GetAllInventoryAdjustmentsRawAsync();

            if (efInventoryAdjustment == null || !efInventoryAdjustment.Any())
                return Enumerable.Empty<entities.InventoryAdjustment>();

            var inventoryAdjustment = efInventoryAdjustment
                .Select(InventoryAdjustmentMapper.MapToEntity)
                .ToList();

            return inventoryAdjustment;
        }

        public async Task<PagedResult<entities.InventoryAdjustment>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var inventoryAdjustment = await GetAllInventoryAdjustmentsRawAsync();

            if (inventoryAdjustment == null || !inventoryAdjustment.Any())
                return new PagedResult<entities.InventoryAdjustment>
                {
                    Data = Enumerable.Empty<entities.InventoryAdjustment>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = inventoryAdjustment.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = inventoryAdjustment
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(InventoryAdjustmentMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.InventoryAdjustment>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.InventoryAdjustment> GetByIdAsync(int id)
        {
            var efInventoryAdjustment = await GetAllInventoryAdjustmentsRawAsync();

            if (efInventoryAdjustment == null || !efInventoryAdjustment.Any())
                return null;

            var inventoryAdjustment = efInventoryAdjustment
                .Select(InventoryAdjustmentMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return inventoryAdjustment;
        }

        public async Task AddAsync(entities.InventoryAdjustment? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryAdjustment entity cannot be null.");

            var inventoryAdjustment = InventoryAdjustmentMapper.MapToEntityFramework(entity, false);

            await _dbContext.InventoryAdjustment.AddAsync(inventoryAdjustment);
        }

        public async Task UpdateAsync(entities.InventoryAdjustment? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "InventoryAdjustment entity cannot be null.");

            var toUpdate = await _dbContext.InventoryAdjustment.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"InventoryAdjustment with ID {entity.id} not found in database.");

            var updatedValues = InventoryAdjustmentMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.InventoryAdjustment>> FindAsync(Expression<Func<entities.InventoryAdjustment, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.InventoryAdjustment, entityframework.InventoryAdjustment>(predicate);

            var efInventoryAdjustment = await _dbContext.InventoryAdjustment
                .Where(efPredicate)
                .ToListAsync();

            var result = efInventoryAdjustment.Select(InventoryAdjustmentMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid InventoryAdjustment ID.", nameof(id));

            var inventoryAdjustment = await _dbContext.InventoryAdjustment
                .FirstOrDefaultAsync(e => e.Id == id);

            if (inventoryAdjustment == null)
                throw new InvalidOperationException($"InventoryAdjustment with ID {id} not found.");

            _dbContext.InventoryAdjustment.Remove(inventoryAdjustment);
        }

        public async Task<IEnumerable<entityframework.InventoryAdjustment>> GetAllInventoryAdjustmentsRawAsync()
        {
            return await _dbContext.InventoryAdjustment
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
