using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class RolePermission : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int RoleId { get; set; }
        public virtual string PermissionName { get; set; } = null!;
        public virtual bool CanView { get; set; }
        public virtual bool CanAdd { get; set; }
        public virtual bool CanEdit { get; set; }
        public virtual bool CanDelete { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual Role Role { get; set; } = null!;

        public static RolePermission Create(RolePermission role, string createdBy)
        {
            //Place your Business logic here
            role.Created_by = createdBy;
            role.Date_created = DateTime.Now;
            return role;
        }

        public static RolePermission Update(RolePermission role, string editedBy)
        {
            //Place your Business logic here
            role.Edited_by = editedBy;
            role.Date_edited = DateTime.Now;
            return role;
        }
    }
}
