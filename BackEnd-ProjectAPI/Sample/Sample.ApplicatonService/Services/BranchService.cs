using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class BranchService : IBranchService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(BranchService));

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BranchDto>> GetAll()
        {
            try
            {
                var branchList = await _unitOfWork.BranchRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Branch>, IEnumerable<BranchDto>>(branchList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<BranchDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.BranchRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<BranchDto>>(result.Data);

                return new PagedResponse<IEnumerable<BranchDto>>
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

        public async Task<BranchDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Branch, BranchDto>(await _unitOfWork.BranchRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateBranch(BranchDto branchDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var branchEntity = Domain.Entities.Branch.Create(
                    _mapper.Map<BranchDto, Domain.Entities.Branch>(branchDto), createdBy);

                await _unitOfWork.BranchRepository.AddAsync(branchEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateBranch", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateBranch(BranchDto branchDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var branchEntity = Domain.Entities.Branch.Update(
                    _mapper.Map<BranchDto, Domain.Entities.Branch>(branchDto), editedBy);

                await _unitOfWork.BranchRepository.UpdateAsync(branchEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateBranch", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<BranchDto>> SearchBranchsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var branch = await _unitOfWork.BranchRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.BranchCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.BranchName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Address ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<BranchDto>>(branch);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchBranchsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
