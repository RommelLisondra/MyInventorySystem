using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class DeliveryReceiptMasterModel
    {
        public int id { get; set; }
        public string Drmno { get; set; } = null!;
        public string? CustNo { get; set; }
        public string Simno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Terms { get; set; }
        public string? TypesOfPay { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? DeliveryCost { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? PrepBy { get; set; }
        public string? ApprBy { get; set; }
        public string? RecStatus { get; set; }

        public EmployeeModel? ApprByNavigation { get; set; }
        public CustomerModel? CustNoNavigation { get; set; }
        public EmployeeModel? PrepByNavigation { get; set; }
        public ICollection<DeliveryReceiptDetailModel>? DeliveryReceiptDetail { get; set; }
    }
}
