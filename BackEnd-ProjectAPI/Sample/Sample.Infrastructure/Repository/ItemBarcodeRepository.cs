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
    public class ItemBarcodeRepository : IItemBarcodeRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemBarcodeRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemBarcode>> GetAllAsync()
        {
            var efItemBarcode = await GetAllItemBarcodesRawAsync();

            if (efItemBarcode == null || !efItemBarcode.Any())
                return Enumerable.Empty<entities.ItemBarcode>();

            var itemBarcode = efItemBarcode
                .Select(ItemBarcodeMapper.MapToEntity)
                .ToList();

            return itemBarcode;
        }

        public async Task<PagedResult<entities.ItemBarcode>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemBarcode = await GetAllItemBarcodesRawAsync();

            if (itemBarcode == null || !itemBarcode.Any())
                return new PagedResult<entities.ItemBarcode>
                {
                    Data = Enumerable.Empty<entities.ItemBarcode>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemBarcode.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemBarcode
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemBarcodeMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ItemBarcode>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemBarcode> GetByIdAsync(int id)
        {
            var efItemBarcode = await GetAllItemBarcodesRawAsync();

            if (efItemBarcode == null || !efItemBarcode.Any())
                return null;

            var itemBarcode = efItemBarcode
                .Select(ItemBarcodeMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return itemBarcode;
        }

        public async Task AddAsync(entities.ItemBarcode? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemBarcode entity cannot be null.");

            var itemBarcode = ItemBarcodeMapper.MapToEntityFramework(entity, false);

            await _dbContext.ItemBarcode.AddAsync(itemBarcode);
        }

        public async Task UpdateAsync(entities.ItemBarcode? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ItemBarcode entity cannot be null.");

            var toUpdate = await _dbContext.ItemBarcode.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"ItemBarcode with ID {entity.id} not found in database.");

            var updatedValues = ItemBarcodeMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemBarcode>> FindAsync(Expression<Func<entities.ItemBarcode, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemBarcode, entityframework.ItemBarcode>(predicate);

            var efItemBarcode = await _dbContext.ItemBarcode
                .Where(efPredicate)
                .ToListAsync();

            var result = efItemBarcode.Select(ItemBarcodeMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ItemBarcode ID.", nameof(id));

            var itemBarcode = await _dbContext.ItemBarcode
                .FirstOrDefaultAsync(e => e.Id == id);

            if (itemBarcode == null)
                throw new InvalidOperationException($"ItemBarcode with ID {id} not found.");

            _dbContext.ItemBarcode.Remove(itemBarcode);
        }

        public async Task<IEnumerable<entityframework.ItemBarcode>> GetAllItemBarcodesRawAsync()
        {
            return await _dbContext.ItemBarcode.Where(e => e.IsActive).ToListAsync();
        }
    }
}
