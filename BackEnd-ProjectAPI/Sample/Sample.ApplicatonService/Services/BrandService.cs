using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class BrandService : IBrandService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(BrandService));

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAll()
        {
            try
            {
                var brandList = await _unitOfWork.BrandRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Brand>, IEnumerable<BrandDto>>(brandList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<BrandDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.BrandRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<BrandDto>>(result.Data);

                return new PagedResponse<IEnumerable<BrandDto>>
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

        public async Task<BrandDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Brand, BrandDto>(await _unitOfWork.BrandRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateBrand(BrandDto BrandDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var brandEntity = Domain.Entities.Brand.Create(
                    _mapper.Map<BrandDto, Domain.Entities.Brand>(BrandDto), createdBy);

                await _unitOfWork.BrandRepository.AddAsync(brandEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateBrand", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateBrand(BrandDto brandDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var brandEntity = Domain.Entities.Brand.Update(
                    _mapper.Map<BrandDto, Domain.Entities.Brand>(brandDto), editedBy);

                await _unitOfWork.BrandRepository.UpdateAsync(brandEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateBrand", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<BrandDto>> SearchBrandsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var brands = await _unitOfWork.BrandRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.BrandName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<BrandDto>>(brands);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchBrandsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
