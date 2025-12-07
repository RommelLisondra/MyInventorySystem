using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class PurchaseRequisitionService : IPurchaseRequisitionService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(PurchaseRequisitionService));

        public PurchaseRequisitionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseRequisitionMasterDto>> GetAll()
        {
            try
            {
                var requesitionList = await _unitOfWork.PurchaseRequisitionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.PurchaseRequisitionMaster>, IEnumerable<PurchaseRequisitionMasterDto>>(requesitionList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<PurchaseRequisitionMasterDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.PurchaseRequisitionRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<PurchaseRequisitionMasterDto>>(result.Data);

                return new PagedResponse<IEnumerable<PurchaseRequisitionMasterDto>>
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

        public async Task<PurchaseRequisitionMasterDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.PurchaseRequisitionMaster, PurchaseRequisitionMasterDto>
                    (await _unitOfWork.PurchaseRequisitionRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreatePurchaseRequisitionAsync(PurchaseRequisitionMasterDto dto, string createdBy)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("CreatedBy is required.", nameof(createdBy));

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var requisition = _mapper.Map<Domain.Entities.PurchaseRequisitionMaster>(dto);
                var entity = Domain.Entities.PurchaseRequisitionMaster.Create(requisition, createdBy);
                await _unitOfWork.PurchaseRequisitionRepository.AddAsync(entity);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreatePurchaseRequisitionAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdatePurchaseRequisitionAsync(PurchaseRequisitionMasterDto dto, string editedBy)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var requesitionEntity = Domain.Entities.PurchaseRequisitionMaster.Update(
                    _mapper.Map<PurchaseRequisitionMasterDto, Domain.Entities.PurchaseRequisitionMaster>(dto), editedBy);

                await _unitOfWork.PurchaseRequisitionRepository.UpdateAsync(requesitionEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdatePurchaseRequisitionAsync", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseRequisitionMasterDto>> GetPurchaseRequisitionByPrNoAsync(string prNo)
        {
            try
            {
                var requesition = await _unitOfWork.PurchaseRequisitionRepository.FindAsync(e => e.Prmno == prNo);
                return _mapper.Map<IEnumerable<PurchaseRequisitionMasterDto>>(requesition);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemsByPrNoAsync", ex);
                throw;
            }
        }

        public async Task<PurchaseRequisitionMasterDto?> GetPurchaseRequisitionByIdAsync(int id)
        {
            try
            {
                var requesitions = await _unitOfWork.PurchaseRequisitionRepository.FindAsync(e => e.id == id);
                var requesition = requesitions.FirstOrDefault();
                return requesition == null ? null : _mapper.Map<PurchaseRequisitionMasterDto>(requesition);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetItemByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PurchaseRequisitionMasterDto>> SearchPurchaseRequisitionByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var requisition = await _unitOfWork.PurchaseRequisitionRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                        EF.Functions.Like((e.Prmno ?? string.Empty).ToLower(), $"%{keyword}%") ||
                        EF.Functions.Like((e.DateNeeded.HasValue ? e.DateNeeded.Value.ToString() : string.Empty),$"%{keyword}%") ||
                        EF.Functions.Like((e.DateRequest.HasValue ? e.DateRequest.Value.ToString() : string.Empty), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<PurchaseRequisitionMasterDto>>(requisition);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchPurchaseRequisitionByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
