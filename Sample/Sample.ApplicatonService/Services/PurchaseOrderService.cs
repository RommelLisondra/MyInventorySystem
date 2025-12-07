using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class PurchaseOrderService : IPurchaseOrderService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseOrderService));

        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseOrderMasterDto>> GetAll()
        {
            try
            {
                var requesitionList = await _unitOfWork.PurchaseOrderRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.PurchaseOrderMaster>, IEnumerable<PurchaseOrderMasterDto>>(requesitionList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<PurchaseOrderMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.PurchaseOrderRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<PurchaseOrderMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<PurchaseOrderMasterDto>>
                {
                    Data = dtoData,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalRecords = result.TotalRecords,
                    TotalPages = result.TotalPages
                };
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAllPaged", ex);
                throw;
            }
        }

        public async Task<PurchaseOrderMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.PurchaseOrderMaster, PurchaseOrderMasterDto>
                    (await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreatePurchaseOrderAsync(PurchaseOrderMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Pomno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.Pomno));

            if (string.IsNullOrWhiteSpace(dto.Prno))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Prno));

            if (string.IsNullOrWhiteSpace(dto.SupplierNo))
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupplierNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase Order total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.PurchaseOrderMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.PurchaseOrderMaster.Create(mappedInvoice, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.PurchaseOrderDetailFile ?? Enumerable.Empty<PurchaseOrderDetailDto>())
                {
                    if (detailDto.QtyOrder <= 0)
                        throw new InvalidOperationException($"Invalid QtyOrder for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    // Update quantity sa inventory entity
                    inventory.IncreaseQuatityOnOrder(detailDto.QtyOrder);

                    // Save changes
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var prDetail = await _unitOfWork.PurchaseRequisitionRepository.GetByPRDNoAndItemAsync(dto.Prno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Purchase requisition detail not found for {dto.Prno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyOrder > prDetail.QtyOrder)
                        throw new InvalidOperationException($"purchase order quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    prDetail.AddRequisitionQtyOnOrder(detailDto.QtyOrder);
                    await _unitOfWork.PurchaseRequisitionRepository.UpdatePurchaseRequisitionQtyOnOrderAsync(prDetail);

                    var newDetail = new Domain.Entities.PurchaseOrderDetail
                    {
                        Podno = dto.Pomno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyOrder = detailDto.QtyOrder,
                        QtyReceived = detailDto.QtyReceived,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RrrecStatus = "O",
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.PurchaseOrderRepository.AddAsync(master);

                var supplier = await _unitOfWork.SupplierRepository.GetBySupplierNoAsync(dto.SupplierNo);
                if (supplier == null)
                    throw new InvalidOperationException($"Supplier not found for {dto.SupplierNo}.");

                supplier.UpdateLastPono(dto.Pomno);

                await _unitOfWork.SupplierRepository.UpdateAsync(supplier);

                var prDetails = await _unitOfWork.PurchaseRequisitionRepository.GetPRDetailsByPrNoAsync(dto.Prno);
                if (prDetails == null)
                    throw new InvalidOperationException($"Purchase requisition details not found for {dto.Prno}.");

                var newStatus = prDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.PurchaseRequisitionRepository.UpdatePurchaseRequisitionStatusAsync(dto.Prno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreatePurchaseOrderAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdatePurchaseOrderAsync(PurchaseOrderMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Pomno))
                throw new ArgumentException("Purchase order number cannot be null or empty.", nameof(dto.Pomno));

            if (string.IsNullOrWhiteSpace(dto.Prno))
                throw new ArgumentException("Purchase requistion number cannot be null or empty.", nameof(dto.Prno));

            if (string.IsNullOrWhiteSpace(dto.SupplierNo))
                throw new ArgumentException("Supplier number cannot be null or empty.", nameof(dto.SupplierNo));

            if (dto.Total <= 0)
                throw new ArgumentException("Purchase Order total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.PurchaseOrderMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.PurchaseOrderMaster.Update(mappedInvoice, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Purchase Order Master domain entity.");

                foreach (var detailDto in dto.PurchaseOrderDetailFile ?? Enumerable.Empty<PurchaseOrderDetailDto>())
                {
                    if (detailDto.QtyOrder <= 0)
                        throw new InvalidOperationException($"Invalid QtyOrder for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                                    ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    // Update quantity sa inventory entity
                    inventory.IncreaseQuatityOnOrder(detailDto.QtyOrder);

                    var prDetail = await _unitOfWork.PurchaseRequisitionRepository.GetByPRDNoAndItemAsync(dto.Prno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"Purchase requisition detail not found for {dto.Prno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyOrder > prDetail.QtyOrder)
                        throw new InvalidOperationException($"purchase order quantity exceeds remaining requisition quantity for item {detailDto.ItemDetailCode}.");

                    prDetail.AddRequisitionQtyOnOrder(detailDto.QtyOrder);
                    await _unitOfWork.PurchaseRequisitionRepository.UpdatePurchaseRequisitionQtyOnOrderAsync(prDetail);

                    var newDetail = new Domain.Entities.PurchaseOrderDetail
                    {
                        Podno = dto.Pomno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyOrder = detailDto.QtyOrder,
                        QtyReceived = detailDto.QtyReceived,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RrrecStatus = detailDto.RrrecStatus,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.PurchaseOrderRepository.AddAsync(master);

                var prDetails = await _unitOfWork.PurchaseRequisitionRepository.GetPRDetailsByPrNoAsync(dto.Prno);
                if (prDetails == null)
                    throw new InvalidOperationException($"Purchase requisition details not found for {dto.Prno}.");

                var newStatus = prDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.PurchaseRequisitionRepository.UpdatePurchaseRequisitionStatusAsync(dto.Prno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdatePurchaseOrderAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrderMasterDto>> GetPurchaseOrderByPrNoAsync(string prNo)
        {
            try
            {
                var requesition = await _unitOfWork.PurchaseOrderRepository.FindAsync(e => e.Pomno == prNo);
                return _mapper.Map<IEnumerable<PurchaseOrderMasterDto>>(requesition);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetPurchaseOrderByPrNoAsync", ex);
                throw;
            }
        }

        public async Task<PurchaseOrderMasterDto?> GetPurchaseOrderByIdAsync(int id)
        {
            try
            {
                var requesitions = await _unitOfWork.PurchaseOrderRepository.FindAsync(e => e.id == id);
                var requesition = requesitions.FirstOrDefault();
                return requesition == null ? null : _mapper.Map<PurchaseOrderMasterDto>(requesition);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseOrderMasterDto>> SearchPurchaseOrderByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var purchases = await _unitOfWork.PurchaseOrderRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Pomno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Prno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.SupplierNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ReferenceNo ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<PurchaseOrderMasterDto>>(purchases);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchPurchaseOrderByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
