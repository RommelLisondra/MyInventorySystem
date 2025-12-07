using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class DeliveryReceiptMapper
    {
        public static List<entities.DeliveryReceiptMaster> MapToEntityList(IEnumerable<entityframework.DeliveryReceiptMasterFile> list)
        {
            if (list == null) return new List<entities.DeliveryReceiptMaster>();

            return list.Select(p => new entities.DeliveryReceiptMaster
            {
                id = p.Id,
                Drmno = p.Drmno,
                CustNo = p.CustNo,
                Simno = p.Simno,
                Terms = p.Terms,
                TypesOfPay = p.TypesOfPay,
                Remarks = p.Remarks,
                Comments = p.Comments,
                TermsAndCondition = p.TermsAndCondition,
                FooterText = p.FooterText,
                Recuring = p.Recuring,
                Total = p.Total,
                DisPercent = p.DisPercent,
                DisTotal = p.DisTotal,
                DeliveryCost = p.DeliveryCost,
                SubTotal = p.SubTotal,
                Vat = p.Vat,
                ApprBy = p.ApprBy,
                PrepBy = p.PrepBy,
                RecStatus = p.RecStatus,
                ApprByNavigation = p.ApprByNavigation != null
                        ? EmployeeMapper.MapToEntity(p.ApprByNavigation)
                        : null,
                PrepByNavigation = p.PrepByNavigation != null
                        ? EmployeeMapper.MapToEntity(p.PrepByNavigation)
                        : null,

                CustNoNavigation = p.CustNoNavigation != null
                        ? CustomerMapper.MapToEntity(p.CustNoNavigation)
                        : null,

                DeliveryReceiptDetail = p.DeliveryReceiptDetailFile != null
                        ? MapToEntityDeliveryReceiptDetailList(p.DeliveryReceiptDetailFile)
                        : null,
            }).ToList();
        }

        public static List<entities.DeliveryReceiptDetail> MapToEntityDeliveryReceiptDetailList(IEnumerable<entityframework.DeliveryReceiptDetailFile> list)
        {
            if (list == null) return new List<entities.DeliveryReceiptDetail>();

            return list.Select(p => new entities.DeliveryReceiptDetail
            {
                Drdno = p.Drdno,
                ItemDetailCode = p.ItemDetailCode,
                QtyDel = p.QtyDel,
                Uprice = p.Uprice,
                Amount = p.Amount,
                RecStatus = p.RecStatus,
            }).ToList();
        }

        public static entities.DeliveryReceiptMaster MapToEntity(entityframework.DeliveryReceiptMasterFile entitydelivery)
        {
            if (entitydelivery == null) return null;

            return new entities.DeliveryReceiptMaster
            {
                id = entitydelivery.Id,
                Drmno = entitydelivery.Drmno,
                CustNo = entitydelivery.CustNo,
                Simno = entitydelivery.Simno,
                Terms = entitydelivery.Terms,
                TypesOfPay = entitydelivery.TypesOfPay,
                Remarks = entitydelivery.Remarks,
                Comments = entitydelivery.Comments,
                TermsAndCondition = entitydelivery.TermsAndCondition,
                FooterText = entitydelivery.FooterText,
                Recuring = entitydelivery.Recuring,
                Total = entitydelivery.Total,
                DisPercent = entitydelivery.DisPercent,
                DisTotal = entitydelivery.DisTotal,
                DeliveryCost = entitydelivery.DeliveryCost,
                SubTotal = entitydelivery.SubTotal,
                Vat = entitydelivery.Vat,
                ApprBy = entitydelivery.ApprBy,
                PrepBy = entitydelivery.PrepBy,
                RecStatus = entitydelivery.RecStatus,
                ApprByNavigation = entitydelivery.ApprByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entitydelivery.ApprByNavigation)
                                    : null,
                PrepByNavigation = entitydelivery.PrepByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entitydelivery.PrepByNavigation)
                                    : null,

                CustNoNavigation = entitydelivery.CustNoNavigation != null
                                    ? CustomerMapper.MapToEntity(entitydelivery.CustNoNavigation)
                                    : null,

                DeliveryReceiptDetail = entitydelivery.DeliveryReceiptDetailFile != null
                        ? MapToEntityDeliveryReceiptDetailList(entitydelivery.DeliveryReceiptDetailFile)
                        : null,
            };
        }
    }
}
