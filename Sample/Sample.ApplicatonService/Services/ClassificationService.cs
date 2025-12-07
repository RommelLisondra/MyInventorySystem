using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class ClassificationService : IClassificationService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(ClassificationService));

        public ClassificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassificationDto>> GetAll()
        {
            try
            {
                var classificationList = await _unitOfWork.ClassificationRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Classification>, IEnumerable<ClassificationDto>>(classificationList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<ClassificationDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.ClassificationRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<ClassificationDto>>(result.Data);

                return new PagedResponse<IEnumerable<ClassificationDto>>
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

        public async Task<ClassificationDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Classification, ClassificationDto>(await _unitOfWork.ClassificationRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateClassification(ClassificationDto classificationDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var classificationEntity = Domain.Entities.Classification.Create(
                    _mapper.Map<ClassificationDto, Domain.Entities.Classification>(classificationDto), createdBy);

                await _unitOfWork.ClassificationRepository.AddAsync(classificationEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateClassification", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateClassification(ClassificationDto classificationDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var classificationEntity = Domain.Entities.Classification.Update(
                    _mapper.Map<ClassificationDto, Domain.Entities.Classification>(classificationDto), editedBy);

                await _unitOfWork.ClassificationRepository.UpdateAsync(classificationEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateClassification", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ClassificationDto>> SearchClassificationsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var classifications = await _unitOfWork.ClassificationRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Name ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<ClassificationDto>>(classifications);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchClassificationsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
