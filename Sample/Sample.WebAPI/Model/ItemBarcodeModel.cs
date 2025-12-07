using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class ItemBarcodeModel
    {
        public virtual int id { get; set; }               // Primary key
        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual string Barcode { get; set; } = null!;
        public virtual string? Description { get; set; }  // Optional note
        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ItemModel Item { get; set; } = null!;
    }
}
