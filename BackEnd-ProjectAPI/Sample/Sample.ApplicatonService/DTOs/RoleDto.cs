using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class RoleDto 
    {
        public int id { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RecStatus { get; set; }

        public ICollection<RolePermissionDto>? RolePermission { get; set; }
        public ICollection<UserAccountDto>? UserAccount { get; set; }
    }
}
