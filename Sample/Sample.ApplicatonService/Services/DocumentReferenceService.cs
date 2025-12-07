using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class DocumentReferenceService : IDocumentReferenceService, IDisposable
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(DocumentReferenceService));

        public DocumentReferenceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentReferenceDto>> GetAll()
        {
            try
            {
                var documentReferenceList = await _unitOfWork.DocumentReferenceRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.DocumentReference>, IEnumerable<DocumentReferenceDto>>(documentReferenceList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<DocumentReferenceDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.DocumentReferenceRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<DocumentReferenceDto>>(result.Data);

                return new PagedResponse<IEnumerable<DocumentReferenceDto>>
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

        public async Task<DocumentReferenceDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.DocumentReference, DocumentReferenceDto>(await _unitOfWork.DocumentReferenceRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateDocumentReference(DocumentReferenceDto documentReferenceDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var documentReferenceEntity = Domain.Entities.DocumentReference.Create(
                    _mapper.Map<DocumentReferenceDto, Domain.Entities.DocumentReference>(documentReferenceDto), createdBy);

                await _unitOfWork.DocumentReferenceRepository.AddAsync(documentReferenceEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateDocumentReference", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateDocumentReference(DocumentReferenceDto documentReferenceDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var documentReferenceEntity = Domain.Entities.DocumentReference.Update(
                    _mapper.Map<DocumentReferenceDto, Domain.Entities.DocumentReference>(documentReferenceDto), editedBy);

                await _unitOfWork.DocumentReferenceRepository.UpdateAsync(documentReferenceEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateDocumentReference", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<DocumentReferenceDto>> SearchDocumentReferencesByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var documentReferences = await _unitOfWork.DocumentReferenceRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.RefNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.ModuleName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.DocRefId.ToString().ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<DocumentReferenceDto>>(documentReferences);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchDocumentReferencesByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
