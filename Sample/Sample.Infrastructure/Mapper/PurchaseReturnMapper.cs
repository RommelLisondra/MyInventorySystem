using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entities = Sample.Domain.Entities;
using entityframework = Sample.Infrastructure.EntityFramework;

namespace Sample.Infrastructure.Mapper
{
    internal class PurchaseReturnMapper
    {
        public static List<entities.PurchaseReturnMaster> MapToEntityList(IEnumerable<entityframework.PurchaseReturnMasterFile> list)
        {
            if (list == null) return new List<entities.PurchaseReturnMaster>();

            return list?.Select(p => new PurchaseReturnMaster
            {
                id = p.Id,
                PretMno = p.Prmno,
                SupplierNo = p.SupplierNo,
                Date = p.Date,
                ApprBy = p.ApprovedBy ?? string.Empty,
                Remarks = p.Remarks,
                RecStatus = p.RecStatus,
                Rrno = p.Rrno,
                RefNo = p.RefNo,
                Terms = p.Terms,
                TypesOfPay = p.TypesOfPay,
                Comments = p.Comments,
                TermsAndCondition = p.TermsAndCondition,
                FooterText = p.FooterText,
                Recuring = p.Recuring,
                Total = p.Total,
                DisPercent = p.DisPercent,
                DisTotal = p.DisTotal,
                SubTotal = p.SubTotal,
                Vat = p.Vat,
                ApprByNavigation = p.ApprovedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(p.ApprovedByNavigation)
                                    : null,
                PrepByNavigation = p.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(p.PreparedByNavigation)
                                    : null,
                //public virtual Supplier? SupplierNoNavigation 
                PurchaseReturnDetailFile = p.PurchaseReturnDetailFile != null
                    ? MapToEntityPurchaseOrderDetailList(p.PurchaseReturnDetailFile)
                    : null,
            }).ToList() ?? new List<PurchaseReturnMaster>();
        }

        public static List<entities.PurchaseReturnDetail> MapToEntityPurchaseOrderDetailList(IEnumerable<entityframework.PurchaseReturnDetailFile> list)
        {
            if (list == null) return new List<entities.PurchaseReturnDetail>();

            return list.Select(a => new entities.PurchaseReturnDetail
            {
                id = a.Id,
                PretDno = a.Prmno,
                ItemDetailCode = a.ItemDetailCode,
                QtyRet = a.Quantity,
                Amount = a.Amount,
                RecStatus = a.RecStatus
            }).ToList();
        }

        public static entities.PurchaseReturnMaster MapToEntity(entityframework.PurchaseReturnMasterFile entity)
        {
            if (entity == null) return null;

            return new entities.PurchaseReturnMaster
            {
                id = entity.Id,
                PretMno = entity.Prmno,
                SupplierNo = entity.SupplierNo,
                Date = entity.Date,
                ApprBy = entity.ApprovedBy ?? string.Empty,
                Remarks = entity.Remarks,
                RecStatus = entity.RecStatus,
                Rrno = entity.Rrno,
                RefNo = entity.RefNo,
                Terms = entity.Terms,
                TypesOfPay = entity.TypesOfPay,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                ApprByNavigation = entity.ApprovedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entity.ApprovedByNavigation)
                                    : null,
                PrepByNavigation = entity.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entity.PreparedByNavigation)
                                    : null,
                PurchaseReturnDetailFile = entity.PurchaseReturnDetailFile != null
                    ? MapToEntityPurchaseOrderDetailList(entity.PurchaseReturnDetailFile)
                    : null,
            };
        }
    }
}
