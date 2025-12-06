using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.DTOs;
using Sample.ApplicationService.ServiceContract;
using Sample.Common.Logger;
using Sample.Domain.Contracts;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class UserAccountService : IUserAcountService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(UserAccountService));

        public UserAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserAccountDto>> GetAll()
        {
            try
            {
                var userAccounts = await _unitOfWork.UserAcountRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.UserAccount>, IEnumerable<UserAccountDto>>(userAccounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }
        public async Task<PagedResponse<IEnumerable<UserAccountDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.UserAcountRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<UserAccountDto>>(result.Data);

                return new PagedResponse<IEnumerable<UserAccountDto>>
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

        public async Task<UserAccountDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.UserAccount, UserAccountDto>(await _unitOfWork.UserAcountRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task<UserAccountDto> GetLoginUserAccount(string username, string password)
        {
            try
            {
                return _mapper.Map<Domain.Entities.UserAccount, UserAccountDto>(await _unitOfWork.UserAcountRepository.GetLoginUserAccount(username, password));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateUserAccount(UserAccountDto userAccountDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var userAccounts = Domain.Entities.UserAccount.Create(
                    _mapper.Map<UserAccountDto, Domain.Entities.UserAccount>(userAccountDto), createdBy);

                await _unitOfWork.UserAcountRepository.AddAsync(userAccounts);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateUserAccount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateUserAccount(UserAccountDto userAccountDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var userAccounts = Domain.Entities.UserAccount.Update(
                    _mapper.Map<UserAccountDto, Domain.Entities.UserAccount>(userAccountDto), editedBy);

                await _unitOfWork.UserAcountRepository.UpdateAsync(userAccounts);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateUserAccount", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<UserAccountDto>> SearchUserAccountsByKeywordAsync(string? keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? string.Empty;

                var userAccounts = await _unitOfWork.UserAcountRepository.FindAsync(e =>
                    string.IsNullOrEmpty(keyword) ||
                    EF.Functions.Like((e.Username ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.FullName ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.Email ?? string.Empty).ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like((e.RecStatus ?? string.Empty).ToLower(), $"%{keyword}%")
                );

                return _mapper.Map<IEnumerable<UserAccountDto>>(userAccounts);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchUserAccountsByKeywordAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
