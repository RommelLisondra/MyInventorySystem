using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class DocumentSeriesService : IDocumentSeriesService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DocumentSeriesService));

        public DocumentSeriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentSeriesDto>> GetAll()
        {
            try
            {
                var documentSeriesList = await _unitOfWork.DocumentSeriesRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.DocumentSeries>, IEnumerable<DocumentSeriesDto>>(documentSeriesList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<DocumentSeriesDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.DocumentSeriesRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<DocumentSeriesDto>>(result.Data);

                return new PagedResponse<IEnumerable<DocumentSeriesDto>>
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

        public async Task<DocumentSeriesDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.DocumentSeries, DocumentSeriesDto>(await _unitOfWork.DocumentSeriesRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateDocumentSeries(DocumentSeriesDto documentSeriesDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var documentSeriesEntity = Domain.Entities.DocumentSeries.Create(
                    _mapper.Map<DocumentSeriesDto, Domain.Entities.DocumentSeries>(documentSeriesDto), createdBy);

                await _unitOfWork.DocumentSeriesRepository.AddAsync(documentSeriesEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateDocumentSeries", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateDocumentSeries(DocumentSeriesDto documentSeriesDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var documentSeriesEntity = Domain.Entities.DocumentSeries.Update(
                    _mapper.Map<DocumentSeriesDto, Domain.Entities.DocumentSeries>(documentSeriesDto), editedBy);

                await _unitOfWork.DocumentSeriesRepository.UpdateAsync(documentSeriesEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateDocumentSeries", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<DocumentSeriesDto>> SearchDocumentSeriessByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var documentSeries = await _unitOfWork.DocumentSeriesRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like(e.SeriesId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.BranchId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.Year.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.DocumentType ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Prefix ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Suffix ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<DocumentSeriesDto>>(documentSeries);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchDocumentSeriessByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
