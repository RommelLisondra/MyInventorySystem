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
    public class SalesInvoiceRepository : ISalesInvoiceRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SalesInvoiceRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SalesInvoiceMaster>> GetAllAsync()
        {
            var efsalesInvoice = await GetAllSalesInvoiceRawAsync();

            if (efsalesInvoice == null || !efsalesInvoice.Any())
                return Enumerable.Empty<entities.SalesInvoiceMaster>();

            var salesInvoice = efsalesInvoice
                .Select(SalesInvoiceMapper.MapToEntity)
                .ToList();

            return salesInvoice;
        }

        public async Task<PagedResult<entities.SalesInvoiceMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var salesInvoice = await GetAllSalesInvoiceRawAsync();

            if (salesInvoice == null || !salesInvoice.Any())
                return new PagedResult<entities.SalesInvoiceMaster>
                {
                    Data = Enumerable.Empty<entities.SalesInvoiceMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = salesInvoice.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = salesInvoice
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SalesInvoiceMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SalesInvoiceMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SalesInvoiceMaster> GetByIdAsync(int id)
        {
            var efsalesInvoice = await GetAllSalesInvoiceRawAsync();

            if (efsalesInvoice == null || !efsalesInvoice.Any())
                return null;

            var salesInvoice = efsalesInvoice
                .Select(SalesInvoiceMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return salesInvoice;
        }

        public async Task AddAsync(entities.SalesInvoiceMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Sales Invoice Master entity cannot be null.");

            var salesInvoice = new entityframework.SalesInvoiceMasterFile
            {
                Simno = entity.Simno ?? throw new ArgumentNullException(nameof(entity.Simno)),
                Somno = entity.Somno ?? throw new ArgumentNullException(nameof(entity.Somno)),
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
                DeliveryCost = entity.DeliveryCost,
                Balance = entity.Balance,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                DueDate = entity.DueDate,
                OrrecStatus = entity.OrrecStatus,
                DrrecStatus = entity.DrrecStatus,
                SrrecStatus = entity.SrrecStatus,
                RecStatus = entity.RecStatus,
                SalesInvoiceDetailFile = entity.SalesInvoiceDetail?.Select(d => new entityframework.SalesInvoiceDetailFile
                {
                    Simno = d.Sidno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyInv = d.QtyInv,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.SalesInvoiceDetailFile>() // safe fallback
            };

            await _dbContext.SalesInvoiceMasterFile.AddAsync(salesInvoice);
        }

        public async Task UpdateAsync(entities.SalesInvoiceMaster? entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.CustNo is null)
                throw new ArgumentNullException(nameof(entity.CustNo));

            if (entity.SalesInvoiceDetail is null)
                throw new ArgumentNullException(nameof(entity.SalesInvoiceDetail));

            var existing = (await _dbContext.SalesInvoiceMasterFile
                .Include(x => x.SalesInvoiceDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Sales Invoice Master not found");

            existing.Simno = entity.Simno ?? throw new ArgumentNullException(nameof(entity.Simno));
            existing.Somno = entity.Somno ?? throw new ArgumentNullException(nameof(entity.Somno));
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
            existing.DeliveryCost = entity.DeliveryCost;
            existing.Balance = entity.Balance;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.DueDate = entity.DueDate;
            existing.OrrecStatus = entity.OrrecStatus;
            existing.DrrecStatus = entity.DrrecStatus;
            existing.SrrecStatus = entity.SrrecStatus;
            existing.RecStatus = entity.RecStatus;

            // Sync details
            existing.SalesInvoiceDetailFile.Clear();

            foreach (var d in entity.SalesInvoiceDetail)
            {
                existing.SalesInvoiceDetailFile.Add(new entityframework.SalesInvoiceDetailFile
                {
                    Simno = d.Sidno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyInv = d.QtyInv,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task UpdateSalesInvoiceDetailStatusAsync(entities.SalesInvoiceDetail invoiceDetail)
        {
            if (string.IsNullOrWhiteSpace(invoiceDetail.Sidno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(invoiceDetail.Sidno));

            if (string.IsNullOrWhiteSpace(invoiceDetail.ItemDetailCode))
                throw new ArgumentException("Item detail cannot be null or empty.", nameof(invoiceDetail.ItemDetailCode));

            var existing = (await _dbContext.SalesInvoiceDetailFile
                .FirstOrDefaultAsync(x => x.Simno == invoiceDetail.Sidno && x.ItemDetailCode == invoiceDetail.ItemDetailCode));

            if (existing == null)
                throw new InvalidOperationException("Sales order not found");

            existing.RecStatus = invoiceDetail.RecStatus;
        }

        public async Task UpdateSalesInvoiceStatusAsync(string siNo, string status)
        {
            if (string.IsNullOrWhiteSpace(siNo))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(siNo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));

            var existing = (await _dbContext.SalesInvoiceMasterFile
                .Include(x => x.SalesInvoiceDetailFile)
                .FirstOrDefaultAsync(x => x.Simno == siNo));

            if (existing == null)
                throw new InvalidOperationException("Sales Invoice not found");

            existing.RecStatus = status;
        }

        public async Task<IEnumerable<entities.SalesInvoiceMaster>> FindAsync(Expression<Func<entities.SalesInvoiceMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SalesInvoiceMaster, entityframework.SalesInvoiceMasterFile>(predicate);

            var efsalesInvoice = await _dbContext.SalesInvoiceMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efsalesInvoice.Select(SalesInvoiceMapper.MapToEntity);

            return result;
        }

        public async Task<entities.SalesInvoiceDetail?> GetBySINoAndItemAsync(string siNo, string itemDetailCode)
        {
            if (string.IsNullOrWhiteSpace(siNo))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(siNo));

            if (string.IsNullOrWhiteSpace(itemDetailCode))
                throw new ArgumentException("Item detail code cannot be null or empty.", nameof(itemDetailCode));

            var efSalesInvoiceDetail = await _dbContext.SalesInvoiceDetailFile
                .FirstOrDefaultAsync(e => e.Simno == siNo && e.ItemDetailCode == itemDetailCode);

            if (efSalesInvoiceDetail == null)
                return null;

            var salesInvoiceDetail = SalesInvoiceMapper.MapToEntitySalesInvoiceDetail(efSalesInvoiceDetail);

            return salesInvoiceDetail;
        }

        public async Task<List<entities.SalesInvoiceDetail>> GetSIDetailsBySiNoAsync(string siNo)
        {
            if (string.IsNullOrWhiteSpace(siNo))
                throw new ArgumentException("Sales invoice number cannot be null or empty.", nameof(siNo));

            var efSalesInvoiceDetails = await _dbContext.SalesInvoiceDetailFile
                .Where(e => e.Simno == siNo)
                .ToListAsync()
                ?? new List<entityframework.SalesInvoiceDetailFile>();

            var salesInvoiceDetails = efSalesInvoiceDetails
                .Select(e => SalesInvoiceMapper.MapToEntitySalesInvoiceDetail(e)!)
                .Where(e => e != null)
                .ToList();

            return salesInvoiceDetails;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Sales Invoice detailed ID.", nameof(id));

            var efsalesInvoice = await _dbContext.SalesInvoiceMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efsalesInvoice == null)
                throw new InvalidOperationException($"Sales Invoice detailed with ID {id} not found.");

            _dbContext.SalesInvoiceMasterFile.Remove(efsalesInvoice);
        }

        public async Task<IEnumerable<entityframework.SalesInvoiceMasterFile>> GetAllSalesInvoiceRawAsync()
        {
            return await _dbContext.SalesInvoiceMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
