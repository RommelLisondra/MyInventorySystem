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
    internal class SalesOrderMapper
    {
        public static List<entities.SalesOrderMaster> MapToEntityList(IEnumerable<entityframework.SalesOrderMasterFile> list)
        {
            if (list == null) return new List<entities.SalesOrderMaster>();

            return list?.Select(p => new SalesOrderMaster
            {
                id = p.Id,
                Somno = p.Somno,
                CustNo = p.CustNo,
                Date = p.Date,
                Terms = p.Terms,
                PrepBy = p.PreparedBy ?? string.Empty,
                ApprBy = p.ApprovedBy ?? string.Empty,
                Remarks = p.Remarks,
                RecStatus = p.RecStatus,
                SalesOrderDetail = p.SalesOrderDetailFile != null
                    ? MapToEntitySalesOrderDetailList(p.SalesOrderDetailFile)
                    : null,
            }).ToList() ?? new List<SalesOrderMaster>();
        }

        public static List<entities.SalesOrderDetail> MapToEntitySalesOrderDetailList(IEnumerable<entityframework.SalesOrderDetailFile> list)
        {
            if (list == null) return new List<entities.SalesOrderDetail>();

            return list.Select(a => new entities.SalesOrderDetail
            {
                id = a.Id,
                Sodno = a.Somno,
                ItemDetailCode = a.ItemDetailCode,
                QtyOnOrder = a.QtyOnOrder,
                Uprice = a.Uprice,
                Amount = a.Amount,
                RecStatus = a.RecStatus,
            }).ToList();
        }

        public static entities.SalesOrderDetail? MapToEntitySalesOrderDetail(entityframework.SalesOrderDetailFile? a)
        {
            if (a == null) return null;

            return new entities.SalesOrderDetail
            {
                id = a.Id,
                Sodno = a.Somno,
                ItemDetailCode = a.ItemDetailCode,
                QtyOnOrder = a.QtyOnOrder,
                Uprice = a.Uprice,
                Amount = a.Amount,
                RecStatus = a.RecStatus
            };
        }

        public static entities.SalesOrderMaster MapToEntity(entityframework.SalesOrderMasterFile entitysalesorder)
        {
            if (entitysalesorder == null) return null;

            return new entities.SalesOrderMaster
            {
                id = entitysalesorder.Id,
                Somno = entitysalesorder.Somno,
                CustNo = entitysalesorder.CustNo,
                Date = entitysalesorder.Date,
                Terms = entitysalesorder.Terms,
                PrepBy = entitysalesorder.PreparedBy ?? string.Empty,
                ApprBy = entitysalesorder.ApprovedBy ?? string.Empty,
                Remarks = entitysalesorder.Remarks,
                RecStatus = entitysalesorder.RecStatus,
                SalesOrderDetail = entitysalesorder.SalesOrderDetailFile != null
                    ? MapToEntitySalesOrderDetailList(entitysalesorder.SalesOrderDetailFile)
                    : null,
            };
        }
    }
}
