using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeCheckerService : IEmployeeCheckerService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeCheckerService));

        public EmployeeCheckerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeCheckerDto>> GetAll()
        {
            try
            {
                var employeeCheckerList = await _unitOfWork.EmployeeCheckerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.EmployeeChecker>, IEnumerable<EmployeeCheckerDto>>(employeeCheckerList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<EmployeeCheckerDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeCheckerRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeCheckerDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeCheckerDto>>
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

        public async Task<EmployeeCheckerDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.EmployeeChecker, EmployeeCheckerDto>(await _unitOfWork.EmployeeCheckerRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateEmployeeChecker(EmployeeCheckerDto employeeCheckerDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeChecker = Domain.Entities.EmployeeChecker.Create(
                    _mapper.Map<EmployeeCheckerDto, Domain.Entities.EmployeeChecker>(employeeCheckerDto), createdBy);

                await _unitOfWork.EmployeeCheckerRepository.AddAsync(employeeChecker);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployeeChecker", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeChecker(EmployeeCheckerDto employeeCheckerDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeChecker = Domain.Entities.EmployeeChecker.Update(
                    _mapper.Map<EmployeeCheckerDto, Domain.Entities.EmployeeChecker>(employeeCheckerDto), editedBy);

                await _unitOfWork.EmployeeCheckerRepository.UpdateAsync(employeeChecker);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployeeChecker", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeCheckerDto>> SearchEmployeeCheckersByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employeeChecker = await _unitOfWork.EmployeeCheckerRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.EmpIdno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeCheckerDto>>(employeeChecker);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchEmployeeCheckersByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
