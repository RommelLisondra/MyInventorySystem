using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeImageService : IEmployeeImageService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeService));

        public EmployeeImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EmployeeImageDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeImageRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeImageDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeImageDto>>
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

        public async Task<EmployeeImageDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.EmployeeImage, EmployeeImageDto>(await _unitOfWork.EmployeeImageRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task CreateEmployeeImage(EmployeeImageDto employeeImageDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeEntity = Domain.Entities.EmployeeImage.Create(
                    _mapper.Map<EmployeeImageDto, Domain.Entities.EmployeeImage>(employeeImageDto), createdBy);

                await _unitOfWork.EmployeeImageRepository.AddAsync(employeeEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployeeImage", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeImage(EmployeeImageDto employeeImageDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeEntity = Domain.Entities.EmployeeImage.Update(
                    _mapper.Map<EmployeeImageDto, Domain.Entities.EmployeeImage>(employeeImageDto), editedBy);

                await _unitOfWork.EmployeeImageRepository.UpdateAsync(employeeEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployeeImage", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeImageDto>> SearchEmployeeByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employees = await _unitOfWork.EmployeeImageRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.EmpIdno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeImageDto>>(employees);
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
