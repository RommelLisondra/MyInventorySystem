using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class AccountService : IAccountService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(AccountService));

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Account>, IEnumerable<AccountDto>>(accountList);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<AccountDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.AccountRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<AccountDto>>(result.Data);

                return new PagedResponse<IEnumerable<AccountDto>>
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

        public async Task<AccountDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Account, AccountDto>(await _unitOfWork.AccountRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateAccount(AccountDto accountDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var accountEntity = Domain.Entities.Account.Create(
                    _mapper.Map<AccountDto, Domain.Entities.Account>(accountDto), createdBy);

                await _unitOfWork.AccountRepository.AddAsync(accountEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateAccount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateAccount(AccountDto accountDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var accountEntity = Domain.Entities.Account.Update(
                    _mapper.Map<AccountDto, Domain.Entities.Account>(accountDto), editedBy);

                await _unitOfWork.AccountRepository.UpdateAsync(accountEntity);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateAccount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<AccountDto>> SearchAccountsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var accounts = await _unitOfWork.AccountRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.AccountCode ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.AccountName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Description ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<AccountDto>>(accounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchAccountsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
