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
    public class ItemImageRepository : IItemImageRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ItemImageRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ItemImage>> GetAllAsync()
        {
            var efItemImage = await GetAllItemImageRawAsync();

            if (efItemImage == null || !efItemImage.Any())
                return Enumerable.Empty<entities.ItemImage>();

            var itemImages = efItemImage
                .Select(ItemMapper.MapToItemImage)
                .ToList();

            return itemImages;
        }

        public async Task<PagedResult<entities.ItemImage>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var itemImages = await GetAllItemImageRawAsync();

            if (itemImages == null || !itemImages.Any())
                return new PagedResult<entities.ItemImage>
                {
                    Data = Enumerable.Empty<entities.ItemImage>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = itemImages.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = itemImages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ItemMapper.MapToItemImage)
                .ToList();

            return new PagedResult<entities.ItemImage>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ItemImage> GetByIdAsync(int id)
        {
            var efItemImage = await GetAllItemImageRawAsync();

            if (efItemImage == null || !efItemImage.Any())
                return null;

            var itemImages = efItemImage
                .Select(ItemMapper.MapToItemImage).Where(e => e.id == id)
                .FirstOrDefault();

            return itemImages;
        }

        public async Task AddAsync(entities.ItemImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var ItemImage = ItemMapper.MapToEntityFramework(entity, includeId: false);

            await _dbContext.ItemImage.AddAsync(ItemImage);
        }

        public async Task UpdateAsync(entities.ItemImage? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var toUpdate = await _dbContext.Customer.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Customer with ID {entity.id} not found in database.");

            var updatedValues = ItemMapper.MapToEntityFramework(entity, includeId: true);

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.ItemImage>> FindAsync(Expression<Func<entities.ItemImage, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ItemImage, entityframework.ItemImage>(predicate);

            var efitemImages = await _dbContext.ItemImage
                .Where(efPredicate)
                .ToListAsync();

            var result = efitemImages.Select(ItemMapper.MapToItemImage);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid item detailed ID.", nameof(id));

            var efitemImages = await _dbContext.ItemImage
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efitemImages == null)
                throw new InvalidOperationException($"item detailed with ID {id} not found.");

            _dbContext.ItemImage.Remove(efitemImages);
        }

        public async Task<IEnumerable<entityframework.ItemImage>> GetAllItemImageRawAsync()
        {
            return await _dbContext.ItemImage
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
