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
    public class DeliveryReceiptRepository : IDeliveryReceiptRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public DeliveryReceiptRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.DeliveryReceiptMaster>> GetAllAsync()
        {
            var efDelivery = await GetAllDeliveryRawAsync();

            if (efDelivery == null || !efDelivery.Any())
                return Enumerable.Empty<entities.DeliveryReceiptMaster>();

            var delivery = efDelivery
                .Select(DeliveryReceiptMapper.MapToEntity)
                .ToList();

            return delivery;
        }

        public async Task<PagedResult<entities.DeliveryReceiptMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var delivery = await GetAllDeliveryRawAsync();

            if (delivery == null || !delivery.Any())
                return new PagedResult<entities.DeliveryReceiptMaster>
                {
                    Data = Enumerable.Empty<entities.DeliveryReceiptMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = delivery.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = delivery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(DeliveryReceiptMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.DeliveryReceiptMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.DeliveryReceiptMaster> GetByIdAsync(int id)
        {
            var efDelivery = await GetAllDeliveryRawAsync();

            if (efDelivery == null || !efDelivery.Any())
                return null;

            var delivery = efDelivery.Where(a => a.Id == id)
                .Select(DeliveryReceiptMapper.MapToEntity)
                .FirstOrDefault();

            return delivery;

        }

        public async Task AddAsync(entities.DeliveryReceiptMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Delivery Receipt Master entity cannot be null.");

            var delivery = new entityframework.DeliveryReceiptMasterFile
            {
                Simno = entity.Simno ?? throw new ArgumentNullException(nameof(entity.Simno)),
                CustNo = entity.CustNo ?? throw new ArgumentNullException(nameof(entity.CustNo)),
                Date = entity.Date,
                Terms = entity.Terms,
                PrepBy = entity.PrepBy,
                ApprBy = entity.ApprBy,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                Recuring = entity.Recuring,
                FooterText = entity.FooterText,
                DeliveryCost = entity.DeliveryCost,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                RecStatus = entity.RecStatus,
                Vat = entity.Vat,
                Total = entity.Total,
                SubTotal = entity.SubTotal,
                DeliveryReceiptDetailFile = entity.DeliveryReceiptDetail?.Select(d => new entityframework.DeliveryReceiptDetailFile
                {
                    Drdno = d.Drdno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyDel = d.QtyDel,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.DeliveryReceiptDetailFile>() // safe fallback
            };

            await _dbContext.DeliveryReceiptMasterFile.AddAsync(delivery);
        }

        public async Task UpdateAsync(entities.DeliveryReceiptMaster? updatedEntity)
        {
            if (updatedEntity == null) throw new ArgumentNullException(nameof(updatedEntity));
            if (updatedEntity.CustNo is null) throw new ArgumentNullException(nameof(updatedEntity.CustNo));
            if (updatedEntity.DeliveryReceiptDetail is null) throw new ArgumentNullException(nameof(updatedEntity.DeliveryReceiptDetail));

            var existing = (await _dbContext.DeliveryReceiptMasterFile
                .Include(x => x.DeliveryReceiptDetailFile)
                .FirstOrDefaultAsync(x => x.Id == updatedEntity.id)) ?? throw new InvalidOperationException("Delivery Receipt not found");

            existing.Simno = updatedEntity.Simno;
            existing.CustNo = updatedEntity.CustNo;
            existing.Date = updatedEntity.Date;
            existing.Terms = updatedEntity.Terms;
            existing.PrepBy = updatedEntity.PrepBy;
            existing.ApprBy = updatedEntity.ApprBy;
            existing.Remarks = updatedEntity.Remarks;
            existing.RecStatus = updatedEntity.RecStatus;
            existing.Comments = updatedEntity.Comments;
            existing.TermsAndCondition = updatedEntity.TermsAndCondition;
            existing.Recuring = updatedEntity.Recuring;
            existing.FooterText = updatedEntity.FooterText;
            existing.DeliveryCost = updatedEntity.DeliveryCost;
            existing.DisPercent = updatedEntity.DisPercent;
            existing.DisTotal = updatedEntity.DisTotal;
            existing.Vat = updatedEntity.Vat;
            existing.Total = updatedEntity.Total;
            existing.SubTotal = updatedEntity.SubTotal;

            existing.DeliveryReceiptDetailFile.Clear();

            foreach (var d in updatedEntity.DeliveryReceiptDetail)
            {
                existing.DeliveryReceiptDetailFile.Add(new entityframework.DeliveryReceiptDetailFile
                {
                    Drdno = d.Drdno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyDel = d.QtyDel,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.DeliveryReceiptMaster>> FindAsync(Expression<Func<entities.DeliveryReceiptMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.DeliveryReceiptMaster, entityframework.DeliveryReceiptMasterFile>(predicate);

            var efDelivery = await _dbContext.DeliveryReceiptMasterFile
                .Where(efPredicate)
                .ToListAsync();

            if (efDelivery == null || !efDelivery.Any())
                return Enumerable.Empty<entities.DeliveryReceiptMaster>();

            var result = efDelivery.Select(DeliveryReceiptMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid DeliveryReceiptMasterFile ID.", nameof(id));

            var efDelivery = await _dbContext.DeliveryReceiptMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efDelivery == null)
                throw new InvalidOperationException($"DeliveryReceiptMasterFile with ID {id} not found.");

            _dbContext.DeliveryReceiptMasterFile.Remove(efDelivery);
        }

        public async Task<IEnumerable<entityframework.DeliveryReceiptMasterFile>> GetAllDeliveryRawAsync()
        {
            return await _dbContext.DeliveryReceiptMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
