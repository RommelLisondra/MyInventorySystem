using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class UserAccount : EntityBase, IAggregateRoot   
    {
        public virtual int id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Username { get; set; } = null!;
        public virtual string PasswordHash { get; set; } = null!;
        public virtual string? FullName { get; set; }
        public virtual string? Email { get; set; }
        public virtual int? RoleId { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual Role? Role { get; set; }

        public static UserAccount Create(UserAccount userAccount, string createdBy)
        {
            //Place your Business logic here
            userAccount.Created_by = createdBy;
            userAccount.Date_created = DateTime.Now;
            return userAccount;
        }

        public static UserAccount Update(UserAccount userAccount, string editedBy)
        {
            //Place your Business logic here
            userAccount.Edited_by = editedBy;
            userAccount.Date_edited = DateTime.Now;
            return userAccount;
        }
    }
}
