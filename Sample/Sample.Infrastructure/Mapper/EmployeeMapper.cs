using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class EmployeeMapper
    {
        public static entities.Employee MapToEntity(entityframework.Employee x)
        {
            if (x == null) return null;

            return new entities.Employee
            {
                id = x.Id,
                EmpId = x.EmpId,
                EmpIdno = x.EmpIdno,
                LastName = x.LastName,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                Age = x.Age,
                Gender = x.Gender,
                Mstatus = x.Mstatus,
                Religion = x.Religion,
                EduAttentment = x.EduAttentment,
                DateHired = x.DateHired,
                Department = x.Department,
                Position = x.Position,
                ContactNo = x.ContactNo,
                PostalCode = x.PostalCode,
                Country = x.Country,
                State = x.State,
                EmailAddress = x.EmailAddress,
                Fax = x.Fax,
                MobileNo = x.MobileNo,
                City = x.City,
                ModifiedDateTime = x.ModifiedDateTime,
                CreatedDateTime = x.CreatedDateTime,
                RecStatus = x.RecStatus,
                DeliveryReceiptMasterFileApprByNavigations = DeliveryReceiptMapper.MapToEntityList(x.DeliveryReceiptMasterFileApprByNavigation),
                DeliveryReceiptMasterFilePrepByNavigations = DeliveryReceiptMapper.MapToEntityList(x.DeliveryReceiptMasterFilePrepByNavigation),
                OfficialReceiptMasterFiles = OfficialReceiptMapper.MapToEntityList(x.OfficialReceiptMasterFileApprovedByNavigation),
                SalesInvoiceMasterFileApprByNavigations = SalesInvoiceMapper.MapToEntityList(x.SalesInvoiceMasterFileApprovedByNavigation),
                SalesInvoiceMasterFilePrepByNavigations = SalesInvoiceMapper.MapToEntityList(x.SalesInvoiceMasterFilePreparedByNavigation),
                SalesReturnMasterFileApprByNavigations = SalesReturnMapper.MapToEntityList(x.SalesReturnMasterFileApprovedByNavigation),
                SalesReturnMasterFilePrepByNavigations = SalesReturnMapper.MapToEntityList(x.SalesReturnMasterFilePreparedByNavigation)
            };
        }

        public static entityframework.Employee MapToEntityFramework(entities.Employee entity, bool includeId = false)
        {
            if (entity == null) return null;

            var mapped = new entityframework.Employee
            {
                Id = includeId ? entity.id : 0,
                EmpId = entity.EmpId,
                EmpIdno = entity.EmpIdno,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                Address = entity.Address,
                DateOfBirth = entity.DateOfBirth,
                Age = entity.Age,
                Gender = entity.Gender,
                Mstatus = entity.Mstatus,
                Religion = entity.Religion,
                EduAttentment = entity.EduAttentment,
                DateHired = entity.DateHired,
                Department = entity.Department,
                Position = entity.Position,
                ContactNo = entity.ContactNo,
                PostalCode = entity.PostalCode,
                Country = entity.Country,
                State = entity.State,
                EmailAddress = entity.EmailAddress,
                Fax = entity.Fax,
                MobileNo = entity.MobileNo,
                City = entity.City,
                ModifiedDateTime = entity.ModifiedDateTime,
                CreatedDateTime = entity.CreatedDateTime,
                RecStatus = entity.RecStatus,
                Guid = includeId ? entity.Guid : Guid.NewGuid()
            };

            return mapped;
        }

        public static entities.EmployeeSalesRef MapToSalesRefEntity(entityframework.SalesRef salesref)
        {
            if (salesref == null) return null!;

            return new entities.EmployeeSalesRef
            {
                id = salesref.Id,
                EmpIdno = salesref.EmpIdno,
                EmpIdnoNavigation = salesref.EmpIdnoNavigation != null
                                    ? MapToEntity(salesref.EmpIdnoNavigation)
                                    : throw new ArgumentNullException(nameof(salesref.EmpIdnoNavigation), "EmpIdnoNavigation cannot be null")
            };
        }

        public static entityframework.SalesRef MapToEntityFramework(entities.EmployeeSalesRef entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var mapped = new entityframework.SalesRef
            {
                EmpIdno = entity.EmpIdno,
                RecStatus = entity.RecStatus
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entityframework.EmployeeImage MapToEntityFramework(entities.EmployeeImage entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var mapped = new entityframework.EmployeeImage
            {
                EmpIdno = entity.EmpIdno,
                ImagePath = entity.FilePath
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.EmployeeImage MapToEmployeeImage(entityframework.EmployeeImage employeeImage)
        {
            if (employeeImage == null) return null!;

            return new entities.EmployeeImage
            {
                id = employeeImage.Id,
                EmpIdno = employeeImage.EmpIdno,
                EmpIdnoNavigation = employeeImage.EmpIdnoNavigation != null
                                    ? MapToEntity(employeeImage.EmpIdnoNavigation)
                                    : throw new ArgumentNullException(nameof(employeeImage.EmpIdnoNavigation), "EmpIdnoNavigation cannot be null")
            };
        }

        public static entityframework.Checker MapToEntityFramework(entities.EmployeeChecker entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Checker entity cannot be null.");

            var mapped = new entityframework.Checker
            {
                EmpIdno = entity.EmpIdno,
                RecStatus = entity.RecStatus
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.EmployeeChecker MapToChecker(entityframework.Checker checker)
        {
            if (checker == null) return null!;

            return new entities.EmployeeChecker
            {
                id = checker.Id,
                EmpIdno = checker.EmpIdno,
                EmpIdnoNavigation = checker.EmpIdnoNavigation != null
                                    ? MapToEntity(checker.EmpIdnoNavigation)
                                    : throw new ArgumentNullException(nameof(checker.EmpIdnoNavigation), "EmpIdnoNavigation cannot be null")
            };
        }

        public static entityframework.Deliverer MapToEntityFramework(entities.EmployeeDelivered entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Deliverer entity cannot be null.");

            var mapped = new entityframework.Deliverer
            {
                EmpIdno = entity.EmpIdno,
                RecStatus = entity.RecStatus
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }

        public static entities.EmployeeDelivered MapToDelivered(entityframework.Deliverer deliverer)
        {
            if (deliverer == null) return null!;

            return new entities.EmployeeDelivered
            {
                id = deliverer.Id,
                EmpIdno = deliverer.EmpIdno,
                EmpIdnoNavigation = deliverer.EmpIdnoNavigation != null
                                    ? MapToEntity(deliverer.EmpIdnoNavigation)
                                    : throw new ArgumentNullException(nameof(deliverer.EmpIdnoNavigation), "EmpIdnoNavigation cannot be null")
            };
        }

        public static entities.EmployeeApprover MapToApprover(entityframework.Approver approver)
        {
            if (approver == null) return null!;

            return new entities.EmployeeApprover
            {
                id = approver.Id,
                EmpIdno = approver.EmpIdno,
                EmpIdnoNavigation = approver.EmpIdnoNavigation != null ? MapToEntity(approver.EmpIdnoNavigation) : default!
            };
        }

        public static entityframework.Approver MapToEntityFramework(entities.EmployeeApprover entity, bool includeId = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee Approver entity cannot be null.");

            var mapped = new entityframework.Approver
            {
                EmpIdno = entity.EmpIdno,
            };

            if (includeId)
            {
                mapped.Id = entity.id;
            }

            return mapped;
        }
    }
}
