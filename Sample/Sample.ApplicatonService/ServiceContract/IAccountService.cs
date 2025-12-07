using Sample.ApplicationService.DTOs;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IAccountService : IDisposable
    {
        Task<IEnumerable<AccountDto>> GetAll();
        Task<PagedResponse<IEnumerable<AccountDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<AccountDto> GetById(int id);
        Task CreateAccount(AccountDto accountDto, string createdBy);
        Task UpdateAccount(AccountDto accountDto, string editedBy);
        Task<IEnumerable<AccountDto>> SearchAccountsByKeywordAsync(string? keyword);
    }
}
