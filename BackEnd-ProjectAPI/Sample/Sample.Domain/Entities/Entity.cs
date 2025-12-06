using System;

namespace Sample.Domain.Entities
{
    public abstract class EntityBase : IAuditable
    {
        private DateTime _date_edited;

        public virtual DateTime Date_edited
        {
            get { return _date_edited < DateTime.Parse("1753-01-01") ? DateTime.Now : _date_edited; }
            set { _date_edited = value; }
        }

        public virtual string Edited_by { get; set; }

        private DateTime _date_created;

        public virtual DateTime Date_created
        {
            get { return _date_created < DateTime.Parse("1753-01-01") ? DateTime.Now : _date_created; }
            set { _date_created = value; }
        }

        public virtual string Created_by { get; set; }
    }

    public interface IAuditable
    {
        DateTime Date_edited { get; set; }
        string Edited_by { get; set; }
        DateTime Date_created { get; set; }
        string Created_by { get; set; }
    }
}
