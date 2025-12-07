using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using entityframework = Sample.Infrastructure.EntityFramework;
using entities = Sample.Domain.Entities;

namespace Sample.Infrastructure.Mapper
{
    internal class OfficialReceiptMapper
    {
        public static List<entities.OfficialReceiptMaster> MapToEntityList(IEnumerable<entityframework.OfficialReceiptMasterFile> list)
        {
            if (list == null) return new List<entities.OfficialReceiptMaster>();

            return list.Select(p => new entities.OfficialReceiptMaster
            {
                id = p.Id,
                Orno = p.Orno,
                Date = p.Date,
                CustNo = p.CustNo,
                RecStatus = p.RecStatus,
                TotalAmtPaid = p.TotAmtPaid,
                DisPercent = p.DisPercent,
                DisTotal = p.DisTotal,
                SubTotal = p.SubTotal,
                Vat = p.Vat,
                FormPay = p.FormPay,
                CashAmt = p.CashAmt,
                CheckAmt = p.CheckAmt,
                CheckNo = p.CheckNo,
                BankName = p.BankName,
                Remarks = p.Remarks,
                Comments = p.Comments,
                TermsAndCondition = p.TermsAndCondition,
                FooterText = p.FooterText,
                Recuring = p.Recuring,
                PrepBy = p.PreparedBy ?? string.Empty,

                custNoNavigation = p.CustNoNavigation != null
                                    ? CustomerMapper.MapToEntity(p.CustNoNavigation)
                                    : null,

                prepByNavigation = p.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(p.PreparedByNavigation)
                                    : null,

                OfficialReceiptDetailFile = p.OfficialReceiptDetailFile != null
                        ? MapToEntityOfficialReceiptDetailList(p.OfficialReceiptDetailFile)
                        : null,
            }).ToList();
        }

        public static List<entities.OfficialReceiptDetail> MapToEntityOfficialReceiptDetailList(IEnumerable<entityframework.OfficialReceiptDetailFile> list)
        {
            if (list == null) return new List<entities.OfficialReceiptDetail>();

            return list.Select(a => new entities.OfficialReceiptDetail
            {
                id = a.Id,
                Ordno = a.Orno,
                Simno = a.Simno,
                AmountPaid = a.AmountPaid,
                AmountDue = a.AmountDue,
                RecStatus = a.RecStatus
            }).ToList();
        }

        public static entities.OfficialReceiptMaster MapToEntity(entityframework.OfficialReceiptMasterFile entityofficialreceipt)
        {
            if (entityofficialreceipt == null) return null;

            return new entities.OfficialReceiptMaster
            {
                id = entityofficialreceipt.Id,
                Orno = entityofficialreceipt.Orno,
                Date = entityofficialreceipt.Date,
                CustNo = entityofficialreceipt.CustNo,
                RecStatus = entityofficialreceipt.RecStatus,
                TotalAmtPaid = entityofficialreceipt.TotAmtPaid,
                DisPercent = entityofficialreceipt.DisPercent,
                DisTotal = entityofficialreceipt.DisTotal,
                SubTotal = entityofficialreceipt.SubTotal,
                Vat = entityofficialreceipt.Vat,
                FormPay = entityofficialreceipt.FormPay,
                CashAmt = entityofficialreceipt.CashAmt,
                CheckAmt = entityofficialreceipt.CheckAmt,
                CheckNo = entityofficialreceipt.CheckNo,
                BankName = entityofficialreceipt.BankName,
                Remarks = entityofficialreceipt.Remarks,
                Comments = entityofficialreceipt.Comments,
                TermsAndCondition = entityofficialreceipt.TermsAndCondition,
                FooterText = entityofficialreceipt.FooterText,
                Recuring = entityofficialreceipt.Recuring,
                PrepBy = entityofficialreceipt.PreparedBy ?? string.Empty,
                custNoNavigation = entityofficialreceipt.CustNoNavigation != null
                                    ? CustomerMapper.MapToEntity(entityofficialreceipt.CustNoNavigation)
                                    : null,

                prepByNavigation = entityofficialreceipt.PreparedByNavigation != null
                                    ? EmployeeMapper.MapToEntity(entityofficialreceipt.PreparedByNavigation)
                                    : null,
                OfficialReceiptDetailFile = entityofficialreceipt.OfficialReceiptDetailFile != null
                        ? MapToEntityOfficialReceiptDetailList(entityofficialreceipt.OfficialReceiptDetailFile)
                        : null,
            };
        }
    }
}
