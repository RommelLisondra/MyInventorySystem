using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IRolePermissionService : IDisposable
    {
        Task<IEnumerable<RolePermissionDto>> GetAll();
        Task<RolePermissionDto> GetById(int id);
        Task CreateRolePermission(RolePermissionDto RolePermissionDto, string createdBy);
        Task UpdateRolePermission(RolePermissionDto RolePermissionDto, string editedBy);
        Task<IEnumerable<RolePermissionDto>> SearchRolePermissionsByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<RolePermissionDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
