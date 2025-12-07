using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class SubCategoryService : ISubCategoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(SubCategoryService));

        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubCategoryDto>> GetAll()
        {
            try
            {
                var subCategories = await _unitOfWork.SubCategoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.SubCategory>, IEnumerable<SubCategoryDto>>(subCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<SubCategoryDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.SubCategoryRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<SubCategoryDto>>(result.Data);

                return new PagedResponse<IEnumerable<SubCategoryDto>>
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

        public async Task<SubCategoryDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.SubCategory, SubCategoryDto>(await _unitOfWork.SubCategoryRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateSubCategory(SubCategoryDto subCategoryDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var subCategories = Domain.Entities.SubCategory.Create(
                    _mapper.Map<SubCategoryDto, Domain.Entities.SubCategory>(subCategoryDto), createdBy);

                await _unitOfWork.SubCategoryRepository.AddAsync(subCategories);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateSubCategory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateSubCategory(SubCategoryDto subCategoryDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var subCategories = Domain.Entities.SubCategory.Update(
                    _mapper.Map<SubCategoryDto, Domain.Entities.SubCategory>(subCategoryDto), editedBy);

                await _unitOfWork.SubCategoryRepository.UpdateAsync(subCategories);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateSubCategory", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<SubCategoryDto>> SearchSubCategorysByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var subCategories = await _unitOfWork.SubCategoryRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.SubCategoryName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchSubCategorysByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
