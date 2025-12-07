using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RecStatus { get; set; }

        public virtual Role? Role { get; set; }
    }
}
