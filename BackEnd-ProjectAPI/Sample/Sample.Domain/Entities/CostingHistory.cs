using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class CostingHistory : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }               // Primary key
        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual decimal Cost { get; set; }         // Cost at this point in time
        public virtual DateTime EffectiveDate { get; set; } = DateTime.Now;
        public virtual string? Remarks { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation
        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;

        public static CostingHistory Create(CostingHistory costingHistory, string createdBy)
        {
            //Place your Business logic here
            costingHistory.Created_by = createdBy;
            costingHistory.Date_created = DateTime.Now;
            return costingHistory;
        }

        public static CostingHistory Update(CostingHistory costingHistory, string editedBy)
        {
            //Place your Business logic here
            costingHistory.Edited_by = editedBy;
            costingHistory.Date_edited = DateTime.Now;
            return costingHistory;
        }
    }
}
