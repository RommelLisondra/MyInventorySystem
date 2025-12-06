using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq;
using System.Linq.Expressions;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class ItemInventoryRepository : IItemInventoryRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemInventoryRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemInventory>> GetAllAsync()
        {
            var data = await _dbContext.ItemInventory
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                )
                .ToListAsync();

            return data.Select(x =>
            {
                var entity = ItemMapper.MapToEntityInventory(x.inv);

                // ALWAYS has detail because INNER JOIN
                entity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(x.det);

                // ALWAYS has master because INNER JOIN
                entity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(x.itm);

                return entity;
            });
        }

        public async Task<PagedResult<entities.ItemInventory>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var query = _dbContext.ItemInventory
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                );

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagedData = data.Select(x =>
            {
                var entity = ItemMapper.MapToEntityInventory(x.inv);

                entity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(x.det);
                entity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(x.itm);

                return entity;
            }).ToList();

            return new PagedResult<entities.ItemInventory>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemInventory> GetByIdAsync(int id)
        {
            var query = await _dbContext.ItemInventory
                .Where(inv => inv.Id == id)
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                )
                .FirstOrDefaultAsync();

            if (query == null)
                return null;

            // Map ItemInventory first
            var inventoryEntity = ItemMapper.MapToEntityInventory(query.inv);

            // Map ItemDetail
            inventoryEntity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(query.det);

            // Map ItemMaster
            inventoryEntity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(query.itm);

            return inventoryEntity;
        }

        public async Task<entities.ItemInventory> GetByItemdetailCodeAsync(string itemDetailCode)
        {
            var query = await _dbContext.ItemInventory
                .Where(inv => inv.ItemDetailCode == itemDetailCode)
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                )
                .FirstOrDefaultAsync();

            if (query == null)
                return null;

            // Map ItemInventory first
            var inventoryEntity = ItemMapper.MapToEntityInventory(query.inv);

            // Map ItemDetail
            inventoryEntity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(query.det);

            // Map ItemMaster
            inventoryEntity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(query.itm);

            return inventoryEntity;
        }

        public async Task<entities.ItemInventory> GetByItemdetailCodeAndWarehouseCodeAsync(string itemDetailCode, string warehouseCode)
        {
            var query = await _dbContext.ItemInventory
                .Where(inv => inv.ItemDetailCode == itemDetailCode && inv.WarehouseCode == warehouseCode)
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                )
                .FirstOrDefaultAsync();

            if (query == null)
                return null;

            // Map ItemInventory first
            var inventoryEntity = ItemMapper.MapToEntityInventory(query.inv);

            // Map ItemDetail
            inventoryEntity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(query.det);

            // Map ItemMaster
            inventoryEntity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(query.itm);

            return inventoryEntity;
        }

        public async Task AddAsync(entities.ItemInventory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var itemDetail = ItemMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.ItemInventory.AddAsync(itemDetail);
        }

        public async Task UpdateAsync(entities.ItemInventory? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemDetail entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemDetail with ID {entity.id} not found in database.");

            var updatedValues = ItemMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemInventory>> FindAsync(Expression<Func<entities.ItemInventory, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var efPredicate = Helpers.ConvertPredicate<entities.ItemInventory, entityframework.ItemInventory>(predicate);

            var query = _dbContext.ItemInventory
                .Where(efPredicate)
                .Join(
                    _dbContext.ItemDetail,
                    inv => inv.ItemDetailCode,
                    det => det.ItemDetailNo,
                    (inv, det) => new { inv, det }
                )
                .Join(
                    _dbContext.Item,
                    x => x.det.ItemId,
                    itm => itm.Id.ToString(),
                    (x, itm) => new { x.inv, x.det, itm }
                );

            var data = await query.ToListAsync();

            var result = data
                .GroupBy(x => x.det.ItemDetailNo) 
                .Select(g =>
                {
                    var first = g.First();
                    var entity = ItemMapper.MapToEntityInventory(first.inv);

                    //entity.ItemDetailCodeNavigation = g
                    //    .Select(x => ItemMapper.MapToEntity(x.det))
                    //    .ToList();

                    entity.ItemDetailCodeNavigation = ItemMapper.MapToEntity(first.det);

                    entity.ItemDetailCodeNavigation.ItemMaster = ItemMapper.MapToItem(first.itm);

                    return entity;
                })
                .ToList();

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid item detailed ID.", nameof(id));

            var itemDetail = await _dbContext.ItemInventory
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemDetail == null)
                throw new InvalidOperationException($"item detailed with ID {id} not found.");

            _dbContext.ItemInventory.Remove(itemDetail);
        }

        public async Task<IEnumerable<entityframework.ItemInventory>> GetAllItemDetailRawAsync()
        {
            return await _dbContext.ItemInventory
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
