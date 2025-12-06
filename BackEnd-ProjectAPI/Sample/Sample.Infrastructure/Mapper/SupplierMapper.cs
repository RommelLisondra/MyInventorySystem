using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class SupplierMapper
    {
        public static entities.Supplier MapToEntity(entityframework.Supplier entity)
        {
            var item = new entities.Supplier
            {
                id = entity.Id,
                SupplierNo = entity.SupplierNo,
                Name = entity.Name,
                Address = entity.Address,
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                EmailAddress = entity.Email,
                FaxNo = entity.FaxNo,
                MobileNo = entity.MobileNo,
                PostalCode = entity.PostalCode,
                Notes = entity.Notes,
                ContactNo = entity.ContactNo,
                ContactPerson = entity.ContactPerson,
                LastPono = entity.LastPono,
                RecStatus = entity.RecStatus,
                ItemSuppliers = entity.ItemSupplier != null
                        ? ItemSupplierMapper.MapToEntityList(entity.ItemSupplier)
                        : null,
                PurchaseOrderMasterFiles = entity.PurchaseOrderMasterFile != null
                        ? PurchaseOrderMapper.MapToEntityList(entity.PurchaseOrderMasterFile)
                        : null,
                PurchaseReturnMasterFiles = entity.PurchaseReturnMasterFile != null
                        ? PurchaseReturnMapper.MapToEntityList(entity.PurchaseReturnMasterFile)
                        : null,
            };

            return item;
        }

        public static entityframework.Supplier MapToEntityFramework(entities.Supplier entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Item Detail entity cannot be null.");

            var mapped = new entityframework.Supplier
            {
                SupplierNo = entity.SupplierNo,
                Name = entity.Name,
                Address = entity.Address,
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                Email = entity.EmailAddress,
                FaxNo = entity.FaxNo,
                MobileNo = entity.MobileNo,
                PostalCode = entity.PostalCode,
                Notes = entity.Notes,
                ContactNo = entity.ContactNo,
                ContactPerson = entity.ContactPerson,
                LastPono = entity.LastPono,
                RecStatus = entity.RecStatus,
            };

            if (includeId)
                mapped.Id = entity.id;

            return mapped;
        }
    }
}
