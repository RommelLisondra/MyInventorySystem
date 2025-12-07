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
    internal class PurchaseRequisitionMapper
    {
        public static List<entities.PurchaseRequisitionMaster> MapToEntityList(IEnumerable<entityframework.PurchaseRequisitionMasterFile> list)
        {
            if (list == null) return new List<entities.PurchaseRequisitionMaster>();

            return list?.Select(p => new PurchaseRequisitionMaster
            {
                id = p.Id,
                Prmno = p.Prno,
                DateRequest = p.DateRequested,
                ApprovedBy = p.ApprovedBy ?? string.Empty,
                Remarks = p.Remarks,
                RecStatus = p.RecStatus,
                PurchaseRequisitionDetailFile = p.PurchaseRequisitionDetailFile != null
                    ? MapToEntityPurchaseRequisitionDetailList(p.PurchaseRequisitionDetailFile)
                    : null,
            }).ToList() ?? new List<PurchaseRequisitionMaster>();
        }

        public static List<entities.PurchaseRequisitionDetail> MapToEntityPurchaseRequisitionDetailList(IEnumerable<entityframework.PurchaseRequisitionDetailFile> list)
        {
            if (list == null) return new List<entities.PurchaseRequisitionDetail>();

            return list.Select(a => new entities.PurchaseRequisitionDetail
            {
                id = a.Id,
                Prdno = a.Prno,
                ItemDetailCode = a.ItemDetailCode,
                QtyRequested = a.QtyRequested,
                QtyOrder = a.QtyOrder,
                RecStatus = a.RecStatus
            }).ToList();
        }

        public static entities.PurchaseRequisitionDetail? MapToEntityPurchaseRequisitionDetail(entityframework.PurchaseRequisitionDetailFile? a)
        {
            if (a == null) return null;

            return new entities.PurchaseRequisitionDetail
            {
                id = a.Id,
                Prdno = a.Prno,
                ItemDetailCode = a.ItemDetailCode,
                QtyRequested = a.QtyRequested,
                QtyOrder = a.QtyOrder,
                RecStatus = a.RecStatus
            };
        }

        public static entities.PurchaseRequisitionMaster MapToEntity(entityframework.PurchaseRequisitionMasterFile entity)
        {
            if (entity == null) return null;

            return new entities.PurchaseRequisitionMaster
            {
                id = entity.Id,
                Prmno = entity.Prno,
                DateRequest = entity.DateRequested,
                ApprovedBy = entity.ApprovedBy ?? string.Empty,
                Remarks = entity.Remarks,
                RecStatus = entity.RecStatus,
                PurchaseRequisitionDetailFile = entity.PurchaseRequisitionDetailFile != null
                    ? MapToEntityPurchaseRequisitionDetailList(entity.PurchaseRequisitionDetailFile)
                    : null,
            };
        }
    }
}
