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
    internal class PurchaseOrderMapper
    {
        public static List<entities.PurchaseOrderMaster> MapToEntityList(IEnumerable<entityframework.PurchaseOrderMasterFile> list)
        {
            if (list == null) return new List<entities.PurchaseOrderMaster>();

            return list?.Select(p => new PurchaseOrderMaster
            {
                id = p.Id,
                Prno = p.Prno,
                Pomno = p.Pono,
                SupplierNo = p.SupplierNo,
                TypesofPay = p.TypesofPay,
                Terms = p.Terms,
                Date = p.Date,
                DateNeeded = p.DateNeeded,
                ReferenceNo = p.ReferenceNo,
                Total = p.Total,
                DisPercent = p.DisPercent,
                DisTotal = p.DisTotal,
                SubTotal = p.SubTotal,
                Vat = p.Vat,
                PrepBy = p.PreparedBy ?? string.Empty,
                ApprBy = p.ApprovedBy ?? string.Empty,
                Remarks = p.Remarks,
                Comments = p.Comments,
                TermsAndCondition = p.TermsAndCondition,
                FooterText = p.FooterText,
                Recuring = p.Recuring,
                RrrecStatus = p.RrrecStatus,
                PreturnRecStatus = p.PreturnRecStatus,
                RecStatus = p.RecStatus,
                ApprByNavigation = p.ApprovedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(p.ApprovedByNavigation)
                                    : null,
                PrepByNavigation = p.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(p.PreparedByNavigation)
                                    : null,
                //PrnoNavigation
                //SupNoNavigation
                PurchaseOrderDetailFile = p.PurchaseOrderDetailFile != null
                    ? MapToEntityPurchaseOrderDetailList(p.PurchaseOrderDetailFile)
                    : null,
            }).ToList() ?? new List<PurchaseOrderMaster>();
        }

        public static List<entities.PurchaseOrderDetail> MapToEntityPurchaseOrderDetailList(IEnumerable<entityframework.PurchaseOrderDetailFile> list)
        {
            if (list == null) return new List<entities.PurchaseOrderDetail>();

            return list.Select(a => new entities.PurchaseOrderDetail
            {
                id = a.Id,
                //Prdno = a.Prno,
                ItemDetailCode = a.ItemDetailCode,
                //QtyReq = a.QtyRequested,
                QtyOrder = a.QtyOrder,
                RecStatus = a.RecStatus
            }).ToList();
        }

        public static entities.PurchaseOrderDetail? MapToEntityPurchaseOrderDetail(entityframework.PurchaseOrderDetailFile? a)
        {
            if (a == null) return null;

            return new entities.PurchaseOrderDetail
            {
                id = a.Id,
                //Prdno = a.Prno,
                Podno = a.Pono,
                ItemDetailCode = a.ItemDetailCode,
                //QtyReq = a.QtyRequested,
                QtyOrder = a.QtyOrder,
                Uprice = a.Uprice,
                Amount = a.Amount,
                RecStatus = a.RecStatus
            };
        }

        public static entities.PurchaseOrderMaster MapToEntity(entityframework.PurchaseOrderMasterFile entity)
        {
            if (entity == null) return null;

            return new entities.PurchaseOrderMaster
            {
                id = entity.Id,
                Prno = entity.Prno,
                Pomno = entity.Pono,
                SupplierNo = entity.SupplierNo,
                TypesofPay = entity.TypesofPay,
                Terms = entity.Terms,
                Date = entity.Date,
                DateNeeded = entity.DateNeeded,
                ReferenceNo = entity.ReferenceNo,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                PrepBy = entity.PreparedBy ?? string.Empty,
                ApprBy = entity.ApprovedBy ?? string.Empty,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                RrrecStatus = entity.RrrecStatus,
                PreturnRecStatus = entity.PreturnRecStatus,
                RecStatus = entity.RecStatus,
                ApprByNavigation = entity.ApprovedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entity.ApprovedByNavigation)
                                    : null,
                PrepByNavigation = entity.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entity.PreparedByNavigation)
                                    : null,
                PurchaseOrderDetailFile = entity.PurchaseOrderDetailFile != null
                    ? MapToEntityPurchaseOrderDetailList(entity.PurchaseOrderDetailFile)
                    : null,
            };
        }
    }
}
