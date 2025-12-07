using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class Role : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }

        public virtual int RoleId { get; set; }
        public virtual string RoleName { get; set; } = null!;
        public virtual string? Description { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ICollection<RolePermission>? RolePermission { get; set; }
        public virtual ICollection<UserAccount>? UserAccount { get; set; }

        public static Role Create(Role role, string createdBy)
        {
            //Place your Business logic here
            role.Created_by = createdBy;
            role.Date_created = DateTime.Now;
            return role;
        }

        public static Role Update(Role role, string editedBy)
        {
            //Place your Business logic here
            role.Edited_by = editedBy;
            role.Date_edited = DateTime.Now;
            return role;
        }
    }
}
