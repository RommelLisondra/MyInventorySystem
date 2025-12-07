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
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public SalesOrderRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.SalesOrderMaster>> GetAllAsync()
        {
            var efsalesorder = await GetAllSalesOrderRawAsync();

            if (efsalesorder == null || !efsalesorder.Any())
                return Enumerable.Empty<entities.SalesOrderMaster>();

            var salesorder = efsalesorder
                .Select(SalesOrderMapper.MapToEntity)
                .ToList();

            return salesorder;
        }

        public async Task<PagedResult<entities.SalesOrderMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var salesorder = await GetAllSalesOrderRawAsync();

            if (salesorder == null || !salesorder.Any())
                return new PagedResult<entities.SalesOrderMaster>
                {
                    Data = Enumerable.Empty<entities.SalesOrderMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = salesorder.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = salesorder
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(SalesOrderMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.SalesOrderMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.SalesOrderMaster> GetByIdAsync(int id)
        {
            var efsalesorder = await GetAllSalesOrderRawAsync();

            if (efsalesorder == null || !efsalesorder.Any())
                return null;

            var salesorder = efsalesorder
                .Select(SalesOrderMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return salesorder;
        }

        public async Task<entities.SalesOrderDetail?> GetBySODNoAndItemAsync(string soNo, string itemDetailCode)
        {
            if (string.IsNullOrWhiteSpace(soNo))
                throw new ArgumentException("Sales Order number cannot be null or empty.", nameof(soNo));

            if (string.IsNullOrWhiteSpace(itemDetailCode))
                throw new ArgumentException("Item detail code cannot be null or empty.", nameof(itemDetailCode));

            var efSalesOrderDetail = await _dbContext.SalesOrderDetailFile
                .FirstOrDefaultAsync(e => e.Somno == soNo && e.ItemDetailCode == itemDetailCode);

            if (efSalesOrderDetail == null)
                return null;

            var salesOrderDetail = SalesOrderMapper.MapToEntitySalesOrderDetail(efSalesOrderDetail);

            return salesOrderDetail;
        }

        public async Task<List<entities.SalesOrderDetail>> GetSODetailsBySoNoAsync(string soNo)
        {
            if (string.IsNullOrWhiteSpace(soNo))
                throw new ArgumentException("Sales Order number cannot be null or empty.", nameof(soNo));

            var efSalesOrderDetails = await _dbContext.SalesOrderDetailFile
                .Where(e => e.Somno == soNo)
                .ToListAsync()
                ?? new List<entityframework.SalesOrderDetailFile>();

            var salesOrderDetails = efSalesOrderDetails
                .Select(e => SalesOrderMapper.MapToEntitySalesOrderDetail(e)!)
                .Where(e => e != null)
                .ToList();

            return salesOrderDetails;
        }

        public async Task AddAsync(entities.SalesOrderMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Sales Invoice Master entity cannot be null.");

            var salesOrder = new entityframework.SalesOrderMasterFile
            {
                Somno = entity.Somno ?? throw new ArgumentNullException(nameof(entity.Somno)),
                CustNo = entity.CustNo ?? throw new ArgumentNullException(nameof(entity.CustNo)),
                Date = entity.Date,
                Terms = entity.Terms,
                TypesOfPay = entity.TypesOfPay,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                Recuring = entity.Recuring,
                FooterText = entity.FooterText,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                Total = entity.Total,
                PreparedBy = entity.PrepBy,
                ApprovedBy = entity.ApprBy,
                RecStatus = entity.RecStatus,
                SalesOrderDetailFile = entity.SalesOrderDetail?.Select(d => new entityframework.SalesOrderDetailFile
                {
                    Somno = d.Sodno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyOnOrder = d.QtyOnOrder,
                    QtyInvoice = d.QtyInvoice,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.SalesOrderDetailFile>() // safe fallback
            };

            await _dbContext.SalesOrderMasterFile.AddAsync(salesOrder);
        }

        public async Task UpdateAsync(entities.SalesOrderMaster? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.CustNo is null)
                throw new ArgumentNullException(nameof(entity.CustNo));

            if (entity.SalesOrderDetail is null)
                throw new ArgumentNullException(nameof(entity.SalesOrderDetail));

            var existing = (await _dbContext.SalesOrderMasterFile
                .Include(x => x.SalesOrderDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null)
                throw new InvalidOperationException("Sales order not found");

            // Update master fields
            existing.Somno = entity.Somno;
            existing.CustNo = entity.CustNo;
            existing.Date = entity.Date;
            existing.Terms = entity.Terms;
            existing.TypesOfPay = entity.TypesOfPay;
            existing.PreparedBy = entity.PrepBy;
            existing.ApprovedBy = entity.ApprBy;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.Recuring = entity.Recuring;
            existing.FooterText = entity.FooterText;
            existing.DisPercent = entity.DisPercent;
            existing.DisTotal = entity.DisTotal;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.Total = entity.Total;
            existing.RecStatus = entity.RecStatus;

            // Sync details
            existing.SalesOrderDetailFile.Clear();

            foreach (var d in entity.SalesOrderDetail)
            {
                existing.SalesOrderDetailFile.Add(new entityframework.SalesOrderDetailFile
                {
                    Somno = d.Sodno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyOnOrder = d.QtyOnOrder,
                    QtyInvoice = d.QtyInvoice,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task UpdateSalesOrderStatusAsync(string soNo, string status)
        {
            if (string.IsNullOrWhiteSpace(soNo))
                throw new ArgumentException("Sales Order number cannot be null or empty.", nameof(soNo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("status cannot be null or empty.", nameof(status));

            var existing = (await _dbContext.SalesOrderMasterFile
                .Include(x => x.SalesOrderDetailFile)
                .FirstOrDefaultAsync(x => x.Somno == soNo));

            if (existing == null)
                throw new InvalidOperationException("Sales order not found");

            existing.RecStatus = status;
        }

        public async Task UpdateSalesOrderQtyInvoiceAsync(entities.SalesOrderDetail? orderDetail)
        {
            if (orderDetail == null)
                throw new ArgumentNullException(nameof(orderDetail));

            if (orderDetail.Sodno is null)
                throw new ArgumentNullException(nameof(orderDetail.Sodno));

            if (orderDetail.ItemDetailCode is null)
                throw new ArgumentNullException(nameof(orderDetail.ItemDetailCode));

            var existing = (await _dbContext.SalesOrderDetailFile
                .FirstOrDefaultAsync(x => x.Somno == orderDetail.Sodno && x.ItemDetailCode == orderDetail.ItemDetailCode));

            if (existing == null)
                throw new InvalidOperationException("Sales order not found");

            existing.QtyInvoice = orderDetail.QtyInvoice;
        }

        public async Task<IEnumerable<entities.SalesOrderMaster>> FindAsync(Expression<Func<entities.SalesOrderMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.SalesOrderMaster, entityframework.SalesOrderMasterFile>(predicate);

            var efsalesorder = await _dbContext.SalesOrderMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efsalesorder.Select(SalesOrderMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Sales Invoice detailed ID.", nameof(id));

            var efsalesorder = await _dbContext.SalesOrderMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efsalesorder == null)
                throw new InvalidOperationException($"Sales Invoice detailed with ID {id} not found.");

            _dbContext.SalesOrderMasterFile.Remove(efsalesorder);
        }

        public async Task<IEnumerable<entityframework.SalesOrderMasterFile>> GetAllSalesOrderRawAsync()
        {
            return await _dbContext.SalesOrderMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
