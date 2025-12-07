using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RecStatus { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
