using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class SystemLog : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual int Id { get; set; }
        public virtual string LogType { get; set; } = null!;
        public virtual string Message { get; set; } = null!;
        public virtual string? StackTrace { get; set; }
        public virtual string? LoggedBy { get; set; }
        public virtual DateTime LoggedDate { get; set; }
        public virtual string? RecStatus { get; set; }

        public static SystemLog Create(SystemLog systemLog, string createdBy)
        {
            //Place your Business logic here
            systemLog.Created_by = createdBy;
            systemLog.Date_created = DateTime.Now;
            return systemLog;
        }

        public static SystemLog Update(SystemLog systemLog, string editedBy)
        {
            //Place your Business logic here
            systemLog.Edited_by = editedBy;
            systemLog.Date_edited = DateTime.Now;
            return systemLog;
        }
    }
}
