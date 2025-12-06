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
    internal class ReceivingReportMapper
    {
        public static List<entities.ReceivingReportMaster> MapToEntityList(IEnumerable<entityframework.ReceivingReportMasterFile> list)
        {
            if (list == null) return new List<entities.ReceivingReportMaster>();

            return list?.Select(p => new ReceivingReportMaster
            {
                id = p.Id,
                Rrmno = p.Rrno,
                Date = p.Date,
                SupNo = p.SupplierNo,
                RefNo = p.RefNo,
                Pono = p.Pono,
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
                SubTotal = p.SubTotal,
                Vat = p.Vat,
                ReceivedBy = p.ReceivedBy ?? string.Empty,
                PreturnRecStatus = p.PreturnRecStatus,
                RecStatus = p.RecStatus,
                ReceivingReportDetailFile = p.ReceivingReportDetailFile != null
                    ? MapToEntityReceivingReportDetailList(p.ReceivingReportDetailFile)
                    : null,
            }).ToList() ?? new List<ReceivingReportMaster>();
        }

        public static List<entities.ReceivingReportDetail> MapToEntityReceivingReportDetailList(IEnumerable<entityframework.ReceivingReportDetailFile> list)
        {
            if (list == null) return new List<entities.ReceivingReportDetail>();

            return list.Select(a => new entities.ReceivingReportDetail
            {
                id = a.Id,
                Rrdno = a.Rrno,
                ItemDetailCode = a.ItemDetailCode,
                QtyReceived = a.QtyReceived,
                QtyRet = a.QtyReturn,
                Uprice = a.Uprice,
                Amount = a.Amount,
                PretrunRecStatus = a.PretrunRecStatus,
                RecStatus = a.RecStatus
            }).ToList();
        }

        public static entities.ReceivingReportDetail? MapToEntityReceivingReportDetail(entityframework.ReceivingReportDetailFile? a)
        {
            if (a == null) return null;

            return new entities.ReceivingReportDetail
            {
                id = a.Id,
                Rrdno = a.Rrno,
                ItemDetailCode = a.ItemDetailCode,
                QtyReceived = a.QtyReceived,
                QtyRet = a.QtyReturn,
                Uprice = a.Uprice,
                Amount = a.Amount,
                PretrunRecStatus = a.PretrunRecStatus,
                RecStatus = a.RecStatus
            };
        }

        public static entities.ReceivingReportMaster MapToEntity(entityframework.ReceivingReportMasterFile entity)
        {
            if (entity == null) return null;

            return new entities.ReceivingReportMaster
            {
                id = entity.Id,
                Rrmno = entity.Rrno,
                Date = entity.Date,
                SupNo = entity.SupplierNo,
                RefNo = entity.RefNo,
                Pono = entity.Pono,
                Terms = entity.Terms,
                TypesOfPay = entity.TypesOfPay,
                Remarks = entity.Remarks,
                Comments = entity.Comments,
                TermsAndCondition = entity.TermsAndCondition,
                FooterText = entity.FooterText,
                Recuring = entity.Recuring,
                Total = entity.Total,
                DisPercent = entity.DisPercent,
                DisTotal = entity.DisTotal,
                SubTotal = entity.SubTotal,
                Vat = entity.Vat,
                ReceivedBy = entity.ReceivedBy ?? string.Empty,
                PreturnRecStatus = entity.PreturnRecStatus,
                RecStatus = entity.RecStatus,
                ReceivingReportDetailFile = entity.ReceivingReportDetailFile != null
                    ? MapToEntityReceivingReportDetailList(entity.ReceivingReportDetailFile)
                    : null,
            };
        }
    }
}
