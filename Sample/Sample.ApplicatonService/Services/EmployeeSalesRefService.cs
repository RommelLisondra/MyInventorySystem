using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class EmployeeSalesRefService : IEmployeeSalesRefService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(EmployeeSalesRefService));

        public EmployeeSalesRefService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeSalesRefDto>> GetAll()
        {
            try
            {
                var employeeSalesRef = await _unitOfWork.EmployeeSalesRefRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.EmployeeSalesRef>, IEnumerable<EmployeeSalesRefDto>>(employeeSalesRef);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<EmployeeSalesRefDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.EmployeeSalesRefRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<EmployeeSalesRefDto>>(result.Data);

                return new PagedResponse<IEnumerable<EmployeeSalesRefDto>>
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

        public async Task<EmployeeSalesRefDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.EmployeeSalesRef, EmployeeSalesRefDto>(await _unitOfWork.EmployeeSalesRefRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateEmployeeSalesRef(EmployeeSalesRefDto employeeSalesRefDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeSalesRef = Domain.Entities.EmployeeSalesRef.Create(
                    _mapper.Map<EmployeeSalesRefDto, Domain.Entities.EmployeeSalesRef>(employeeSalesRefDto), createdBy);

                await _unitOfWork.EmployeeSalesRefRepository.AddAsync(employeeSalesRef);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateEmployeeSalesRef", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeSalesRef(EmployeeSalesRefDto employeeSalesRefDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var employeeSalesRef = Domain.Entities.EmployeeSalesRef.Update(
                    _mapper.Map<EmployeeSalesRefDto, Domain.Entities.EmployeeSalesRef>(employeeSalesRefDto), editedBy);

                await _unitOfWork.EmployeeSalesRefRepository.UpdateAsync(employeeSalesRef);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateEmployeeSalesRef", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeSalesRefDto>> SearchEmployeeSalesRefsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var employeeSalesRef = await _unitOfWork.EmployeeSalesRefRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.EmpIdno ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<EmployeeSalesRefDto>>(employeeSalesRef);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchEmployeeSalesRefsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
