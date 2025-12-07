using System;
using System.Collections.Generic;
using Sample.Domain.Entities;
using Sample.Domain.Domain;

namespace Sample.Domain.Entities
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual string? CustNo { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Address1 { get; set; }
        public virtual string? Address2 { get; set; }
        public virtual string? Address3 { get; set; }
        public virtual string? City { get; set; }
        public virtual string? PostalCode { get; set; }
        public virtual string? Country { get; set; }
        public virtual string? State { get; set; }
        public virtual string? EmailAddress { get; set; }
        public virtual string? Fax { get; set; }
        public virtual string? MobileNo { get; set; }
        public virtual string? AcountNo { get; set; }
        public virtual string? CreditCardNo { get; set; }
        public virtual string? CreditCardType { get; set; }
        public virtual string? CreditCardName { get; set; }
        public virtual string? CreditCardExpiry { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? ContactPerson { get; set; }
        public virtual decimal? CreditLimit { get; set; }
        public virtual decimal? Balance { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual string? LastSono { get; set; }
        public virtual string? LastSino { get; set; }
        public virtual string? LastDrno { get; set; }
        public virtual string? LastOr { get; set; }
        public virtual string? LastSrno { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual DeliveryReceiptMaster? LastDrnoNavigation { get; set; }
        public virtual OfficialReceiptMaster? LastOrNavigation { get; set; }
        public virtual SalesInvoiceMaster? LastSinoNavigation { get; set; }
        public virtual SalesOrderMaster? LastSonoNavigation { get; set; }
        public virtual SalesReturnMaster? LastSrnoNavigation { get; set; }
        public virtual ICollection<DeliveryReceiptMaster>? DeliveryReceiptMaster { get; set; }
        public virtual ICollection<OfficialReceiptMaster>? OfficialReceiptMaster { get; set; }
        public virtual ICollection<SalesInvoiceMaster>? SalesInvoiceMaster { get; set; }
        public virtual ICollection<SalesOrderMaster>? SalesOrderMaster { get; set; }
        public virtual ICollection<SalesReturnMaster>? SalesReturnMaster { get; set; }

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

        public void UpdateLastSono(string sono)
        {
            if (string.IsNullOrWhiteSpace(sono))
                throw new ArgumentException("SONO cannot be empty.", nameof(sono));

            LastSono = sono;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void UpdateLastSino(string sino)
        {
            if (string.IsNullOrWhiteSpace(sino))
                throw new ArgumentException("SINO cannot be empty.", nameof(sino));

            LastSino = sino;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void UpdateLastDrno(string drno)
        {
            if (string.IsNullOrWhiteSpace(drno))
                throw new ArgumentException("DRNO cannot be empty.", nameof(drno));

            LastDrno = drno;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void UpdateLastOr(string orNo)
        {
            if (string.IsNullOrWhiteSpace(orNo))
                throw new ArgumentException("OR cannot be empty.", nameof(orNo));

            LastOr = orNo;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void UpdateLastSrno(string srno)
        {
            if (string.IsNullOrWhiteSpace(srno))
                throw new ArgumentException("SRNO cannot be empty.", nameof(srno));

            LastSrno = srno;
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void IncreaseBalance(decimal? amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount to increase must be greater than zero.", nameof(amount));

            Balance += amount;
        }

        public void DecreaseBalance(decimal? amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount to decrease must be greater than zero.", nameof(amount));

            Balance -= amount;
        }
    }
}
