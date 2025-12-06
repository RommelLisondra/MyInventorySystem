using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
//using Customer = Sample.Domain.Entities.Customer;
//using Customers = Sample.Infrastructure.EntityFramework.Customer;

using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;


namespace Sample.Infrastructure.Repository
{
    public class PurchaseReturnRepository : IPurchaseReturnRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public PurchaseReturnRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.PurchaseReturnMaster>> GetAllAsync()
        {
            var efPurchaseReturn = await GetAllPurchaseReturnRawAsync();

            if (efPurchaseReturn == null || !efPurchaseReturn.Any())
                return Enumerable.Empty<entities.PurchaseReturnMaster>();

            var purchaseReturn = efPurchaseReturn
                .Select(PurchaseReturnMapper.MapToEntity)
                .ToList();

            return purchaseReturn;
        }

        public async Task<PagedResult<entities.PurchaseReturnMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var purchaseReturn = await GetAllPurchaseReturnRawAsync();

            if (purchaseReturn == null || !purchaseReturn.Any())
                return new PagedResult<entities.PurchaseReturnMaster>
                {
                    Data = Enumerable.Empty<entities.PurchaseReturnMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = purchaseReturn.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = purchaseReturn
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(PurchaseReturnMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.PurchaseReturnMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.PurchaseReturnMaster> GetByIdAsync(int id)
        {
            var efPurchaseReturn = await GetAllPurchaseReturnRawAsync();

            if (efPurchaseReturn == null || !efPurchaseReturn.Any())
                return null;

            var purchaseReturn = efPurchaseReturn
                .Select(PurchaseReturnMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return purchaseReturn;
        }

        public async Task AddAsync(entities.PurchaseReturnMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Purchase Return Master entity cannot be null.");

            var purchasereturn = new entityframework.PurchaseReturnMasterFile
            {
                Rrno = entity.Rrno,
                RefNo = entity.RefNo,
                SupplierNo = entity.SupplierNo,
                Date = entity.Date,
                Terms = entity.Terms,
                TypesOfPay = entity.TypesOfPay,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                PreparedBy = entity.PrepBy,
                ApprovedBy = entity.ApprBy,
                RecStatus = entity.RecStatus,
                PurchaseReturnDetailFile = entity.PurchaseReturnDetailFile?.Select(d => new entityframework.PurchaseReturnDetailFile
                {
                    Prmno = d.PretDno,
                    ItemDetailCode = d.ItemDetailCode,
                    Quantity = d.QtyRet,
                    Amount = d.Amount,
                    Uprice = d.Uprice,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.PurchaseReturnDetailFile>() // safe fallback    
            };

            await _dbContext.PurchaseReturnMasterFile.AddAsync(purchasereturn);
        }

        public async Task UpdateAsync(entities.PurchaseReturnMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Rrno is null) throw new ArgumentNullException(nameof(entity.Rrno));
            if (entity.RefNo is null) throw new ArgumentNullException(nameof(entity.RefNo));
            if (entity.PrepBy is null) throw new ArgumentNullException(nameof(entity.PrepBy));
            if (entity.ApprBy is null) throw new ArgumentNullException(nameof(entity.ApprBy));
            if (entity.PurchaseReturnDetailFile is null) throw new ArgumentNullException(nameof(entity.PurchaseReturnDetailFile));

            var existing = (await _dbContext.PurchaseReturnMasterFile
                .Include(x => x.PurchaseReturnDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Purchase Return Master not found");

            existing.Rrno = entity.Rrno;
            existing.RefNo = entity.RefNo;
            existing.Date = entity.Date;
            existing.Terms = entity.Terms;

            existing.TypesOfPay = entity.TypesOfPay;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.FooterText = entity.FooterText;
            existing.Recuring = entity.Recuring;
            existing.Total = entity.Total;
            existing.DisPercent = entity.DisPercent;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.RecStatus = entity.RecStatus;
            existing.PreparedBy = entity.PrepBy;
            existing.ApprovedBy = entity.ApprBy;

            existing.PurchaseReturnDetailFile.Clear();

            foreach (var d in entity.PurchaseReturnDetailFile)
            {
                existing.PurchaseReturnDetailFile.Add(new entityframework.PurchaseReturnDetailFile
                {
                    Id = d.id,
                    Prmno = d.PretDno,
                    ItemDetailCode = d.ItemDetailCode,
                    Quantity = d.QtyRet,
                    Amount = d.Amount,
                    Uprice = d.Uprice,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.PurchaseReturnMaster>> FindAsync(Expression<Func<entities.PurchaseReturnMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.PurchaseReturnMaster, entityframework.PurchaseReturnMasterFile>(predicate);

            var efpurchaseReturn = await _dbContext.PurchaseReturnMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efpurchaseReturn.Select(PurchaseReturnMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Purchase Return detailed ID.", nameof(id));

            var purchaseReturn = await _dbContext.PurchaseReturnMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (purchaseReturn == null)
                throw new InvalidOperationException($"Purchase Return detailed with ID {id} not found.");

            _dbContext.PurchaseReturnMasterFile.Remove(purchaseReturn);
        }

        public async Task<IEnumerable<entityframework.PurchaseReturnMasterFile>> GetAllPurchaseReturnRawAsync()
        {
            return await _dbContext.PurchaseReturnMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
