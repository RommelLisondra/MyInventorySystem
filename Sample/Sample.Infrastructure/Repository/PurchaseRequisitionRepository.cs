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
    public class PurchaseRequisitionRepository : IPurchaseRequisitionRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public PurchaseRequisitionRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.PurchaseRequisitionMaster>> GetAllAsync()
        {
            var efRequisition = await GetAllPurchaseRequisitionRawAsync();

            if (efRequisition == null || !efRequisition.Any())
                return Enumerable.Empty<entities.PurchaseRequisitionMaster>();

            var requisition = efRequisition
                .Select(PurchaseRequisitionMapper.MapToEntity)
                .ToList();

            return requisition;
        }

        public async Task<PagedResult<entities.PurchaseRequisitionMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var requisition = await GetAllPurchaseRequisitionRawAsync();

            if (requisition == null || !requisition.Any())
                return new PagedResult<entities.PurchaseRequisitionMaster>
                {
                    Data = Enumerable.Empty<entities.PurchaseRequisitionMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = requisition.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = requisition
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(PurchaseRequisitionMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.PurchaseRequisitionMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.PurchaseRequisitionMaster> GetByIdAsync(int id)
        {
            var efRequisition = await GetAllPurchaseRequisitionRawAsync();

            if (efRequisition == null || !efRequisition.Any())
                return null;

            var requisition = efRequisition
                .Select(PurchaseRequisitionMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return requisition;
        }

        public async Task<entities.PurchaseRequisitionMaster> GetByPrNoAsync(string prNo)
        {
            var efRequisition = await GetAllPurchaseRequisitionRawAsync();

            if (efRequisition == null || !efRequisition.Any())
                return null;

            var requisition = efRequisition
                .Select(PurchaseRequisitionMapper.MapToEntity).Where(e => e.Prmno == prNo)
                .FirstOrDefault();

            return requisition;
        }

        public async Task AddAsync(entities.PurchaseRequisitionMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Purchase Requisition Master entity cannot be null.");

            var requesition = new entityframework.PurchaseRequisitionMasterFile
            {
                Prno = entity.Prmno,
                DateRequested = entity.DateRequest,
                ApprovedBy = entity.ApprovedBy,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                RequestedBy = entity.RequestedBy,
                //Department = entity.Department,
                RecStatus = entity.RecStatus,
                PurchaseRequisitionDetailFile = entity.PurchaseRequisitionDetailFile?.Select(d => new entityframework.PurchaseRequisitionDetailFile
                {
                    ItemDetailCode = d.ItemDetailCode,
                    Prno = d.Prdno,
                    QtyRequested = d.QtyRequested,
                    QtyOrder = d.QtyOrder,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.PurchaseRequisitionDetailFile>() // safe fallback    
            };

            await _dbContext.PurchaseRequisitionMasterFile.AddAsync(requesition);
        }

        public async Task UpdateAsync(entities.PurchaseRequisitionMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Prmno is null) throw new ArgumentNullException(nameof(entity.Prmno));
            if (entity.RequestedBy is null) throw new ArgumentNullException(nameof(entity.RequestedBy));
            if (entity.ApprovedBy is null) throw new ArgumentNullException(nameof(entity.ApprovedBy));
            if (entity.PurchaseRequisitionDetailFile is null) throw new ArgumentNullException(nameof(entity.PurchaseRequisitionDetailFile));

            var existing = (await _dbContext.PurchaseRequisitionMasterFile
                .Include(x => x.PurchaseRequisitionDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Purchase Requisition Master not found");

            existing.Id = entity.id;
            existing.Prno = entity.Prmno;
            existing.DateRequested = entity.DateRequest;
            existing.ApprovedBy = entity.ApprovedBy;
            existing.Remarks = entity.Remarks;
            existing.Comments = entity.Comments;
            existing.TermsAndCondition = entity.TermsAndCondition;
            existing.FooterText = entity.FooterText;
            existing.Recuring = entity.Recuring;
            existing.RecStatus = entity.RecStatus;

            existing.PurchaseRequisitionDetailFile.Clear();

            foreach (var d in entity.PurchaseRequisitionDetailFile)
            {
                existing.PurchaseRequisitionDetailFile.Add(new entityframework.PurchaseRequisitionDetailFile
                {
                    Id = d.id,
                    ItemDetailCode = d.ItemDetailCode,
                    Prno = d.Prdno,
                    QtyRequested = d.QtyRequested,
                    QtyOrder = d.QtyOrder,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.PurchaseRequisitionMaster>> FindAsync(Expression<Func<entities.PurchaseRequisitionMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.PurchaseRequisitionMaster, entityframework.PurchaseRequisitionMasterFile>(predicate);

            var requisition = await _dbContext.PurchaseRequisitionMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = requisition.Select(PurchaseRequisitionMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Purchase Requisition detailed ID.", nameof(id));

            var requisition = await _dbContext.PurchaseRequisitionMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (requisition == null)
                throw new InvalidOperationException($"Purchase Requisition detailed with ID {id} not found.");

            _dbContext.PurchaseRequisitionMasterFile.Remove(requisition);
        }

        public async Task<IEnumerable<entityframework.PurchaseRequisitionMasterFile>> GetAllPurchaseRequisitionRawAsync()
        {
            return await _dbContext.PurchaseRequisitionMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }

        public async Task<entities.PurchaseRequisitionDetail?> GetByPRDNoAndItemAsync(string prNo, string itemDetailCode)
        {
            if (string.IsNullOrWhiteSpace(prNo))
                throw new ArgumentException("Purchase requisition number cannot be null or empty.", nameof(prNo));

            if (string.IsNullOrWhiteSpace(itemDetailCode))
                throw new ArgumentException("Item detail code cannot be null or empty.", nameof(itemDetailCode));

            var efpurchaseRequisitionDetail = await _dbContext.PurchaseRequisitionDetailFile
                .FirstOrDefaultAsync(e => e.Prno == prNo && e.ItemDetailCode == itemDetailCode);

            if (efpurchaseRequisitionDetail == null)
                return null;

            var purcahesrequisitionDetail = PurchaseRequisitionMapper.MapToEntityPurchaseRequisitionDetail(efpurchaseRequisitionDetail);

            return purcahesrequisitionDetail;
        }

        public async Task UpdatePurchaseRequisitionQtyOnOrderAsync(entities.PurchaseRequisitionDetail? requisitionDetail)
        {
            if (requisitionDetail == null)
                throw new ArgumentNullException(nameof(requisitionDetail));

            if (requisitionDetail.Prdno is null)
                throw new ArgumentNullException(nameof(requisitionDetail.Prdno));

            if (requisitionDetail.ItemDetailCode is null)
                throw new ArgumentNullException(nameof(requisitionDetail.ItemDetailCode));

            var existing = (await _dbContext.PurchaseRequisitionDetailFile
                .FirstOrDefaultAsync(x => x.Prno == requisitionDetail.Prdno && x.ItemDetailCode == requisitionDetail.ItemDetailCode));

            if (existing == null)
                throw new InvalidOperationException("Purchase requisition not found");

            existing.QtyOrder = requisitionDetail.QtyOrder;
            existing.RecStatus = requisitionDetail.RecStatus;
        }

        public async Task UpdatePurchaseRequisitionStatusAsync(string prNo, string status)
        {
            if (string.IsNullOrWhiteSpace(prNo))
                throw new ArgumentException("Purchase requisition number cannot be null or empty.", nameof(prNo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("status cannot be null or empty.", nameof(status));

            var existing = (await _dbContext.PurchaseRequisitionMasterFile
                .Include(x => x.PurchaseRequisitionDetailFile)
                .FirstOrDefaultAsync(x => x.Prno == prNo));

            if (existing == null)
                throw new InvalidOperationException("Purchase requisition not found");

            existing.RecStatus = status;
        }

        public async Task<List<entities.PurchaseRequisitionDetail>> GetPRDetailsByPrNoAsync(string prNo)
        {
            if (string.IsNullOrWhiteSpace(prNo))
                throw new ArgumentException("Purchase requisition number cannot be null or empty.", nameof(prNo));

            var efSalesOrderDetails = await _dbContext.PurchaseRequisitionDetailFile
                .Where(e => e.Prno == prNo)
                .ToListAsync()
                ?? new List<entityframework.PurchaseRequisitionDetailFile>();

            var salesOrderDetails = efSalesOrderDetails
                .Select(e => PurchaseRequisitionMapper.MapToEntityPurchaseRequisitionDetail(e)!)
                .Where(e => e != null)
                .ToList();

            return salesOrderDetails;
        }
    }
}
