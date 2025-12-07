using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IUserAcountService : IDisposable
    {
        Task<IEnumerable<UserAccountDto>> GetAll();
        Task<UserAccountDto> GetById(int id);
        Task CreateUserAccount(UserAccountDto UserAccountDto, string createdBy);
        Task UpdateUserAccount(UserAccountDto UserAccountDto, string editedBy);
        Task<IEnumerable<UserAccountDto>> SearchUserAccountsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<UserAccountDto>>> GetAllPaged(int pageNumber, int pageSize);
        Task<UserAccountDto> GetLoginUserAccount(string username, string password);
    }
}
