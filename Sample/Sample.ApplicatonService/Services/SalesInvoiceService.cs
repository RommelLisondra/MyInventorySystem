using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SalesInvoiceService : ISalesInvoiceService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesInvoiceService));

        public SalesInvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesInvoiceMasterDto>> GetAll()
        {
            try
            {
                var salesInvoices = await _unitOfWork.SalesInvoiceRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SalesInvoiceMaster>, IEnumerable<SalesInvoiceMasterDto>>(salesInvoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<SalesInvoiceMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SalesInvoiceRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SalesInvoiceMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<SalesInvoiceMasterDto>>
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

        public async Task<SalesInvoiceMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SalesInvoiceMaster, SalesInvoiceMasterDto>(await _unitOfWork.SalesInvoiceRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSalesInvoiceAsync(SalesInvoiceMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (string.IsNullOrWhiteSpace(dto.Somno))
                throw new ArgumentException("Sales Order number cannot be null or empty.", nameof(dto.Somno));

            if (dto.Total <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.SalesInvoiceMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesInvoiceMaster.Create(mappedInvoice, createdBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesInvoiceDetail ?? Enumerable.Empty<SalesInvoiceDetailDto>())
                {
                    if (detailDto.QtyInv <= 0)
                        throw new InvalidOperationException($"Invalid QtyInv for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.DecreaseOnHand(detailDto.QtyInv, dto.Simno);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var soDetail = await _unitOfWork.SalesOrderRepository.GetBySODNoAndItemAsync(dto.Somno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"SO detail not found for {dto.Somno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyInv > soDetail.QtyOnOrder)
                        throw new InvalidOperationException($"Invoiced quantity exceeds remaining SO quantity for item {detailDto.ItemDetailCode}.");

                    soDetail.AddInvoicedQty(detailDto.QtyInv);
                    await _unitOfWork.SalesOrderRepository.UpdateSalesOrderQtyInvoiceAsync(soDetail);

                    var newDetail = new Domain.Entities.SalesInvoiceDetail
                    {
                        Sidno = dto.Simno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyInv = detailDto.QtyInv,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesInvoiceRepository.AddAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                               ?? throw new InvalidOperationException($"Customer '{dto.CustNo}' not found.");

                customer.UpdateLastSino(dto.Simno);

                if (dto.TypesOfPay?.Equals("CREDIT", StringComparison.OrdinalIgnoreCase) == true)
                {
                    customer.IncreaseBalance(dto.Total);
                }

                await _unitOfWork.CustomerRepository.UpdateAsync(customer);

                var soDetails = await _unitOfWork.SalesOrderRepository.GetSODetailsBySoNoAsync(dto.Somno);
                if (soDetails == null)
                    throw new InvalidOperationException($"Sales order details not found for {dto.Somno}.");

                var newStatus = soDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.SalesOrderRepository.UpdateSalesOrderStatusAsync(dto.Somno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("CreateSalesInvoiceAsync", ex);
                throw;
            }
        }

        public async Task UpdateSalesinvoiceAsync(SalesInvoiceMasterDto dto, string editedBy)
        {

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Simno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Simno));

            if (string.IsNullOrWhiteSpace(dto.Somno))
                throw new ArgumentException("Sales Order number cannot be null or empty.", nameof(dto.Somno));

            if (dto.Total <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.Total));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedInvoice = _mapper.Map<Domain.Entities.SalesInvoiceMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesInvoiceMaster.Update(mappedInvoice, editedBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesInvoiceDetail ?? Enumerable.Empty<SalesInvoiceDetailDto>())
                {
                    if (detailDto.QtyInv <= 0)
                        throw new InvalidOperationException($"Invalid QtyInv for item {detailDto.ItemDetailCode}.");

                    var item = await _unitOfWork.ItemDetailRepository.GetByItemdetailCodeAsync(detailDto.ItemDetailCode)
                               ?? throw new InvalidOperationException($"Item {detailDto.ItemDetailCode} not found.");

                    var inventory = item.ItemInventory
                                        ?.FirstOrDefault(i => i.WarehouseCode == detailDto.ItemDetailCodeNavigation.WarehouseCode &&
                                                              i.LocationCode == detailDto.ItemDetailCodeNavigation.LocationCode)
                                     ?? throw new InvalidOperationException("Inventory record not found.");

                    inventory.DecreaseOnHand(detailDto.QtyInv, dto.Simno);
                    await _unitOfWork.ItemDetailRepository.UpdateAsync(item);

                    var soDetail = await _unitOfWork.SalesOrderRepository.GetBySODNoAndItemAsync(dto.Somno, detailDto.ItemDetailCode)
                                   ?? throw new InvalidOperationException($"SO detail not found for {dto.Somno} - {detailDto.ItemDetailCode}.");

                    if (detailDto.QtyInv > soDetail.QtyOnOrder)
                        throw new InvalidOperationException($"Invoiced quantity exceeds remaining SO quantity for item {detailDto.ItemDetailCode}.");

                    soDetail.AddInvoicedQty(detailDto.QtyInv);
                    await _unitOfWork.SalesOrderRepository.UpdateSalesOrderQtyInvoiceAsync(soDetail);

                    var newDetail = new Domain.Entities.SalesInvoiceDetail
                    {
                        Sidno = dto.Simno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyInv = detailDto.QtyInv,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesInvoiceRepository.UpdateAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                               ?? throw new InvalidOperationException($"Customer '{dto.CustNo}' not found.");

                if (dto.TypesOfPay?.Equals("CREDIT", StringComparison.OrdinalIgnoreCase) == true)
                {
                    customer.IncreaseBalance(dto.Total);
                }

                await _unitOfWork.CustomerRepository.UpdateAsync(customer);

                var soDetails = await _unitOfWork.SalesOrderRepository.GetSODetailsBySoNoAsync(dto.Somno);
                if (soDetails == null)
                    throw new InvalidOperationException($"Sales order details not found for {dto.Somno}.");

                var newStatus = soDetails.All(s => s.RecStatus == "C") ? "C" : "O";
                await _unitOfWork.SalesOrderRepository.UpdateSalesOrderStatusAsync(dto.Somno, newStatus);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("UpdateSalesinvoiceAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalesInvoiceMasterDto>> GetSalesinvoiceByCustNoAsync(string custNo)
        {
            try
            {
                var salesInvoices = await _unitOfWork.SalesInvoiceRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<SalesInvoiceMasterDto>>(salesInvoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSalesinvoiceByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<SalesInvoiceMasterDto?> GetSalesinvoiceByIdAsync(int id)
        {
            try
            {
                var salesInvoices = await _unitOfWork.SalesInvoiceRepository.FindAsync(e => e.id == id);
                var salesInvoice = salesInvoices.FirstOrDefault();
                return salesInvoice == null ? null : _mapper.Map<SalesInvoiceMasterDto>(salesInvoice);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSalesinvoiceByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalesInvoiceMasterDto>> SearchSalesInvoiceByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var salesInvoices = await _unitOfWork.SalesInvoiceRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Simno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Simno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Somno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                     EF.Functions.Like((e.Date.HasValue ? e.Date.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SalesInvoiceMasterDto>>(salesInvoices);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSalesInvoiceByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
