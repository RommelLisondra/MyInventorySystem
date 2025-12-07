using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class CustomerMapper
    {
        public static entityframework.Customer MapToEntityFramework(entities.Customer entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.Customer
            {
                Id = includeId ? entity.id : default,         // default = 0 kung int
                CustNo = entity.CustNo ?? string.Empty,
                Guid = entity.Guid,
                Name = entity.Name,
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                Address3 = entity.Address3,
                City = entity.City,
                PostalCode = entity.PostalCode,
                Country = entity.Country,
                State = entity.State,
                EmailAddress = entity.EmailAddress,
                Fax = entity.Fax,
                MobileNo = entity.MobileNo,
                AcountNo = entity.AcountNo,
                CreditCardNo = entity.CreditCardNo,
                CreditCardType = entity.CreditCardType,
                CreditCardName = entity.CreditCardName,
                CreditCardExpiry = entity.CreditCardExpiry,
                ContactNo = entity.ContactNo,
                ContactPerson = entity.ContactPerson,
                CreditLimit = entity.CreditLimit,
                Balance = entity.Balance,
                ModifiedDateTime = entity.ModifiedDateTime,
                CreatedDateTime = entity.CreatedDateTime,
                LastSono = entity.LastSono,
                LastSino = entity.LastSino,
                LastDrno = entity.LastDrno,
                LastOr = entity.LastOr,
                LastSrno = entity.LastSrno,
                RecStatus = entity.RecStatus,
            };

            return mapped;
        }

        public static entities.Customer MapToEntity(entityframework.Customer customer)
        {
            if (customer == null) return null!;

            return new entities.Customer
            {
                id = customer.Id,
                Name = customer.Name,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                Address3 = customer.Address3,
                City = customer.City,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                State = customer.State,
                EmailAddress = customer.EmailAddress,
                Fax = customer.Fax,
                MobileNo = customer.MobileNo,
                AcountNo = customer.AcountNo,
                CreditCardNo = customer.CreditCardNo,
                CreditCardType = customer.CreditCardType,
                CreditCardName = customer.CreditCardName,
                CreditCardExpiry = customer.CreditCardExpiry,
                ContactNo = customer.ContactNo,
                ContactPerson = customer.ContactPerson,
                CreditLimit = customer.CreditLimit,
                Balance = customer.Balance,
                ModifiedDateTime = customer.ModifiedDateTime,
                CreatedDateTime = customer.CreatedDateTime,
                LastSono = customer.LastSono,
                LastSino = customer.LastSino,
                LastDrno = customer.LastDrno,
                LastOr = customer.LastOr,
                LastSrno = customer.LastSrno,
                RecStatus = customer.RecStatus,
                DeliveryReceiptMaster = DeliveryReceiptMapper.MapToEntityList(customer.DeliveryReceiptMasterFile),
                OfficialReceiptMaster = OfficialReceiptMapper.MapToEntityList(customer.OfficialReceiptMasterFile),
                SalesInvoiceMaster = SalesInvoiceMapper.MapToEntityList(customer.SalesInvoiceMasterFile),
                SalesOrderMaster = SalesOrderMapper.MapToEntityList(customer.SalesOrderMasterFile),
                SalesReturnMaster = SalesReturnMapper.MapToEntityList(customer.SalesReturnMasterFile),
            };
        }

        public static entities.CustomerImage CustomerImageMapToEntity(entityframework.CustomerImage x)
        {
            var customerimage = new entities.CustomerImage
            {
                id = x.Id,
                CustNo = x.CustNo,
                FilePath = x.FilePath,
                Picture = x.Picture
            };

            return customerimage;
        }

        public static entityframework.CustomerImage MapToEntityFramework(entities.CustomerImage entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Customer entity cannot be null.");

            var mapped = new entityframework.CustomerImage
            {
                CustNo = entity.CustNo,
                FilePath = entity.FilePath,
                Picture = entity.Picture
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }
    }
}
