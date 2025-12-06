using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ReceivingReportDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Rrdno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyReceived { get; set; }
        public virtual int? QtyRet { get; set; }
        public virtual decimal? Uprice { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string? PretrunRecStatus { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual ReceivingReportMaster RrdnoNavigation { get; set; } = null!;

        public static ReceivingReportDetail Create(ReceivingReportDetail receivingReportDetail, string createdBy)
        {
            //Place your Business logic here
            receivingReportDetail.Created_by = createdBy;
            receivingReportDetail.Date_created = DateTime.Now;
            return receivingReportDetail;
        }

        public static ReceivingReportDetail Update(ReceivingReportDetail receivingReportDetail, string editedBy)
        {
            //Place your Business logic here
            receivingReportDetail.Edited_by = editedBy;
            receivingReportDetail.Date_edited = DateTime.Now;
            return receivingReportDetail;
        }

        public void AddPurchaseReturnQuantity(int? rrQtyreturn, int? prQtyreturn)
        {
            QtyRet = (rrQtyreturn ?? 0) + (prQtyreturn ?? 0);
            RecStatus = (QtyRet == QtyReceived) ? "C" : "O";
        }
    }
}
