using Sample.Domain.Domain;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Contracts
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Retrieve all the records from the database
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieve paged records from the database
        /// </summary>
        Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieve single record from the database
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Find single or multiple records in the database
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Persist single record to the database
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Persist changes on single record to database
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Delete permanently a single record from the database
        /// </summary>
        Task RemoveAsync(int id);
    }
}
