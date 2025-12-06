using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class OfficialReceiptService : IOfficialReceiptService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(OfficialReceiptService));

        public OfficialReceiptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfficialReceiptMasterDto>> GetAll()
        {
            try
            {
                var officialReceipt = await _unitOfWork.OfficialReceiptRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.OfficialReceiptMaster>, IEnumerable<OfficialReceiptMasterDto>>(officialReceipt);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<OfficialReceiptMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.OfficialReceiptRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<OfficialReceiptMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<OfficialReceiptMasterDto>>
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

        public async Task<OfficialReceiptMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.OfficialReceiptMaster, OfficialReceiptMasterDto>(await _unitOfWork.OfficialReceiptRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateOfficialReceiptAsync(OfficialReceiptMasterDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Orno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Orno));

            if (dto.TotalAmtPaid <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.TotalAmtPaid));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedOfficialReceipt = _mapper.Map<Domain.Entities.OfficialReceiptMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Official Receipt Master is null.");

                var master = Domain.Entities.OfficialReceiptMaster.Create(mappedOfficialReceipt, createdBy)
                    ?? throw new InvalidOperationException("Failed to create Official Receipt Master domain entity.");

                foreach (var detailDto in dto.OfficialReceiptDetailFile ?? Enumerable.Empty<OfficialReceiptDetailDto>())
                {
                    var siMaster = await _unitOfWork.SalesInvoiceRepository.GetByIdAsync(dto.id)
                                   ?? throw new InvalidOperationException($"Sales Invoice master not found for {dto.id}.");

                    var customerForDetail = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                               ?? throw new InvalidOperationException($"Customer '{dto.CustNo}' not found.");

                    if (siMaster.TypesOfPay?.Equals("CREDIT", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        customerForDetail.DecreaseBalance(dto.TotalAmtPaid);
                        await _unitOfWork.CustomerRepository.UpdateAsync(customerForDetail);

                        siMaster.DecreaseBalance(dto.TotalAmtPaid);
                        await _unitOfWork.SalesInvoiceRepository.UpdateAsync(siMaster);

                        await _unitOfWork.SaveAsync();
                    }

                    var siMasterBalance = await _unitOfWork.SalesInvoiceRepository.GetByIdAsync(dto.id)
                                   ?? throw new InvalidOperationException($"Sales Invoice master not found for {dto.id}.");

                    siMasterBalance.ChangeSalesInvoiceMasterStatus(siMasterBalance.Balance);
                    await _unitOfWork.SalesInvoiceRepository.UpdateAsync(siMasterBalance);

                    var newDetail = new Domain.Entities.OfficialReceiptDetail
                    {
                        Ordno = detailDto.Ordno,
                        Simno = detailDto.Simno,
                        AmountPaid = detailDto.AmountPaid,
                        AmountDue = detailDto.AmountDue,
                        RecStatus = "O"
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.OfficialReceiptRepository.AddAsync(master);

                var customer = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                    ?? throw new InvalidOperationException($"Customer with CustNo '{dto.CustNo}' not found.");

                customer.UpdateLastOr(dto.Orno);
                await _unitOfWork.CustomerRepository.UpdateFieldAsync(customer.id, nameof(Domain.Entities.Customer.LastOr), dto.Orno);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("CreateOfficialReceiptAsync", ex);
                throw;
            }
        }

        public async Task UpdateOfficialReceiptAsync(OfficialReceiptMasterDto dto, string editedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.CustNo))
                throw new ArgumentException("Customer number cannot be null or empty.", nameof(dto.CustNo));

            if (string.IsNullOrWhiteSpace(dto.Orno))
                throw new ArgumentException("Sales Invoice number cannot be null or empty.", nameof(dto.Orno));

            if (dto.TotalAmtPaid <= 0)
                throw new ArgumentException("Invoice total must be greater than zero.", nameof(dto.TotalAmtPaid));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var mappedOfficialReceipt = _mapper.Map<Domain.Entities.OfficialReceiptMaster>(dto)
                    ?? throw new InvalidOperationException("Mapping failed: Official Receipt Master is null.");

                var master = Domain.Entities.OfficialReceiptMaster.Update(mappedOfficialReceipt, editedBy)
                    ?? throw new InvalidOperationException("Failed to create Official Receipt Master domain entity.");

                foreach (var detailDto in dto.OfficialReceiptDetailFile ?? Enumerable.Empty<OfficialReceiptDetailDto>())
                {
                    var siMaster = await _unitOfWork.SalesInvoiceRepository.GetByIdAsync(dto.id)
                                   ?? throw new InvalidOperationException($"Sales Invoice master not found for {dto.id}.");

                    var customerForDetail = await _unitOfWork.CustomerRepository.GetByCustNoAsync(dto.CustNo)
                               ?? throw new InvalidOperationException($"Customer '{dto.CustNo}' not found.");

                    if (siMaster.TypesOfPay?.Equals("CREDIT", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        customerForDetail.DecreaseBalance(dto.TotalAmtPaid);
                        await _unitOfWork.CustomerRepository.UpdateAsync(customerForDetail);

                        siMaster.DecreaseBalance(dto.TotalAmtPaid);
                        await _unitOfWork.SalesInvoiceRepository.UpdateAsync(siMaster);

                        await _unitOfWork.SaveAsync();
                    }

                    var siMasterBalance = await _unitOfWork.SalesInvoiceRepository.GetByIdAsync(dto.id)
                                   ?? throw new InvalidOperationException($"Sales Invoice master not found for {dto.id}.");

                    siMasterBalance.ChangeSalesInvoiceMasterStatus(siMasterBalance.Balance);
                    await _unitOfWork.SalesInvoiceRepository.UpdateAsync(siMasterBalance);

                    var newDetail = new Domain.Entities.OfficialReceiptDetail
                    {
                        Ordno = detailDto.Ordno,
                        Simno = detailDto.Simno,
                        AmountPaid = detailDto.AmountPaid,
                        AmountDue = detailDto.AmountDue,
                        RecStatus = detailDto.RecStatus
                    };

                    master.AddDetail(newDetail);
                }

                await _unitOfWork.OfficialReceiptRepository.UpdateAsync(master);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logCentral.Error("UpdateOfficialReceiptAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<OfficialReceiptMasterDto>> GetItemsByCustNoAsync(string custNo)
        {
            try
            {
                var officialReceipt = await _unitOfWork.OfficialReceiptRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<OfficialReceiptMasterDto>>(officialReceipt);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<OfficialReceiptMasterDto?> GetItemByIdAsync(int id)
        {
            try
            {
                var officialReceipts = await _unitOfWork.OfficialReceiptRepository.FindAsync(e => e.id == id);
                var officialReceipt = officialReceipts.FirstOrDefault();
                return officialReceipt == null ? null : _mapper.Map<OfficialReceiptMasterDto>(officialReceipt);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<OfficialReceiptMasterDto>> SearchOfficialReceiptByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var officialReceipts = await _unitOfWork.OfficialReceiptRepository.FindAsync(e =>
                        string.IsNullOrEmpty(keyword) ||
                        EF.Functions.Like((e.Orno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<OfficialReceiptMasterDto>>(officialReceipts);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchOfficialReceiptByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
