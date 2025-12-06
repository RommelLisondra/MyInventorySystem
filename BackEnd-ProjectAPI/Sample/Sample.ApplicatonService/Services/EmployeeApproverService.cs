using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeApproverService : IEmployeeApproverService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeApproverService));

        public EmployeeApproverService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeApproverDto>> GetAll()
        {
            try
            {
                var employeeApproverList = await _unitOfWork.EmployeeApproverRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.EmployeeApprover>, IEnumerable<EmployeeApproverDto>>(employeeApproverList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<EmployeeApproverDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeApproverRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeApproverDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeApproverDto>>
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

        public async Task<EmployeeApproverDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.EmployeeApprover, EmployeeApproverDto>(await _unitOfWork.EmployeeApproverRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateEmployeeApprover(EmployeeApproverDto employeeApproverDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeApprover = Domain.Entities.EmployeeApprover.Create(
                    _mapper.Map<EmployeeApproverDto, Domain.Entities.EmployeeApprover>(employeeApproverDto), createdBy);

                await _unitOfWork.EmployeeApproverRepository.AddAsync(employeeApprover);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployeeApprover", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeApprover(EmployeeApproverDto employeeApproverDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeApprover = Domain.Entities.EmployeeApprover.Update(
                    _mapper.Map<EmployeeApproverDto, Domain.Entities.EmployeeApprover>(employeeApproverDto), editedBy);

                await _unitOfWork.EmployeeApproverRepository.UpdateAsync(employeeApprover);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployeeApprover", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeApproverDto>> SearchEmployeeApproversByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employeeApprover = await _unitOfWork.EmployeeApproverRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.EmpIdno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeApproverDto>>(employeeApprover);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchEmployeeApproversByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
