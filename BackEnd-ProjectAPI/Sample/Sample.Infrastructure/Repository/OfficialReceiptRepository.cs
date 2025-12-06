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
    public class OfficialReceiptRepository : IOfficialReceiptRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public OfficialReceiptRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.OfficialReceiptMaster>> GetAllAsync()
        {
            var efofficialreceipt = await GetAllOfficialReceiptRawAsync();

            if (efofficialreceipt == null || !efofficialreceipt.Any())
                return Enumerable.Empty<entities.OfficialReceiptMaster>();

            var officialreceipt = efofficialreceipt
                .Select(OfficialReceiptMapper.MapToEntity)
                .ToList();

            return officialreceipt;
        }

        public async Task<PagedResult<entities.OfficialReceiptMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var officialreceipt = await GetAllOfficialReceiptRawAsync();

            if (officialreceipt == null || !officialreceipt.Any())
                return new PagedResult<entities.OfficialReceiptMaster>
                {
                    Data = Enumerable.Empty<entities.OfficialReceiptMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = officialreceipt.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = officialreceipt
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(OfficialReceiptMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.OfficialReceiptMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.OfficialReceiptMaster> GetByIdAsync(int id)
        {
            var efofficialreceipt = await GetAllOfficialReceiptRawAsync();

            if (efofficialreceipt == null || !efofficialreceipt.Any())
                return null;

            var officialreceipt = efofficialreceipt
                .Select(OfficialReceiptMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return officialreceipt;
        }

        public async Task AddAsync(entities.OfficialReceiptMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Official Receipt Master entity cannot be null.");

            var officialReceipt = new entityframework.OfficialReceiptMasterFile
            {
                Orno = entity.Orno,
                Date = entity.Date,
                CustNo = entity.CustNo ?? throw new ArgumentNullException(nameof(entity.CustNo)),
                PreparedBy = entity.PrepBy,
                RecStatus = entity.RecStatus,
                TotAmtPaid = entity.TotalAmtPaid,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                FormPay = entity.FormPay,
                CashAmt = entity.CashAmt,
                CheckAmt = entity.CheckAmt,
                CheckNo = entity.CheckNo,
                BankName = entity.BankName,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                OfficialReceiptDetailFile = entity.OfficialReceiptDetailFile?.Select(d => new entityframework.OfficialReceiptDetailFile
                {
                    Orno = d.Ordno,
                    Simno = d.Simno,
                    AmountPaid = d.AmountPaid,
                    AmountDue = d.AmountDue,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.OfficialReceiptDetailFile>() // safe fallback
            };

            await _dbContext.OfficialReceiptMasterFile.AddAsync(officialReceipt);
        }

        public async Task UpdateAsync(entities.OfficialReceiptMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.CustNo is null) throw new ArgumentNullException(nameof(entity.CustNo));
            if (entity.OfficialReceiptDetailFile is null) throw new ArgumentNullException(nameof(entity.OfficialReceiptDetailFile));

            var existing = (await _dbContext.OfficialReceiptMasterFile
                .Include(x => x.OfficialReceiptDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Official Receipt Master not found");

            existing.Date = entity.Date;
            existing.PreparedBy = entity.PrepBy;
            existing.RecStatus = entity.RecStatus;
            existing.TotAmtPaid = entity.TotalAmtPaid;
            existing.DisPercent = entity.DisPercent;
            existing.DisTotal = entity.DisTotal;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.FormPay = entity.FormPay;
            existing.CashAmt = entity.CashAmt;
            existing.CheckAmt = entity.CheckAmt;
            existing.CheckNo = entity.CheckNo;
            existing.BankName = entity.BankName;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.FooterText = entity.FooterText;
            existing.Recuring = entity.Recuring;
            existing.RecStatus = entity.RecStatus;

            // Sync details
            existing.OfficialReceiptDetailFile.Clear();

            foreach (var d in entity.OfficialReceiptDetailFile)
            {
                existing.OfficialReceiptDetailFile.Add(new entityframework.OfficialReceiptDetailFile
                {
                    AmountPaid = d.AmountPaid,
                    AmountDue = d.AmountDue,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.OfficialReceiptMaster>> FindAsync(Expression<Func<entities.OfficialReceiptMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.OfficialReceiptMaster, entityframework.OfficialReceiptMasterFile>(predicate);

            var efofficialreceipt = await _dbContext.OfficialReceiptMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efofficialreceipt.Select(OfficialReceiptMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Official Receipt detailed ID.", nameof(id));

            var efofficialreceipt = await _dbContext.OfficialReceiptMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efofficialreceipt == null)
                throw new InvalidOperationException($"Official Receipt detailed with ID {id} not found.");

            _dbContext.OfficialReceiptMasterFile.Remove(efofficialreceipt);
        }

        public async Task<IEnumerable<entityframework.OfficialReceiptMasterFile>> GetAllOfficialReceiptRawAsync()
        {
            return await _dbContext.OfficialReceiptMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
