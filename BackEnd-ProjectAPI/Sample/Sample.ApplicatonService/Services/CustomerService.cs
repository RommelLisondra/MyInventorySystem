using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class CustomerService : ICustomerService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(CustomerService));

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            try
            {
                var customerList = await _unitOfWork.CustomerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Customer>, IEnumerable<CustomerDto>>(customerList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<CustomerDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<CustomerDto>>(result.Data);

                return new PagedResponse<IEnumerable<CustomerDto>>
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

        public async Task<CustomerDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Customer, CustomerDto>(await _unitOfWork.CustomerRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateCustomer(CustomerDto customerDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var customerEntity = Domain.Entities.Customer.Create(
                    _mapper.Map<CustomerDto, Domain.Entities.Customer>(customerDto), createdBy);

                await _unitOfWork.CustomerRepository.AddAsync(customerEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateCustomer", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateCustomer(CustomerDto CustomerDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var customerEntity = Domain.Entities.Customer.Update(
                    _mapper.Map<CustomerDto, Domain.Entities.Customer>(CustomerDto), editedBy);

                await _unitOfWork.CustomerRepository.UpdateAsync(customerEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateCustomer", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersByCustNoAsync(string custNo)
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.FindAsync(e => e.CustNo == custNo);
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetCustomersByCustNoAsync", ex);
                throw;
            }
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.FindAsync(e => e.id == id);
                var customer = customers.FirstOrDefault();
                return customer == null ? null : _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetCustomerByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> SearchCustomersByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var customers = await _unitOfWork.CustomerRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Name ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.CustNo ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.City ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Country ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.State ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.EmailAddress ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Address1 ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchCustomersByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
