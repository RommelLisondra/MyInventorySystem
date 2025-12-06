using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.Domain.Entities;

namespace Sample.Domain.Contracts
{
    public interface IItemDetailRepository : IRepository<ItemDetail>
    {
        Task<ItemDetail> GetByItemdetailCodeAsync(string itemDetailCode);
        Task UpdateFieldAsync<TProperty>(int itemId, string propertyName, TProperty newValue);
    }
}
