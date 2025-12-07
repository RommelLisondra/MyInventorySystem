using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.Domain.Entities;

namespace Sample.Domain.Contracts
{
    public interface IItemUnitMeasureRepository : IRepository<ItemUnitMeasure>
    {
        Task<IEnumerable<ItemUnitMeasure>> SearchAsync(string? keyword, string searchBy);
    }
}
