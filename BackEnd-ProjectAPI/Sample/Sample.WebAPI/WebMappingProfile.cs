using AutoMapper;
using Sample.ApplicationService.DTOs;
using Sample.WebAPI.Model;

namespace Sample.WebAPI
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CustomerDto, CustomerModel>().ReverseMap();
            CreateMap<CustomerImageDto, CustomerImageModel>().ReverseMap();
            CreateMap<DeliveryReceiptDetailDto, DeliveryReceiptDetailModel>().ReverseMap();
            CreateMap<DeliveryReceiptMasterDto, DeliveryReceiptMasterModel>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeModel>().ReverseMap();
            CreateMap<EmployeeApproverDto, EmployeeApproverModel>().ReverseMap();
            CreateMap<EmployeeCheckerDto, EmployeeCheckerModel>().ReverseMap();
            CreateMap<EmployeeDeliveredDto, EmployeeDeliveredModel>().ReverseMap();
            CreateMap<EmployeeImageDto, EmployeeImageModel>().ReverseMap();
            CreateMap<EmployeeSalesRefDto, EmployeeSalesRefModel>().ReverseMap();
            CreateMap<ItemDto, ItemModel>().ReverseMap();
            CreateMap<ItemDetailDto, ItemDetailModel>().ReverseMap();
            CreateMap<ItemImageDto, ItemImageModel>().ReverseMap();
            CreateMap<ItemSupplierDto, ItemSupplierModel>().ReverseMap();
            CreateMap<ItemUnitMeasureDto, ItemUnitMeasureModel>().ReverseMap();
            CreateMap<LocationDto, LocationModel>().ReverseMap();
            CreateMap<OfficialReceiptDetailDto, OfficialReceiptDetailModel>().ReverseMap();
            CreateMap<OfficialReceiptMasterDto, OfficialReceiptMasterModel>().ReverseMap();
            CreateMap<PurchaseOrderDetailDto, PurchaseOrderDetailModel>().ReverseMap();
            CreateMap<PurchaseOrderMasterDto, PurchaseOrderMasterModel>().ReverseMap();
            CreateMap<PurchaseRequisitionDetailDto, PurchaseRequisitionDetailModel>().ReverseMap();
            CreateMap<PurchaseRequisitionMasterDto, PurchaseRequisitionMasterModel>().ReverseMap();
            CreateMap<PurchaseReturnDetailDto, PurchaseReturnDetailModel>().ReverseMap();
            CreateMap<PurchaseReturnMasterDto, PurchaseReturnMasterModel>().ReverseMap();
            CreateMap<ReceivingReportDetailDto, ReceivingReportDetailModel>().ReverseMap();
            CreateMap<ReceivingReportMasterDto, ReceivingReportMasterModel>().ReverseMap();
            CreateMap<SalesInvoiceDetailDto, SalesInvoiceDetailModel>().ReverseMap();
            CreateMap<SalesInvoiceMasterDto, SalesInvoiceMasterModel>().ReverseMap();
            CreateMap<SalesOrderDetailDto, SalesOrderDetailModel>().ReverseMap();
            CreateMap<SalesOrderMasterDto, SalesOrderMasterModel>().ReverseMap();
            CreateMap<SalesReturnDetailDto, SalesReturnDetailModel>().ReverseMap();
            CreateMap<SalesReturnMasterDto, SalesReturnMasterModel>().ReverseMap();
            CreateMap<SupplierDto, SupplierModel>().ReverseMap();
            CreateMap<SupplierImageDto, SupplierImageModel>().ReverseMap();
            CreateMap<WarehouseDto, WarehouseModel>().ReverseMap();
            CreateMap<ApprovalFlowDto, ApprovalFlowModel>().ReverseMap();
            CreateMap<ApprovalHistoryDto, ApprovalHistoryModel>().ReverseMap();
            CreateMap<AuditTrailDto, AuditTrailModel>().ReverseMap();
            CreateMap<DocumentReferenceDto, DocumentReferenceModel>().ReverseMap();
            CreateMap<DocumentSeriesDto, DocumentSeriesModel>().ReverseMap();
            CreateMap<InventoryAdjustmentDto, InventoryAdjustmentModel>().ReverseMap();
            CreateMap<InventoryAdjustmentDetailDto, InventoryAdjustmentDetailModel>().ReverseMap();
            CreateMap<InventoryBalanceDto, InventoryBalanceModel>().ReverseMap();
            CreateMap<InventoryTransactionDto, InventoryTransactionModel>().ReverseMap();
            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<RolePermissionDto, RolePermissionModel>().ReverseMap();
            CreateMap<StockCountDetailDto, StockCountDetailModel>().ReverseMap();
            CreateMap<StockCountMasterDto, StockCountMasterModel>().ReverseMap();
            CreateMap<StockTransferDto, StockTransferModel>().ReverseMap();
            CreateMap<StockTransferDetailDto, StockTransferDetailModel>().ReverseMap();
            CreateMap<SystemLogDto, SystemLogModel>().ReverseMap();
            CreateMap<SystemSettingDto, SystemSettingModel>().ReverseMap();
            CreateMap<UserAccountDto, UserAccountModel>().ReverseMap();
            CreateMap<BrandDto, BrandModel>().ReverseMap();
            CreateMap<CategoryDto, CategoryModel>().ReverseMap();
            CreateMap<ExpenseCategoryDto, ExpenseCategoryModel>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategoryModel>().ReverseMap();
            CreateMap<ClassificationDto, ClassificationModel>().ReverseMap();
            CreateMap<ExpenseDto, ExpenseModel>().ReverseMap();

            CreateMap<AccountDto, AccountModel>().ReverseMap();
            CreateMap<CompanyDto, CompanyModel>().ReverseMap();
            CreateMap<BranchDto, BranchModel>().ReverseMap();
            CreateMap<CostingHistoryDto, CostingHistoryModel>().ReverseMap();
            CreateMap<ItemBarcodeDto, ItemBarcodeModel>().ReverseMap();
            CreateMap<ItemPriceHistoryDto, ItemPriceHistoryModel>().ReverseMap();
            CreateMap<ItemWarehouseMappingDto, ItemWarehouseMappingModel>().ReverseMap();
            CreateMap<HolidayDto, HolidayModel>().ReverseMap();
        }
    }
}
