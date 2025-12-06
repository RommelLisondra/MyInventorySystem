using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class PurchaseRequisitionDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string Prdno { get; set; } = null!;
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual int? QtyRequested { get; set; }
        public virtual int? QtyOrder { get; set; }
        public virtual string? Uom { get; set; }
        public virtual string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseRequisitionMaster PrdnoNavigation { get; set; } = null!;

        public static Customer Create(Customer customer, string createdBy)
        {
            //Place your Business logic here
            customer.Created_by = createdBy;
            customer.Date_created = DateTime.Now;
            return customer;
        }

        public static Customer Update(Customer customer, string editedBy)
        {
            //Place your Business logic here
            customer.Edited_by = editedBy;
            customer.Date_edited = DateTime.Now;
            return customer;
        }

        public void AddRequisitionQtyOnOrder(int? qty)
        {
            QtyOrder += qty;
            RecStatus = (QtyOrder == QtyRequested) ? "C" : "O";
        }
    }
}
