using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class RoleModel 
    {
        public int id { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RecStatus { get; set; }

        public ICollection<RolePermissionModel>? RolePermission { get; set; }
        public ICollection<UserAccountModel>? UserAccount { get; set; }
    }
}
