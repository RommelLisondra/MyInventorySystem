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
    public class ItemDetailRepository : IItemDetailRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemDetailRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemDetail>> GetAllAsync()
        {
            var efItemDetails = await GetAllItemDetailRawAsync();

            if (efItemDetails == null || !efItemDetails.Any())
                return Enumerable.Empty<entities.ItemDetail>();

            var itemDetailNos = efItemDetails.Select(x => x.ItemDetailNo).ToList();
            var inventories = await _dbContext.ItemInventory
                .Where(i => itemDetailNos.Contains(i.ItemDetailCode))
                .ToListAsync();

            var itemIds = efItemDetails.Select(x => x.ItemId).Distinct().ToList();
            var itemMasters = await _dbContext.Item
                .Where(im => itemIds.Contains(im.ItemCode))
                .ToListAsync();

            // Map sa domain entities
            var result = efItemDetails.Select(efItemDetail =>
            {
                var entity = ItemMapper.MapToEntity(efItemDetail);

                entity.ItemInventory = inventories
                    .Where(inv => inv.ItemDetailCode == efItemDetail.ItemDetailNo)
                    .Select(ItemMapper.MapToEntityInventory)
                    .ToList();

                var master = itemMasters.FirstOrDefault(im => im.ItemCode == efItemDetail.ItemId);
                entity.ItemMaster = master != null ? ItemMapper.MapToItem(master) : null;

                return entity;
            }).ToList();

            return result;
        }


        public async Task<PagedResult<entities.ItemDetail>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var efItemDetails = await GetAllItemDetailRawAsync();

            if (efItemDetails == null || !efItemDetails.Any())
                return new PagedResult<entities.ItemDetail>
                {
                    Data = Enumerable.Empty<entities.ItemDetail>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = efItemDetails.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedEfItemDetails = efItemDetails
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var itemDetailNos = pagedEfItemDetails.Select(x => x.ItemDetailNo).ToList();
            var inventories = await _dbContext.ItemInventory
                .Where(i => itemDetailNos.Contains(i.ItemDetailCode))
                .ToListAsync();

            var itemIds = pagedEfItemDetails.Select(x => x.ItemId).Distinct().ToList();
            var itemMasters = await _dbContext.Item
                .Where(im => itemIds.Contains(im.ItemCode))
                .ToListAsync();

            var pagedData = pagedEfItemDetails.Select(efItemDetail =>
            {
                var entity = ItemMapper.MapToEntity(efItemDetail);

                entity.ItemInventory = inventories
                    .Where(inv => inv.ItemDetailCode == efItemDetail.ItemDetailNo)
                    .Select(ItemMapper.MapToEntityInventory)
                    .ToList();

                var master = itemMasters.FirstOrDefault(im => im.ItemCode == efItemDetail.ItemId);
                entity.ItemMaster = master != null ? ItemMapper.MapToItem(master) : null;

                return entity;
            }).ToList();

            return new PagedResult<entities.ItemDetail>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }


        public async Task<entities.ItemDetail> GetByIdAsync(int id)
        {
            var efitemDetail = await GetAllItemDetailRawAsync();

            if (efitemDetail == null || !efitemDetail.Any())
                return null;

            var efItemDetail = efitemDetail
                .FirstOrDefault(e => e.Id == id);

            if (efItemDetail == null)
                return null;

            var itemDetailEntity = ItemMapper.MapToEntity(efItemDetail);

            var inventories = await _dbContext.ItemInventory
                .Where(i => i.ItemDetailCode == efItemDetail.ItemDetailNo)
                .ToListAsync();

            itemDetailEntity.ItemInventory = inventories
                .Select(ItemMapper.MapToEntityInventory)
                .ToList();

            var itemMaster = await _dbContext.Item
                .FirstOrDefaultAsync(im => im.ItemCode == efItemDetail.ItemId);

            itemDetailEntity.ItemMaster = itemMaster != null
                ? ItemMapper.MapToItem(itemMaster)
                : null;

            return itemDetailEntity;
        }

        public async Task<entities.ItemDetail> GetByItemdetailCodeAsync(string itemDetailCode)
        {
            var efitemDetails = await GetAllItemDetailRawAsync();

            if (efitemDetails == null || !efitemDetails.Any())
                return null;

            var efItemDetail = efitemDetails
                .FirstOrDefault(e => e.ItemDetailCode == itemDetailCode);

            if (efItemDetail == null)
                return null;

            var itemDetailEntity = ItemMapper.MapToEntity(efItemDetail);

            var inventories = await _dbContext.ItemInventory
                .Where(i => i.ItemDetailCode == efItemDetail.ItemDetailNo)
                .ToListAsync();

            itemDetailEntity.ItemInventory = inventories
                .Select(ItemMapper.MapToEntityInventory)
                .ToList();

            var itemMaster = await _dbContext.Item
                .FirstOrDefaultAsync(im => im.ItemCode == efItemDetail.ItemId);

            itemDetailEntity.ItemMaster = itemMaster != null
                ? ItemMapper.MapToItem(itemMaster)
                : null;

            return itemDetailEntity;
        }


        public async Task AddAsync(entities.ItemDetail? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var itemDetail = ItemMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.ItemDetail.AddAsync(itemDetail);
        }

        public async Task UpdateAsync(entities.ItemDetail? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemDetail entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemDetail with ID {entity.id} not found in database.");

            var updatedValues = ItemMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task UpdateFieldAsync<TProperty>(int itemId, string propertyName, TProperty newValue)
        {
            var item = await _dbContext.ItemDetail.FirstOrDefaultAsync(c => c.Id == itemId);
            if (item == null)
                throw new InvalidOperationException($"Item Dtail {itemId} not found.");

            var entry = _dbContext.Entry(item);
            entry.Property(propertyName).CurrentValue = newValue;
            item.ModifiedDateTime = DateTime.UtcNow;
        }

        public async Task<IEnumerable<entities.ItemDetail>> FindAsync(Expression<Func<entities.ItemDetail, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var efPredicate = Helpers.ConvertPredicate<entities.ItemDetail, entityframework.ItemDetail>(predicate);

            var efItemDetails = await _dbContext.ItemDetail
                .Where(efPredicate)
                .ToListAsync();

            if (!efItemDetails.Any())
                return Enumerable.Empty<entities.ItemDetail>();

            var itemDetailCodes = efItemDetails.Select(d => d.ItemDetailNo).ToList();
            var efInventories = await _dbContext.ItemInventory
                .Where(inv => itemDetailCodes.Contains(inv.ItemDetailCode))
                .ToListAsync();

            // Step 3: Load ItemMaster for all ItemDetails
            var itemIds = efItemDetails.Select(d => d.ItemId).Distinct().ToList();
            var efItemMasters = await _dbContext.Item
                .Where(im => itemIds.Contains(im.ItemCode))
                .ToListAsync();

            // Step 4: Map to domain entities
            var result = efItemDetails.Select(itemDetail =>
            {
                var entity = ItemMapper.MapToEntity(itemDetail);

                // Assign inventories safely
                entity.ItemInventory = efInventories
                    .Where(inv => inv.ItemDetailCode == itemDetail.ItemDetailNo)
                    .Select(ItemMapper.MapToEntityInventory)
                    .ToList() ?? new List<entities.ItemInventory>();

                // Assign ItemMaster safely
                var master = efItemMasters.FirstOrDefault(im => im.ItemCode == itemDetail.ItemId);

                entity.ItemMaster = master != null
                    ? ItemMapper.MapToItem(master)
                    : null;

                return entity;
            });

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid item detailed ID.", nameof(id));

            var itemDetail = await _dbContext.ItemDetail
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemDetail == null)
                throw new InvalidOperationException($"item detailed with ID {id} not found.");

            _dbContext.ItemDetail.Remove(itemDetail);
        }

        public async Task<IEnumerable<entityframework.ItemDetail>> GetAllItemDetailRawAsync()
        {
            return await _dbContext.ItemDetail
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
