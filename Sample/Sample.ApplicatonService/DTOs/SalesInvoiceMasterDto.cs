using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class SalesInvoiceMasterDto 
    {
        public int id { get; set; }
        public string Simno { get; set; } = null!;
        public string Somno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string CustNo { get; set; } = null!;
        public string? Terms { get; set; }
        public string? TypesOfPay { get; set; }
        public string? SalesRef { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? DeliveryCost { get; set; }
        public decimal? Balance { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public DateTime? DueDate { get; set; }
        public string PrepBy { get; set; } = null!;
        public string ApprBy { get; set; } = null!;
        public string? OrrecStatus { get; set; }
        public string? DrrecStatus { get; set; }
        public string? SrrecStatus { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchDto Branch { get; set; } = null!;
        public EmployeeDto ApprByNavigation { get; set; } = null!;
        public CustomerDto? CustNoNavigation { get; set; }
        public EmployeeDto PrepByNavigation { get; set; } = null!;
        public SalesOrderMasterDto SomnoNavigation { get; set; } = null!;
        public SalesReturnMasterDto? SalesReturnMasterFile { get; set; }
        public ICollection<SalesInvoiceDetailDto>? SalesInvoiceDetail { get; set; }
    }
}
