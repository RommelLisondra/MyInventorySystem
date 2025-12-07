using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeService : IEmployeeService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeService));

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var employeeList = await _unitOfWork.EmployeeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Employee>, IEnumerable<EmployeeDto>>(employeeList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<EmployeeDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeDto>>
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

        public async Task<EmployeeDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Employee, EmployeeDto>(await _unitOfWork.EmployeeRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task CreateEmployee(EmployeeDto employeeDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeEntity = Domain.Entities.Employee.Create(
                    _mapper.Map<EmployeeDto, Domain.Entities.Employee>(employeeDto), createdBy);

                await _unitOfWork.EmployeeRepository.AddAsync(employeeEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployee", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeDto employeeDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeEntity = Domain.Entities.Employee.Update(
                    _mapper.Map<EmployeeDto, Domain.Entities.Employee>(employeeDto), editedBy);

                await _unitOfWork.EmployeeRepository.UpdateAsync(employeeEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployee", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentAsync(string department)
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.FindAsync(e => e.Department == department);
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetEmployeesByDepartmentAsync", ex);
                throw;
            }
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.FindAsync(e => e.id == id);
                var employee = employees.FirstOrDefault();
                return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetEmployeeByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeeByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employees = await _unitOfWork.EmployeeRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.LastName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.FirstName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.MiddleName ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchEmployeeByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
