using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> GetByLocationCodeAsync(string LocationCode);
    }
}
