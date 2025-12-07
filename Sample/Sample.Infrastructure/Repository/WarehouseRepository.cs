using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
//using Warehouse = Sample.Domain.Entities.Warehouse;
//using Warehouses = Sample.Infrastructure.EntityFramework.Warehouse;

using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public WarehouseRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Warehouse>> GetAllAsync()
        {
            var efWarehouse = await GetAllWarehousesRawAsync();

            if (efWarehouse == null || !efWarehouse.Any())
                return Enumerable.Empty<entities.Warehouse>();

            var warehouses = efWarehouse
                .Select(WarehouseMapper.MapToEntity)
                .ToList();

            return warehouses;
        }

        public async Task<PagedResult<entities.Warehouse>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efWarehouse = await GetAllWarehousesRawAsync();

            if (efWarehouse == null || !efWarehouse.Any())
                return new PagedResult<entities.Warehouse>
                {
                    Data = Enumerable.Empty<entities.Warehouse>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efWarehouse.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = efWarehouse
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(WarehouseMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Warehouse>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Warehouse> GetByIdAsync(int id)
        {
            var efWarehouse = await GetAllWarehousesRawAsync();

            if (efWarehouse == null || !efWarehouse.Any())
                return null;

            var warehouses = efWarehouse
                .Select(WarehouseMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return warehouses;
        }

        public async Task<entities.Warehouse> GetByWarehouseCodeAsync(string warehouseCode)
        {
            var efWarehouse = await GetAllWarehousesRawAsync();

            if (efWarehouse == null || !efWarehouse.Any())
                return null;

            var warehouses = efWarehouse
                .Select(WarehouseMapper.MapToEntity).Where(e => e.WareHouseCode == warehouseCode)
                .FirstOrDefault();

            return warehouses;
        }

        public async Task AddAsync(entities.Warehouse? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Warehouse entity cannot be null.");

            var warehouses = WarehouseMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.Warehouse.AddAsync(warehouses);
        }

        public async Task UpdateAsync(entities.Warehouse? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Warehouse entity cannot be null.");

            var toUpdate = await _dbContext.Warehouse.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Warehouse with ID {entity.id} not found in database.");

            var updatedValues = WarehouseMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Warehouse>> FindAsync(Expression<Func<entities.Warehouse, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Warehouse, entityframework.Warehouse>(predicate);

            var warehouses = await _dbContext.Warehouse
                .Where(efPredicate)
                .ToListAsync();

            var result = warehouses.Select(WarehouseMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Warehouse ID.", nameof(id));

            var warehouses = await _dbContext.Warehouse
                .FirstOrDefaultAsync(e => e.Id == id);

            if (warehouses == null)
                throw new InvalidOperationException($"Warehouse with ID {id} not found.");

            _dbContext.Warehouse.Remove(warehouses);
        }

        public async Task<IEnumerable<entityframework.Warehouse>> GetAllWarehousesRawAsync()
        {
            return await _dbContext.Warehouse
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
