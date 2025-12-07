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
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly INVENTORYDbContext _dbContext;

        public RolePermissionRepository(INVENTORYDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<entities.RolePermission>> GetAllAsync()
        {
            var efRolePermission = await GetAllRolePermissionsRawAsync();

            if (efRolePermission == null || !efRolePermission.Any())
                return Enumerable.Empty<entities.RolePermission>();

            var rolePermission = efRolePermission
                .Select(RolePermissionMapper.MapToEntity)
                .ToList();

            return rolePermission;
        }

        public async Task<PagedResult<entities.RolePermission>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            var rolePermission = await GetAllRolePermissionsRawAsync();

            if (rolePermission == null || !rolePermission.Any())
                return new PagedResult<entities.RolePermission>
                {
                    Data = Enumerable.Empty<entities.RolePermission>(),
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0
                };

            var totalRecords = rolePermission.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = rolePermission
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(RolePermissionMapper.MapToEntity)
                .ToList();

            return new PagedResult<entities.RolePermission>
            {
                Data = pagedData,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<entities.RolePermission> GetByIdAsync(int id)
        {
            var efRolePermission = await GetAllRolePermissionsRawAsync();

            if (efRolePermission == null || !efRolePermission.Any())
                return null;

            var RolePermissions = efRolePermission
                .Select(RolePermissionMapper.MapToEntity).Where(e => e.id == id)
                .FirstOrDefault();

            return RolePermissions;
        }

        public async Task AddAsync(entities.RolePermission? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "RolePermission entity cannot be null.");

            var rolePermission = RolePermissionMapper.MapToEntityFramework(entity, false);

            await _dbContext.RolePermission.AddAsync(rolePermission);
        }

        public async Task UpdateAsync(entities.RolePermission? entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "RolePermission entity cannot be null.");

            var toUpdate = await _dbContext.RolePermission.FirstOrDefaultAsync(u => u.Id == entity.id);

            if (toUpdate == null)
                throw new InvalidOperationException($"RolePermission with ID {entity.id} not found in database.");

            var updatedValues = RolePermissionMapper.MapToEntityFramework(entity, true);

            //updatedValues.CustNo = toUpdate.CustNo;

            _dbContext.Entry(toUpdate).CurrentValues.SetValues(updatedValues);
        }

        public async Task<IEnumerable<entities.RolePermission>> FindAsync(Expression<Func<entities.RolePermission, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            var efPredicate = Helpers.ConvertPredicate<entities.RolePermission, entityframework.RolePermission>(predicate);

            var efRolePermission = await _dbContext.RolePermission
                .Where(efPredicate)
                .ToListAsync();

            var result = efRolePermission.Select(RolePermissionMapper.MapToEntity);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid RolePermission ID.", nameof(id));

            var rolePermission = await _dbContext.RolePermission
                .FirstOrDefaultAsync(e => e.Id == id);

            if (rolePermission == null)
                throw new InvalidOperationException($"RolePermission with ID {id} not found.");

            _dbContext.RolePermission.Remove(rolePermission);
        }

        public async Task<IEnumerable<entityframework.RolePermission>> GetAllRolePermissionsRawAsync()
        {
            return await _dbContext.RolePermission
                //.Where(e => e.RecStatus == "A")
                .ToListAsync();
        }
    }
}
