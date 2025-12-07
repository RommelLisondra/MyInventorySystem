using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;


namespace Sample.ApplicationService.Services
{
    public class SupplierImageService : ISupplierImageService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SupplierService));

        public SupplierImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<SupplierImageDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SupplierImageRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SupplierImageDto>>(result.Data);

                return new PagedResponse<IEnumerable<SupplierImageDto>>
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

        public async Task<SupplierImageDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SupplierImage, SupplierImageDto>(await _unitOfWork.SupplierImageRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSupplierImage(SupplierImageDto supplierImageDto, string createdBy)
        {
            try
            {
                var supplierImage = Domain.Entities.SupplierImage.Create(
                    _mapper.Map<SupplierImageDto, Domain.Entities.SupplierImage>(supplierImageDto), createdBy);

                await _unitOfWork.SupplierImageRepository.AddAsync(supplierImage);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSupplierImage", ex);
                throw;
            }
        }

        public async Task UpdateSupplierImage(SupplierImageDto supplierImageDto, string editedBy)
        {
            try
            {
                var supplierImage = Domain.Entities.SupplierImage.Update(
                    _mapper.Map<SupplierImageDto, Domain.Entities.SupplierImage>(supplierImageDto), editedBy);

                await _unitOfWork.SupplierImageRepository.UpdateAsync(supplierImage);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateSupplierImage", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SupplierImageDto>> SearchSupplierImageByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var supplierImage = await _unitOfWork.SupplierImageRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.SupplierNo ?? string.Empty).ToLower(), $"%{keyword}%") 
                );

                return _mapper.Map<IEnumerable<SupplierImageDto>>(supplierImage);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSupplierImageByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
