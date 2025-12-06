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
    internal class SalesInvoiceMapper
    {
        public static List<entities.SalesInvoiceMaster> MapToEntityList(IEnumerable<entityframework.SalesInvoiceMasterFile> list)
        {
            if (list == null) return new List<entities.SalesInvoiceMaster>();

            return list?.Select(p => new SalesInvoiceMaster
            {
                id = p.Id,
                Somno = p.Somno,
                CustNo = p.CustNo,
                Date = p.Date,
                Terms = p.Terms,
                PrepBy = p.PreparedBy ?? string.Empty,
                ApprBy = p.ApprovedBy ?? string.Empty,
                RecStatus = p.RecStatus,
                SalesInvoiceDetail = p.SalesInvoiceDetailFile != null
                    ? MapToEntitySalesInvoiceDetailList(p.SalesInvoiceDetailFile)
                    : null,
            }).ToList() ?? new List<SalesInvoiceMaster>();
        }

        public static List<entities.SalesInvoiceDetail> MapToEntitySalesInvoiceDetailList(IEnumerable<entityframework.SalesInvoiceDetailFile> list)
        {
            if (list == null) return new List<entities.SalesInvoiceDetail>();

            return list.Select(a => new entities.SalesInvoiceDetail
            {
                id = a.Id,
                Sidno = a.Simno,
                ItemDetailCode = a.ItemDetailCode,
                QtyInv = a.QtyInv,
                //QtyRet = a.qty
                Uprice = a.Uprice,
                Amount = a.Amount
            }).ToList();
        }

        public static entities.SalesInvoiceDetail? MapToEntitySalesInvoiceDetail(entityframework.SalesInvoiceDetailFile? a)
        {
            if (a == null) return null;

            return new entities.SalesInvoiceDetail
            {
                id = a.Id,
                Sidno = a.Simno,
                ItemDetailCode = a.ItemDetailCode,
                QtyInv = a.QtyInv,
                Uprice = a.Uprice,
                Amount = a.Amount,
                RecStatus = a.RecStatus
            };
        }

        public static entities.SalesInvoiceMaster MapToEntity(entityframework.SalesInvoiceMasterFile entitysalesorder)
        {
            if (entitysalesorder == null) return null;

            return new entities.SalesInvoiceMaster
            {
                id = entitysalesorder.Id,
                Somno = entitysalesorder.Somno,
                CustNo = entitysalesorder.CustNo,
                Date = entitysalesorder.Date,
                Terms = entitysalesorder.Terms,
                PrepBy = entitysalesorder.PreparedBy ?? string.Empty,
                ApprBy = entitysalesorder.ApprovedBy ?? string.Empty,
                RecStatus = entitysalesorder.RecStatus,
                SalesInvoiceDetail = entitysalesorder.SalesInvoiceDetailFile != null
                    ? MapToEntitySalesInvoiceDetailList(entitysalesorder.SalesInvoiceDetailFile)
                    : null,
            };
        }
    }
}
