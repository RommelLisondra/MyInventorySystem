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
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public PurchaseOrderRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.PurchaseOrderMaster>> GetAllAsync()
        {
            var efpurchaseorder = await GetAllPurchaseOrderRawAsync();

            if (efpurchaseorder == null || !efpurchaseorder.Any())
                return Enumerable.Empty<entities.PurchaseOrderMaster>();

            var purchaseOrders = efpurchaseorder
                .Select(PurchaseOrderMapper.MapToEntity)
                .ToList();

            return purchaseOrders;
        }

        public async Task<PagedResult<entities.PurchaseOrderMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var purchaseOrders = await GetAllPurchaseOrderRawAsync();

            if (purchaseOrders == null || !purchaseOrders.Any())
                return new PagedResult<entities.PurchaseOrderMaster>
                {
                    Data = Enumerable.Empty<entities.PurchaseOrderMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = purchaseOrders.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = purchaseOrders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(PurchaseOrderMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.PurchaseOrderMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.PurchaseOrderMaster> GetByIdAsync(int id)
        {
            var efpurchaseOrders = await GetAllPurchaseOrderRawAsync();

            if (efpurchaseOrders == null || !efpurchaseOrders.Any())
                return null;

            var purchaseOrders = efpurchaseOrders
                .Select(PurchaseOrderMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return purchaseOrders;
        }

        public async Task AddAsync(entities.PurchaseOrderMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Purchase Order Master entity cannot be null.");

            var purchaseorder = new entityframework.PurchaseOrderMasterFile
            {
                Prno = entity.Prno,
                Pono = entity.Pomno,
                SupplierNo = entity.SupplierNo,
                TypesofPay = entity.TypesofPay,
                Terms = entity.Terms,
                Date = entity.Date,
                DateNeeded = entity.DateNeeded,
                ReferenceNo = entity.ReferenceNo,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                PreparedBy = entity.PrepBy,
                ApprovedBy = entity.ApprBy,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                RrrecStatus = entity.RrrecStatus,
                PreturnRecStatus = entity.PreturnRecStatus,
                RecStatus = entity.RecStatus,
                PurchaseOrderDetailFile = entity.PurchaseOrderDetailFile?.Select(d => new entityframework.PurchaseOrderDetailFile
                {
                    ItemDetailCode = d.ItemDetailCode,
                    Pono = d.Podno,
                    QtyOrder = d.QtyOrder,
                    Uprice = d.Uprice,
                    RecStatus = d.RecStatus,
                    Amount = d.Amount
                }).ToList() ?? new List<entityframework.PurchaseOrderDetailFile>() // safe fallback    
            };

            await _dbContext.PurchaseOrderMasterFile.AddAsync(purchaseorder);
        }

        public async Task UpdateAsync(entities.PurchaseOrderMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Prno is null) throw new ArgumentNullException(nameof(entity.Prno));
            if (entity.PrepBy is null) throw new ArgumentNullException(nameof(entity.PrepBy));
            if (entity.ApprBy is null) throw new ArgumentNullException(nameof(entity.ApprBy));
            if (entity.PurchaseOrderDetailFile is null) throw new ArgumentNullException(nameof(entity.PurchaseOrderDetailFile));

            var existing = (await _dbContext.PurchaseOrderMasterFile
                .Include(x => x.PurchaseOrderDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Purchase Order Master not found");

            existing.Id = entity.id;
            existing.Prno = entity.Prno;
            existing.Pono = entity.Pomno;
            existing.SupplierNo = entity.SupplierNo;
            existing.TypesofPay = entity.TypesofPay;
            existing.Terms = entity.Terms;
            existing.Date = entity.Date;
            existing.DateNeeded = entity.DateNeeded;
            existing.ReferenceNo = entity.ReferenceNo;
            existing.Total = entity.Total;
            existing.DisPercent = entity.DisPercent;
            existing.DisTotal = entity.DisTotal;
            existing.SubTotal = entity.SubTotal;
            existing.Vat = entity.Vat;
            existing.PreparedBy = entity.PrepBy;
            existing.ApprovedBy = entity.ApprBy;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.FooterText = entity.FooterText;
            existing.Recuring = entity.Recuring;
            existing.RrrecStatus = entity.RrrecStatus;
            existing.PreturnRecStatus = entity.PreturnRecStatus;
            existing.RecStatus = entity.RecStatus;

            existing.PurchaseOrderDetailFile.Clear();

            foreach (var d in entity.PurchaseOrderDetailFile)
            {
                existing.PurchaseOrderDetailFile.Add(new entityframework.PurchaseOrderDetailFile
                {
                    Id = d.id,
                    ItemDetailCode = d.ItemDetailCode,
                    Pono = d.Podno,
                    QtyOrder = d.QtyOrder,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.PurchaseOrderMaster>> FindAsync(Expression<Func<entities.PurchaseOrderMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.PurchaseOrderMaster, entityframework.PurchaseOrderMasterFile>(predicate);

            var purchaseOrders = await _dbContext.PurchaseOrderMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = purchaseOrders.Select(PurchaseOrderMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Purchase Order detailed ID.", nameof(id));

            var purchaseOrders = await _dbContext.PurchaseOrderMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (purchaseOrders == null)
                throw new InvalidOperationException($"Purchase Order detailed with ID {id} not found.");

            _dbContext.PurchaseOrderMasterFile.Remove(purchaseOrders);
        }

        public async Task<IEnumerable<entityframework.PurchaseOrderMasterFile>> GetAllPurchaseOrderRawAsync()
        {
            return await _dbContext.PurchaseOrderMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }

        public async Task<entities.PurchaseOrderDetail?> GetByPODNoAndItemAsync(string poNo, string itemDetailCode)
        {
            if (string.IsNullOrWhiteSpace(poNo))
                throw new ArgumentException("Purchase requisition number cannot be null or empty.", nameof(poNo));

            if (string.IsNullOrWhiteSpace(itemDetailCode))
                throw new ArgumentException("Item detail code cannot be null or empty.", nameof(itemDetailCode));

            var efpurchaseOrderDetail = await _dbContext.PurchaseOrderDetailFile
                .FirstOrDefaultAsync(e => e.Pono == poNo && e.ItemDetailCode == itemDetailCode);

            if (efpurchaseOrderDetail == null)
                return null;

            var purchaseOrderDetail = PurchaseOrderMapper.MapToEntityPurchaseOrderDetail(efpurchaseOrderDetail);

            return purchaseOrderDetail;
        }

        public async Task UpdatePurchaseOrderQtyReceiveAsync(entities.PurchaseOrderDetail? orderDetail)
        {
            if (orderDetail == null)
                throw new ArgumentNullException(nameof(orderDetail));

            if (orderDetail.Podno is null)
                throw new ArgumentNullException(nameof(orderDetail.Podno));

            if (orderDetail.ItemDetailCode is null)
                throw new ArgumentNullException(nameof(orderDetail.ItemDetailCode));

            var existing = (await _dbContext.PurchaseOrderDetailFile
                .FirstOrDefaultAsync(x => x.Pono == orderDetail.Podno && x.ItemDetailCode == orderDetail.ItemDetailCode));

            if (existing == null)
                throw new InvalidOperationException("Purchase requisition not found");

            existing.QtyReceived = orderDetail.QtyReceived;
            existing.RecStatus = orderDetail.RecStatus;
        }

        public async Task<List<entities.PurchaseOrderDetail>> GetPODetailsByPoNoAsync(string poNo)
        {
            if (string.IsNullOrWhiteSpace(poNo))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(poNo));

            var efPurchaseOrderDetails = await _dbContext.PurchaseOrderDetailFile
                .Where(e => e.Pono == poNo)
                .ToListAsync()
                ?? new List<entityframework.PurchaseOrderDetailFile>();

            var purchasederDetails = efPurchaseOrderDetails
                .Select(e => PurchaseOrderMapper.MapToEntityPurchaseOrderDetail(e)!)
                .Where(e => e != null)
                .ToList();

            return purchasederDetails;
        }

        public async Task UpdatePurchaseOrderStatusAsync(string poNo, string status, string rrStatus)
        {
            if (string.IsNullOrWhiteSpace(poNo))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(poNo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("status cannot be null or empty.", nameof(status));

            var existing = (await _dbContext.PurchaseOrderMasterFile
                .Include(x => x.PurchaseOrderDetailFile)
                .FirstOrDefaultAsync(x => x.Pono == poNo));

            if (existing == null)
                throw new InvalidOperationException("Purchase order not found");

            existing.RecStatus = status;
            existing.RrrecStatus = rrStatus;
        }
    }
}
