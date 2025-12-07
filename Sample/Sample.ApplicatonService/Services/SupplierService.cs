using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SupplierService : ISupplierService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SupplierService));

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            try
            {
                var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Supplier>, IEnumerable<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SupplierDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SupplierRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SupplierDto>>(result.Data);

                return new PagedResponse<IEnumerable<SupplierDto>>
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

        public async Task<SupplierDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Supplier, SupplierDto>(await _unitOfWork.SupplierRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSupplier(SupplierDto supplierDto, string createdBy)
        {
            try
            {
                var suppliers = Domain.Entities.Supplier.Create(
                    _mapper.Map<SupplierDto, Domain.Entities.Supplier>(supplierDto), createdBy);

                await _unitOfWork.SupplierRepository.AddAsync(suppliers);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSupplier", ex);
                throw;
            }
        }

        public async Task UpdateSupplier(SupplierDto supplierDto, string editedBy)
        {
            try
            {
                var suppliers = Domain.Entities.Supplier.Update(
                    _mapper.Map<SupplierDto, Domain.Entities.Supplier>(supplierDto), editedBy);

                await _unitOfWork.SupplierRepository.UpdateAsync(suppliers);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateSupplier", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierDto>> GetSuppliersBysupplierNoAsync(string supplierNo)
        {
            try
            {
                var suppliers = await _unitOfWork.SupplierRepository.FindAsync(e => e.SupplierNo == supplierNo);
                return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSuppliersBysupplierNoAsync", ex);
                throw;
            }
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            try
            {
                var suppliers = await _unitOfWork.SupplierRepository.FindAsync(e => e.id == id);
                var supplier = suppliers.FirstOrDefault();
                return supplier == null ? null : _mapper.Map<SupplierDto>(supplier);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetSupplierByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierDto>> SearchSuppliersByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var suppliers = await _unitOfWork.SupplierRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.SupplierNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Name ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Address ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.City ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.State ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Country ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.EmailAddress ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSuppliersAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
