using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.ServiceContract
{
    public interface IRoleService : IDisposable
    {
        Task<IEnumerable<RoleDto>> GetAll(); 
        Task<RoleDto> GetById(int id);
        Task CreateRole(RoleDto RoleDto, string createdBy);
        Task UpdateRole(RoleDto RoleDto, string editedBy);
        Task<IEnumerable<RoleDto>> SearchRolesByKeywordAsync(string? keyword);
        Task<PagedResponse<IEnumerable<RoleDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
