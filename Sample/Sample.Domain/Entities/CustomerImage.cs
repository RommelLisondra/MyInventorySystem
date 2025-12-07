using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class CustomerImage : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string CustNo { get; set; } = null!;
        public virtual string? FilePath { get; set; }
        public virtual byte[]? Picture { get; set; }

        public virtual Customer CustNoNavigation { get; set; } = null!;

        public static Customer Create(Customer customer, string createdBy)
        {
            //Place your Business logic here
            customer.Created_by = createdBy;
            customer.Date_created = DateTime.Now;
            return customer;
        }

        public static Customer Update(Customer customer, string editedBy)
        {
            //Place your Business logic here
            customer.Edited_by = editedBy;
            customer.Date_edited = DateTime.Now;
            return customer;
        }
    }
}
