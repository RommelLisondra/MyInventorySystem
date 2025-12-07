using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeDeliveredService : IEmployeeDeliveredService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeDeliveredService));

        public EmployeeDeliveredService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDeliveredDto>> GetAll()
        {
            try
            {
                var employeeDelivered = await _unitOfWork.EmployeeDeliveredRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.EmployeeDelivered>, IEnumerable<EmployeeDeliveredDto>>(employeeDelivered);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<EmployeeDeliveredDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeDeliveredRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeDeliveredDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeDeliveredDto>>
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

        public async Task<EmployeeDeliveredDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.EmployeeDelivered, EmployeeDeliveredDto>(await _unitOfWork.EmployeeDeliveredRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateEmployeeDelivered(EmployeeDeliveredDto employeeDeliveredDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeDelivered = Domain.Entities.EmployeeDelivered.Create(
                    _mapper.Map<EmployeeDeliveredDto, Domain.Entities.EmployeeDelivered>(employeeDeliveredDto), createdBy);

                await _unitOfWork.EmployeeDeliveredRepository.AddAsync(employeeDelivered);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployeeDelivered", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeDelivered(EmployeeDeliveredDto employeeDeliveredDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeDelivered = Domain.Entities.EmployeeDelivered.Update(
                    _mapper.Map<EmployeeDeliveredDto, Domain.Entities.EmployeeDelivered>(employeeDeliveredDto), editedBy);

                await _unitOfWork.EmployeeDeliveredRepository.UpdateAsync(employeeDelivered);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployeeDelivered", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDeliveredDto>> SearchEmployeeDeliveredsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employeeDelivered = await _unitOfWork.EmployeeDeliveredRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.EmpIdno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeDeliveredDto>>(employeeDelivered);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchEmployeeDeliveredsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
