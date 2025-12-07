using AutoMapper;
using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.WebAPI
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerImage, CustomerImageDto>().ReverseMap();
            CreateMap<DeliveryReceiptDetail, DeliveryReceiptDetailDto>().ReverseMap();
            CreateMap<DeliveryReceiptMaster, DeliveryReceiptMasterDto>().ReverseMap();
            CreateMap<EmployeeApprover, EmployeeApproverDto>().ReverseMap();
            CreateMap<EmployeeChecker, EmployeeCheckerDto>().ReverseMap();
            CreateMap<EmployeeDelivered, EmployeeDeliveredDto>().ReverseMap();
            CreateMap<EmployeeImage, EmployeeImageDto>().ReverseMap();
            CreateMap<EmployeeSalesRef, EmployeeSalesRefDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<ItemDetail, ItemDetailDto>().ReverseMap();
            CreateMap<ItemImage, ItemImageDto>().ReverseMap();
            CreateMap<ItemSupplier, ItemSupplierDto>().ReverseMap();
            CreateMap<ItemUnitMeasure, ItemUnitMeasureDto>().ReverseMap();
            CreateMap<Location, Location>().ReverseMap();
            CreateMap<OfficialReceiptDetail, OfficialReceiptDetailDto>().ReverseMap();
            CreateMap<OfficialReceiptMaster, OfficialReceiptMasterDto>().ReverseMap();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDto>().ReverseMap();
            CreateMap<PurchaseOrderMaster, PurchaseOrderMasterDto>().ReverseMap();
            CreateMap<PurchaseRequisitionDetail, PurchaseRequisitionDetailDto>().ReverseMap();
            CreateMap<PurchaseRequisitionMaster, PurchaseRequisitionMasterDto>().ReverseMap();
            CreateMap<PurchaseReturnDetail, PurchaseReturnDetailDto>().ReverseMap();
            CreateMap<PurchaseReturnMaster, PurchaseReturnMasterDto>().ReverseMap();
            CreateMap<ReceivingReportDetail, ReceivingReportDetailDto>().ReverseMap();
            CreateMap<ReceivingReportMaster, ReceivingReportMasterDto>().ReverseMap();
            CreateMap<SalesInvoiceDetail, SalesInvoiceDetailDto>().ReverseMap();
            CreateMap<SalesInvoiceMaster, SalesInvoiceMasterDto>().ReverseMap();
            CreateMap<SalesOrderDetail, SalesOrderDetailDto>().ReverseMap();
            CreateMap<SalesOrderMaster, SalesOrderMasterDto>().ReverseMap();
            CreateMap<SalesReturnDetail, SalesReturnDetailDto>().ReverseMap();
            CreateMap<SalesReturnMaster, SalesReturnMasterDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<SupplierImage, SupplierImageDto>().ReverseMap();
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<ApprovalFlow, ApprovalFlowDto>().ReverseMap();
            CreateMap<ApprovalHistory, ApprovalHistoryDto>().ReverseMap();
            CreateMap<AuditTrail, AuditTrailDto>().ReverseMap();
            CreateMap<DocumentReference, DocumentReferenceDto>().ReverseMap();
            CreateMap<DocumentSeries, DocumentSeriesDto>().ReverseMap();
            CreateMap<InventoryAdjustment, InventoryAdjustmentDto>().ReverseMap();
            CreateMap<InventoryAdjustmentDetail, InventoryAdjustmentDetailDto>().ReverseMap();
            CreateMap<InventoryBalance, InventoryBalanceDto>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<StockCountDetail, StockCountDetailDto>().ReverseMap();
            CreateMap<StockCountMaster, StockCountMasterDto>().ReverseMap();
            CreateMap<StockTransfer, StockTransferDto>().ReverseMap();
            CreateMap<StockTransferDetail, StockTransferDetailDto>().ReverseMap();
            CreateMap<SystemLog, SystemLogDto>().ReverseMap();
            CreateMap<SystemSetting, SystemSettingDto>().ReverseMap();
            CreateMap<UserAccount, UserAccountDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ExpenseCategory, ExpenseCategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<Classification, ClassificationDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<CostingHistory, CostingHistoryDto>().ReverseMap();
            CreateMap<Holiday, HolidayDto>().ReverseMap();
            CreateMap<ItemBarcode, ItemBarcodeDto>().ReverseMap();
            CreateMap<ItemPriceHistory, ItemPriceHistoryDto>().ReverseMap();
            CreateMap<ItemWarehouseMapping, ItemWarehouseMappingDto>().ReverseMap();
        }
    }
}
