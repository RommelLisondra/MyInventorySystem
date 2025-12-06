//using Microsoft.EntityFrameworkCore;
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
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SalesReturnRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SalesReturnMaster>> GetAllAsync()
        {
            var efsalesReturn = await GetAllSalesReturnRawAsync();

            if (efsalesReturn == null || !efsalesReturn.Any())
                return Enumerable.Empty<entities.SalesReturnMaster>();

            var salesReturn = efsalesReturn
                .Select(SalesReturnMapper.MapToEntity)
                .ToList();

            return salesReturn;
        }

        public async Task<PagedResult<entities.SalesReturnMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var salesReturn = await GetAllSalesReturnRawAsync();

            if (salesReturn == null || !salesReturn.Any())
                return new PagedResult<entities.SalesReturnMaster>
                {
                    Data = Enumerable.Empty<entities.SalesReturnMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = salesReturn.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = salesReturn
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SalesReturnMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SalesReturnMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SalesReturnMaster> GetByIdAsync(int id)
        {
            var efsalesReturn = await GetAllSalesReturnRawAsync();

            if (efsalesReturn == null || !efsalesReturn.Any())
                return null;

            var salesReturn = efsalesReturn
                .Select(SalesReturnMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return salesReturn;
        }

        public async Task AddAsync(entities.SalesReturnMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Sales Invoice Master entity cannot be null.");

            var salesReturn = new entityframework.SalesReturnMasterFile
            {
                Srmno = entity.Srmno ?? throw new ArgumentNullException(nameof(entity.Srmno)),
                CustNo = entity.CustNo ?? throw new ArgumentNullException(nameof(entity.CustNo)),
                Date = entity.Date,
                Terms = entity.Terms,
                PreparedBy = entity.PrepBy,
                ApprovedBy = entity.ApprBy,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                Balance = entity.Balance,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                RecStatus = entity.RecStatus,
                SalesReturnDetailFile = entity.SalesReturnDetailFile?.Select(d => new entityframework.SalesReturnDetailFile
                {
                    Srmno = d.Srdno,
                    ItemDetailCode = d.ItemDetailCode,
                    Qty = d.QtyRet,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.SalesReturnDetailFile>() // safe fallback
            };

            await _dbContext.SalesReturnMasterFile.AddAsync(salesReturn);
        }

        public async Task UpdateAsync(entities.SalesReturnMaster? entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.CustNo is null)
                throw new ArgumentNullException(nameof(entity.CustNo));

            if (entity.SalesReturnDetailFile is null)
                throw new ArgumentNullException(nameof(entity.SalesReturnDetailFile));

            var existing = (await _dbContext.SalesReturnMasterFile
                .Include(x => x.SalesReturnDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Sales Invoice Master not found");

            // Update master fields

            existing.Srmno = entity.Srmno ?? throw new ArgumentNullException(nameof(entity.Srmno));
            existing.CustNo = entity.CustNo ?? throw new ArgumentNullException(nameof(entity.CustNo));
            existing.Date = entity.Date;
            existing.Terms = entity.Terms;
            existing.PreparedBy = entity.PrepBy;
            existing.ApprovedBy = entity.ApprBy;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.FooterText = entity.FooterText;
            existing.Recuring = entity.Recuring;
            existing.Total = entity.Total;
            existing.DisPercent = entity.DisPercent;
            existing.DisTotal = entity.DisTotal;
            existing.Balance = entity.Balance;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.RecStatus = entity.RecStatus;

            // Sync details
            existing.SalesReturnDetailFile.Clear();

            foreach (var d in entity.SalesReturnDetailFile)
            {
                existing.SalesReturnDetailFile.Add(new entityframework.SalesReturnDetailFile
                {
                    Id = d.id,
                    Srmno = d.Srdno,
                    ItemDetailCode = d.ItemDetailCode,
                    Qty = d.QtyRet,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.SalesReturnMaster>> FindAsync(Expression<Func<entities.SalesReturnMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SalesReturnMaster, entityframework.SalesReturnMasterFile>(predicate);

            var efsalesReturn = await _dbContext.SalesReturnMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efsalesReturn.Select(SalesReturnMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Sales Invoice detailed ID.", nameof(id));

            var efsalesReturn = await _dbContext.SalesReturnMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efsalesReturn == null)
                throw new InvalidOperationException($"Sales Invoice detailed with ID {id} not found.");

            _dbContext.SalesReturnMasterFile.Remove(efsalesReturn);
        }

        public async Task<IEnumerable<entityframework.SalesReturnMasterFile>> GetAllSalesReturnRawAsync()
        {
            return await _dbContext.SalesReturnMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
