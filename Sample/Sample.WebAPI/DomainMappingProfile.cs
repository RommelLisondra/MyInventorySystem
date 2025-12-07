using AutoMapper;
using DomainEntity = Sample.Domain.Entities;
using Entity = Sample.Infrastructure.EntityFramework;

namespace Sample.WebAPI
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {

            CreateMap<DomainEntity.Employee, Entity.Employee>().ReverseMap();
            CreateMap<DomainEntity.Customer, Entity.Customer>().ReverseMap();
            CreateMap<DomainEntity.CustomerImage, Entity.CustomerImage>().ReverseMap();
            CreateMap<DomainEntity.DeliveryReceiptDetail, Entity.DeliveryReceiptDetailFile>().ReverseMap();
            CreateMap<DomainEntity.DeliveryReceiptMaster, Entity.DeliveryReceiptMasterFile>().ReverseMap();
            CreateMap<DomainEntity.EmployeeApprover, Entity.Approver>().ReverseMap();
            CreateMap<DomainEntity.EmployeeChecker, Entity.Checker>().ReverseMap();
            CreateMap<DomainEntity.EmployeeDelivered, Entity.Deliverer>().ReverseMap();
            CreateMap<DomainEntity.EmployeeImage, Entity.EmployeeImage>().ReverseMap();
            CreateMap<DomainEntity.EmployeeSalesRef, Entity.SalesRef>().ReverseMap();
            CreateMap<DomainEntity.Item, Entity.Item>().ReverseMap();
            CreateMap<DomainEntity.ItemDetail, Entity.ItemDetail>().ReverseMap();
            CreateMap<DomainEntity.ItemImage, Entity.ItemImage>().ReverseMap();
            CreateMap<DomainEntity.ItemSupplier, Entity.ItemSupplier>().ReverseMap();
            CreateMap<DomainEntity.ItemUnitMeasure, Entity.ItemUnitMeasure>().ReverseMap();
            CreateMap<DomainEntity.Location, Entity.Location>().ReverseMap();
            CreateMap<DomainEntity.OfficialReceiptDetail, Entity.OfficialReceiptDetailFile>().ReverseMap();
            CreateMap<DomainEntity.OfficialReceiptMaster, Entity.OfficialReceiptMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseOrderDetail, Entity.PurchaseOrderDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseOrderMaster, Entity.PurchaseOrderMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseRequisitionDetail, Entity.PurchaseRequisitionDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseRequisitionMaster, Entity.PurchaseRequisitionMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseReturnDetail, Entity.PurchaseReturnDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseReturnMaster, Entity.PurchaseReturnMasterFile>().ReverseMap();
            CreateMap<DomainEntity.ReceivingReportDetail, Entity.ReceivingReportDetailFile>().ReverseMap();
            CreateMap<DomainEntity.ReceivingReportMaster, Entity.ReceivingReportMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesInvoiceDetail, Entity.SalesInvoiceDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesInvoiceMaster, Entity.SalesInvoiceMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesOrderDetail, Entity.SalesOrderDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesOrderMaster, Entity.SalesOrderMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesReturnDetail, Entity.SalesReturnDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesReturnMaster, Entity.SalesReturnMasterFile>().ReverseMap();
            CreateMap<DomainEntity.Supplier, Entity.Supplier>().ReverseMap();
            CreateMap<DomainEntity.SupplierImage, Entity.SupplierImage>().ReverseMap();
            CreateMap<DomainEntity.Warehouse, Entity.Warehouse>().ReverseMap();
            CreateMap<DomainEntity.ApprovalFlow, Entity.ApprovalFlow>().ReverseMap();
            CreateMap<DomainEntity.ApprovalHistory, Entity.ApprovalHistory>().ReverseMap();
            CreateMap<DomainEntity.AuditTrail, Entity.AuditTrail>().ReverseMap();
            CreateMap<DomainEntity.DocumentReference, Entity.DocumentReference>().ReverseMap();
            CreateMap<DomainEntity.DocumentSeries, Entity.DocumentSeries>().ReverseMap();
            CreateMap<DomainEntity.InventoryAdjustment, Entity.InventoryAdjustment>().ReverseMap();
            CreateMap<DomainEntity.InventoryAdjustmentDetail, Entity.InventoryAdjustmentDetail>().ReverseMap();
            CreateMap<DomainEntity.InventoryBalance, Entity.InventoryBalance>().ReverseMap();
            CreateMap<DomainEntity.InventoryTransaction, Entity.InventoryTransaction>().ReverseMap();
            CreateMap<DomainEntity.Role, Entity.Role>().ReverseMap();
            CreateMap<DomainEntity.RolePermission, Entity.RolePermission>().ReverseMap();
            CreateMap<DomainEntity.StockCountDetail, Entity.StockCountDetail>().ReverseMap();
            CreateMap<DomainEntity.StockCountMaster, Entity.StockCountMaster>().ReverseMap();
            CreateMap<DomainEntity.StockTransfer, Entity.StockTransfer>().ReverseMap();
            CreateMap<DomainEntity.StockTransferDetail, Entity.StockTransferDetail>().ReverseMap();
            CreateMap<DomainEntity.SystemLog, Entity.SystemLog>().ReverseMap();
            CreateMap<DomainEntity.SystemSetting, Entity.SystemSetting>().ReverseMap();
            CreateMap<DomainEntity.UserAccount, Entity.UserAccount>().ReverseMap();

            CreateMap<DomainEntity.Brand, Entity.Brand>().ReverseMap();
            CreateMap<DomainEntity.Category, Entity.Category>().ReverseMap();
            CreateMap<DomainEntity.ExpenseCategory, Entity.ExpenseCategory>().ReverseMap();
            CreateMap<DomainEntity.SubCategory, Entity.SubCategory>().ReverseMap();
            CreateMap<DomainEntity.Classification, Entity.Classification>().ReverseMap();
            CreateMap<DomainEntity.Expense, Entity.Expense>().ReverseMap();

            CreateMap<DomainEntity.Account, Entity.Account>().ReverseMap();
            CreateMap<DomainEntity.Company, Entity.Company>().ReverseMap();
            CreateMap<DomainEntity.Branch, Entity.Branch>().ReverseMap();
            CreateMap<DomainEntity.CostingHistory, Entity.CostingHistory>().ReverseMap();
            CreateMap<DomainEntity.Holiday, Entity.Holiday>().ReverseMap();
            CreateMap<DomainEntity.ItemBarcode, Entity.ItemBarcode>().ReverseMap();
            CreateMap<DomainEntity.ItemPriceHistory, Entity.ItemPriceHistory>().ReverseMap();
            CreateMap<DomainEntity.ItemWarehouseMapping, Entity.ItemWarehouseMapping>().ReverseMap();
        }
    }
}
