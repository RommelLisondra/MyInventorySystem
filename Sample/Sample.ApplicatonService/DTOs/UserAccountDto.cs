using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class UserAccountDto 
    {
        public int id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RecStatus { get; set; }

        public RoleDto? Role { get; set; }
    }
}
