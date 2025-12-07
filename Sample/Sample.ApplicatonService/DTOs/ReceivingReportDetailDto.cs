using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ReceivingReportDetailDto 
    {
        public int id { get; set; }
        public string Rrdno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int QtyReceived { get; set; }
        public int? QtyRet { get; set; }
        public decimal Uprice { get; set; }
        public decimal Amount { get; set; }
        public string? PretrunRecStatus { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
        public ReceivingReportMasterDto RrdnoNavigation { get; set; } = null!;
    }
}
