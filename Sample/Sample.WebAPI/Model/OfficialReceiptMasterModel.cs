using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class OfficialReceiptMasterModel
    {
        public int id { get; set; }
        public string Orno { get; set; } = null!;
        public string? CustNo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalAmtPaid { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? FormPay { get; set; }
        public decimal? CashAmt { get; set; }
        public decimal? CheckAmt { get; set; }
        public string? CheckNo { get; set; }
        public string? BankName { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public string? PrepBy { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;
        public CustomerModel? custNoNavigation { get; set; }
        public EmployeeModel? prepByNavigation { get; set; }
        public ICollection<OfficialReceiptDetailModel>? OfficialReceiptDetailFile { get; set; }
    }
}
