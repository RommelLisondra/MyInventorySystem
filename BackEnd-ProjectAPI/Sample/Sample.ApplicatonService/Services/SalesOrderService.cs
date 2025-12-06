using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SalesOrderService : ISalesOrderService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SalesOrderService));

        public SalesOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderMasterDto>> GetAll()
        {
            try
            {
                var salesOrders = await _unitOfWork.SalesOrderRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SalesOrderMaster>, IEnumerable<SalesOrderMasterDto>>(salesOrders);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SalesOrderMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SalesOrderRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SalesOrderMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<SalesOrderMasterDto>>
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

        public async Task<SalesOrderMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SalesOrderMaster, SalesOrderMasterDto>(await _unitOfWork.SalesOrderRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSalesOrderAsync(SalesOrderMasterDto dto, string createdBy)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number (CustNo) is required.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("CreatedBy is required.", nameof(createdBy));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedOrder = _mapper.Map<Domain.Entities.SalesOrderMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesOrderMaster.Create(mappedOrder, createdBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesOrderDetail ?? Enumerable.Empty<SalesOrderDetailDto>())
                {
                    if (detailDto.QtyOnOrder <= 0)
                        throw new InvalidOperationException($"Invalid Quantity On Order for item {detailDto.ItemDetailCode}.");

                    var newDetail = new Domain.Entities.SalesOrderDetail
                    {
                        Sodno = dto.Somno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyOnOrder = detailDto.QtyOnOrder,
                        QtyInvoice = 0,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesOrderRepository.AddAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                    ?? throw new InvalidOperationException($"Customer with CustNo '{dto.CustNo}' not found.");

                customer.UpdateLastSono(dto.Somno);
                await _unitOfWork.CustomerRepository.UpdateFieldAsync(customer.id, nameof(Domain.Entities.Customer.LastSono), dto.Somno);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSalesOrderAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateSalesOrderAsync(SalesOrderMasterDto dto, string editedBy)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number (CustNo) is required.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(editedBy))
                throw new ArgumentException("editedBy is required.", nameof(editedBy));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedOrder = _mapper.Map<Domain.Entities.SalesOrderMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: SalesInvoiceMaster is null.");

                var master = Domain.Entities.SalesOrderMaster.Update(mappedOrder, editedBy)
                    ?? throw new InvalidOperationException("Failed to create SalesInvoiceMaster domain entity.");

                foreach (var detailDto in dto.SalesOrderDetail ?? Enumerable.Empty<SalesOrderDetailDto>())
                {
                    if (detailDto.QtyOnOrder <= 0)
                        throw new InvalidOperationException($"Invalid Quantity On Order for item {detailDto.ItemDetailCode}.");

                    var newDetail = new Domain.Entities.SalesOrderDetail
                    {
                        Sodno = dto.Somno,
                        ItemDetailCode = detailDto.ItemDetailCode,
                        QtyOnOrder = detailDto.QtyOnOrder,
                        QtyInvoice = 0,
                        Uprice = detailDto.Uprice,
                        Amount = detailDto.Amount,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.SalesOrderRepository.AddAsync(master);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSalesOrderAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<SalesOrderMasterDto>> GetItemsByCustNoAsync(string custNo)
        {
            try
            {
                var salesOrders = await _unitOfWork.SalesOrderRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<SalesOrderMasterDto>>(salesOrders);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<SalesOrderMasterDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var salesOrders = await _unitOfWork.SalesOrderRepository.FindAsync(e => e.id == id);
                var item = salesOrders.FirstOrDefault();
                return item == null ? null : _mapper.Map<SalesOrderMasterDto>(item);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SalesOrderMasterDto>> SearchSalesOrderByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var salesOrders = await _unitOfWork.SalesOrderRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Somno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Date.HasValue ? e.Date.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SalesOrderMasterDto>>(salesOrders);
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
