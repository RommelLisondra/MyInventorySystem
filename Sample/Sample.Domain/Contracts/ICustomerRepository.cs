using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task UpdateFieldAsync<TProperty>(int customerId, string propertyName, TProperty newValue);
        Task<Customer> GetByCustNoAsync(string custNo);
    }
}
