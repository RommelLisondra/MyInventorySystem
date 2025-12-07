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
    public class ReceivingReportRepository : IReceivingReportRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public ReceivingReportRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.ReceivingReportMaster>> GetAllAsync()
        {
            var efReceiving = await GetAllReceivingReportRawAsync();

            if (efReceiving == null || !efReceiving.Any())
                return Enumerable.Empty<entities.ReceivingReportMaster>();

            var receiving = efReceiving
                .Select(ReceivingReportMapper.MapToEntity)
                .ToList();

            return receiving;
        }

        public async Task<PagedResult<entities.ReceivingReportMaster>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var receiving = await GetAllReceivingReportRawAsync();

            if (receiving == null || !receiving.Any())
                return new PagedResult<entities.ReceivingReportMaster>
                {
                    Data = Enumerable.Empty<entities.ReceivingReportMaster>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = receiving.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = receiving
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ReceivingReportMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.ReceivingReportMaster>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.ReceivingReportMaster> GetByIdAsync(int id)
        {
            var efReceiving = await GetAllReceivingReportRawAsync();

            if (efReceiving == null || !efReceiving.Any())
                return null;

            var receiving = efReceiving
                .Select(ReceivingReportMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return receiving;
        }

        public async Task AddAsync(entities.ReceivingReportMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Receiving Report Master entity cannot be null.");

            var receiving = new entityframework.ReceivingReportMasterFile
            {
                Rrno = entity.Rrmno ?? throw new ArgumentNullException(nameof(entity.Rrmno)),
                Pono = entity.Pono ?? throw new ArgumentNullException(nameof(entity.Pono)),
                SupplierNo = entity.SupNo ?? throw new ArgumentNullException(nameof(entity.SupNo)),
                RefNo = entity.RefNo ?? throw new ArgumentNullException(nameof(entity.RefNo)),
                Date = entity.Date,
                Terms = entity.Terms,
                ReceivedBy = entity.ReceivedBy ?? throw new ArgumentNullException(nameof(entity.ReceivedBy)),
                TypesOfPay = entity.TypesOfPay,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                RecStatus = entity.RecStatus,
                PreturnRecStatus = entity.PreturnRecStatus,
                ReceivingReportDetailFile = entity.ReceivingReportDetailFile?.Select(d => new entityframework.ReceivingReportDetailFile
                {
                    Rrno = d.Rrdno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyReceived = d.QtyReceived,
                    QtyReturn = d.QtyRet,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    PretrunRecStatus = d.PretrunRecStatus,
                    RecStatus = d.RecStatus
                }).ToList() ?? new List<entityframework.ReceivingReportDetailFile>() // safe fallback    
            };

            await _dbContext.ReceivingReportMasterFile.AddAsync(receiving);
        }

        public async Task UpdateAsync(entities.ReceivingReportMaster? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Rrmno is null) throw new ArgumentNullException(nameof(entity.Rrmno));
            if (entity.Pono is null) throw new ArgumentNullException(nameof(entity.Pono));
            if (entity.RefNo is null) throw new ArgumentNullException(nameof(entity.RefNo));
            if (entity.ReceivedBy is null) throw new ArgumentNullException(nameof(entity.ReceivedBy));
            if (entity.ReceivingReportDetailFile is null) throw new ArgumentNullException(nameof(entity.ReceivingReportDetailFile));

            var existing = (await _dbContext.ReceivingReportMasterFile
                .Include(x => x.ReceivingReportDetailFile)
                .FirstOrDefaultAsync(x => x.Id == entity.id));

            if (existing == null) throw new InvalidOperationException("Receiving Report Master not found");

            existing.Rrno = entity.Rrmno;
            existing.Pono = entity.Pono;
            existing.SupplierNo = entity.SupNo ?? throw new ArgumentNullException(nameof(entity.SupNo));
            existing.RefNo = entity.RefNo;
            existing.Date = entity.Date;
            existing.Terms = entity.Terms;
            existing.ReceivedBy = entity.ReceivedBy;
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
            existing.PreturnRecStatus = entity.PreturnRecStatus;

            existing.ReceivingReportDetailFile.Clear();

            foreach (var d in entity.ReceivingReportDetailFile)
            {
                existing.ReceivingReportDetailFile.Add(new entityframework.ReceivingReportDetailFile
                {
                    Rrno = d.Rrdno,
                    ItemDetailCode = d.ItemDetailCode,
                    QtyReceived = d.QtyReceived,
                    QtyReturn = d.QtyRet,
                    Uprice = d.Uprice,
                    Amount = d.Amount,
                    PretrunRecStatus = d.PretrunRecStatus,
                    RecStatus = d.RecStatus
                });
            }
        }

        public async Task<IEnumerable<entities.ReceivingReportMaster>> FindAsync(Expression<Func<entities.ReceivingReportMaster, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.ReceivingReportMaster, entityframework.ReceivingReportMasterFile>(predicate);

            var efReceiving = await _dbContext.ReceivingReportMasterFile
                .Where(efPredicate)
                .ToListAsync();

            var result = efReceiving.Select(ReceivingReportMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Receiving Report detailed ID.", nameof(id));

            var efReceiving = await _dbContext.ReceivingReportMasterFile
                .FirstOrDefaultAsync(e => e.Id == id);

            if (efReceiving == null)
                throw new InvalidOperationException($"Receiving Report detailed with ID {id} not found.");

            _dbContext.ReceivingReportMasterFile.Remove(efReceiving);
        }

        public async Task<IEnumerable<entityframework.ReceivingReportMasterFile>> GetAllReceivingReportRawAsync()
        {
            return await _dbContext.ReceivingReportMasterFile
                .Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
        public async Task<entities.ReceivingReportDetail?> GetByRRDNoAndItemAsync(string rrNo, string itemDetailCode)
        {
            if (string.IsNullOrWhiteSpace(rrNo))
                throw new ArgumentException("Receiving report number cannot be null or empty.", nameof(rrNo));

            if (string.IsNullOrWhiteSpace(itemDetailCode))
                throw new ArgumentException("Item detail code cannot be null or empty.", nameof(itemDetailCode));

            var efreceivingDetail = await _dbContext.ReceivingReportDetailFile
                .FirstOrDefaultAsync(e => e.Rrno == rrNo && e.ItemDetailCode == itemDetailCode);

            if (efreceivingDetail == null)
                return null;

            var receivingDetail = ReceivingReportMapper.MapToEntityReceivingReportDetail(efreceivingDetail);

            return receivingDetail;
        }

        public async Task UpdateReceivingQtyReceiveAsync(entities.ReceivingReportDetail? receivingDetail)
        {
            if (receivingDetail == null)
                throw new ArgumentNullException(nameof(receivingDetail));

            if (receivingDetail.Rrdno is null)
                throw new ArgumentNullException(nameof(receivingDetail.Rrdno));

            if (receivingDetail.ItemDetailCode is null)
                throw new ArgumentNullException(nameof(receivingDetail.ItemDetailCode));

            var existing = (await _dbContext.ReceivingReportDetailFile
                .FirstOrDefaultAsync(x => x.Rrno == receivingDetail.Rrdno && x.ItemDetailCode == receivingDetail.ItemDetailCode));

            if (existing == null)
                throw new InvalidOperationException("Receiving Report Detail not found");

            existing.QtyReturn = receivingDetail.QtyRet;
            existing.RecStatus = receivingDetail.RecStatus;
        }

        public async Task<List<entities.ReceivingReportDetail>> GetRRDetailsByRrNoAsync(string rrNo)
        {
            if (string.IsNullOrWhiteSpace(rrNo))
                throw new ArgumentException("Receiving report number cannot be null or empty.", nameof(rrNo));

            var efreceivingDetails = await _dbContext.ReceivingReportDetailFile
                .Where(e => e.Rrno == rrNo)
                .ToListAsync()
                ?? new List<entityframework.ReceivingReportDetailFile>();

            var receivingDetails = efreceivingDetails
                .Select(e => ReceivingReportMapper.MapToEntityReceivingReportDetail(e)!)
                .Where(e => e != null)
                .ToList();

            return receivingDetails;
        }

        public async Task UpdateReceivingStatusAsync(string rrNo, string status)
        {
            if (string.IsNullOrWhiteSpace(rrNo))
                throw new ArgumentException("Receiving report number cannot be null or empty.", nameof(rrNo));

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("status cannot be null or empty.", nameof(status));

            var existing = (await _dbContext.ReceivingReportMasterFile
                .Include(x => x.ReceivingReportDetailFile)
                .FirstOrDefaultAsync(x => x.Rrno == rrNo));

            if (existing == null)
                throw new InvalidOperationException("Receiving report not found");

            existing.RecStatus = status;
        }
    }
}
