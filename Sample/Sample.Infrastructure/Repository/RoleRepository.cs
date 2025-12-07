using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contracts;
using Sample.Domain.Entities;
using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Mapper;
using System.Linq.Expressions;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public RoleRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.Role>> GetAllAsync()
        {
            var efRole = await GetAllRolesRawAsync();

            if (efRole == null || !efRole.Any())
                return Enumerable.Empty<entities.Role>();

            var role = efRole
                .Select(RoleMapper.MapToEntity)
                .ToList();

            return role;
        }

        public async Task<PagedResult<entities.Role>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var role = await GetAllRolesRawAsync();

            if (role == null || !role.Any())
                return new PagedResult<entities.Role>
                {
                    Data = Enumerable.Empty<entities.Role>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = role.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = role
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(RoleMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.Role>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.Role> GetByIdAsync(int id)
        {
            var efRole = await GetAllRolesRawAsync();

            if (efRole == null || !efRole.Any())
                return null;

            var Roles = efRole
                .Select(RoleMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return Roles;
        }

        public async Task AddAsync(entities.Role? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Role entity cannot be null.");

            var role = RoleMapper.MapToEntityFramework(entity, false);

            await _dbContext.Role.AddAsync(role);
        }

        public async Task UpdateAsync(entities.Role? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Role entity cannot be null.");

            var toUpdate = await _dbContext.Role.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"Role with ID {entity.id} not found in database.");

            var updatedValues = RoleMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.Role>> FindAsync(Expression<Func<entities.Role, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.Role, entityframework.Role>(predicate);

            var efRole = await _dbContext.Role
                .Where(efPredicate)
                .ToListAsync();

            var result = efRole.Select(RoleMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Role ID.", nameof(id));

            var role = await _dbContext.Role
                .FirstOrDefaultAsync(e => e.Id == id);

            if (role == null)
                throw new InvalidOperationException($"Role with ID {id} not found.");

            _dbContext.Role.Remove(role);
        }

        public async Task<IEnumerable<entityframework.Role>> GetAllRolesRawAsync()
        {
            return await _dbContext.Role
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
