using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface IUserAcountRepository : IRepository<UserAccount>
    {
        Task<UserAccount> GetLoginUserAccount(string username, string passwordHash);
    }
}
