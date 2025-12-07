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
    internal class SalesReturnMapper
    {

        public static List<entities.SalesReturnMaster> MapToEntityList(IEnumerable<entityframework.SalesReturnMasterFile> list)
        {
            if (list == null) return new List<entities.SalesReturnMaster>();

            return list?.Select(p => new SalesReturnMaster
            {
                id = p.Id,
                //Somno = p.Somno,
                CustNo = p.CustNo,
                Date = p.Date,
                //Terms = p.Terms,
                PrepBy = p.PreparedBy ?? string.Empty,
                ApprBy = p.ApprovedBy ?? string.Empty,
                Remarks = p.Remarks,
                RecStatus = p.RecStatus,
                SalesReturnDetailFile = p.SalesReturnDetailFile != null
                    ? MapToEntitySalesReturnDetailList(p.SalesReturnDetailFile)
                    : null,
            }).ToList() ?? new List<SalesReturnMaster>();
        }

        public static List<entities.SalesReturnDetail> MapToEntitySalesReturnDetailList(IEnumerable<entityframework.SalesReturnDetailFile> list)
        {
            if (list == null) return new List<entities.SalesReturnDetail>();

            return list.Select(a => new entities.SalesReturnDetail
            {
                id = a.Id,
                Srdno = a.Srmno,
                ItemDetailCode = a.ItemDetailCode,
                QtyRet = a.Qty,
                Uprice = a.Uprice,
                Amount = a.Amount
            }).ToList();
        }

        public static entities.SalesReturnMaster MapToEntity(entityframework.SalesReturnMasterFile entity)
        {
            if (entity == null) return null;

            return new entities.SalesReturnMaster
            {
                id = entity.Id,
                //Simno = entity.si,
                CustNo = entity.CustNo,
                Date = entity.Date,
                //Terms = entity.ter,
                PrepBy = entity.PreparedBy ?? string.Empty,
                ApprBy = entity.ApprovedBy ?? string.Empty,
                Remarks = entity.Remarks,
                RecStatus = entity.RecStatus,
                SalesReturnDetailFile = entity.SalesReturnDetailFile != null
                    ? MapToEntitySalesReturnDetailList(entity.SalesReturnDetailFile)
                    : null,
            };
        }
    }
}
