using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class RolePermissionModel 
    {
        public int id { get; set; }
        public int RoleId { get; set; }
        public string PermissionName { get; set; } = null!;
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public string? RecStatus { get; set; }

        public RoleModel Role { get; set; } = null!;
    }
}
