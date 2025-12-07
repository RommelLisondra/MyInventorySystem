using AutoMapper;

using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Repository;
using Sample.ApplicationService.Services;
using Sample.ApplicationService.ServiceContract;
using Sample.Domain.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace Sample.WebAPI
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //===== Application Services =====
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeImageService, EmployeeImageService>();
            services.AddScoped<IEmployeeApproverService, EmployeeApproverService>();
            services.AddScoped<IEmployeeCheckerService, EmployeeCheckerService>();
            services.AddScoped<IEmployeeDeliveredService, EmployeeDeliveredService>();
            services.AddScoped<IEmployeeSalesRefService, EmployeeSalesRefService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDeliveryReceiptService, DeliveryReceiptService>();
            services.AddScoped<IItemDetailService, ItemDetailService>();
            services.AddScoped<IItemImageService, ItemImageService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemSupplierService, ItemSupplierService>();
            services.AddScoped<IItemUnitMeasureService, ItemUnitMeasureService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IOfficialReceiptService, OfficialReceiptService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPurchaseRequisitionService, PurchaseRequisitionService>();
            services.AddScoped<IPurchaseReturnService, PurchaseReturnService>();
            services.AddScoped<IReceivingReportService, ReceivingReportService>();
            services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<ISalesReturnService, SalesReturnService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IApprovalFlowService, ApprovalFlowService>();
            services.AddScoped<IApprovalHistoryService, ApprovalHistoryService>();
            services.AddScoped<IAuditTrailService, AuditTrailService>();
            services.AddScoped<IDocumentReferenceService, DocumentReferenceService>();
            services.AddScoped<IDocumentSeriesService, DocumentSeriesService>();
            services.AddScoped<IInventoryAdjustmentService, InventoryAdjustmentService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStockTransferService, StockTransferService>();
            services.AddScoped<IStockCountService, StockCountService>();
            services.AddScoped<ISystemLogsService, SystemLogsService>();
            services.AddScoped<ISystemSettingsService, SystemSettingService>();
            services.AddScoped<IUserAcountService, UserAccountService>();

            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();

            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IItemPriceHistoryService, ItemPriceHistoryService>();
            services.AddScoped<IItemBarcodeService, ItemBarcodeService>();
            services.AddScoped<ICostingHistoryService, CostingHistoryService>();
            services.AddScoped<IItemWarehouseMappingService, ItemWarehouseMappingService>();

            //// ===== Repository Layer ====

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeImageRepository, EmployeeImageRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerImageRepository, CustomerImageRepository>();
            services.AddScoped<IDeliveryReceiptRepository, DeliveryReceiptRepository>();
            services.AddScoped<IEmployeeApproverRepository, EmployeeApproverRepository>();
            services.AddScoped<IEmployeeCheckerRepository, EmployeeCheckerRepository>();
            services.AddScoped<IEmployeeDeliveredRepository, EmployeeDeliveredRepository>();
            services.AddScoped<IEmployeeImageRepository, EmployeeImageRepository>();
            services.AddScoped<IEmployeeSalesRefRepository, EmployeeSalesRefRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemDetailRepository, ItemDetailRepository>();
            services.AddScoped<IItemImageRepository, ItemImageRepository>();
            services.AddScoped<IItemSupplierRepository, ItemSupplierRepository>();
            services.AddScoped<IItemUnitMeasureRepository, ItemUnitMeasureRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IOfficialReceiptRepository, OfficialReceiptRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseRequisitionRepository, PurchaseRequisitionRepository>();
            services.AddScoped<IPurchaseReturnRepository, PurchaseReturnRepository>();
            services.AddScoped<IReceivingReportRepository, ReceivingReportRepository>();
            services.AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<ISalesReturnRepository, SalesReturnRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierImageRepository, SupplierImageRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IApprovalFlowRepository, ApprovalFlowRepository>();
            services.AddScoped<IApprovalHistoryRepository, ApprovalHistoryRepository>();
            services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
            services.AddScoped<IDocumentReferenceRepository, DocumentReferenceRepository>();
            services.AddScoped<IDocumentSeriesRepository, DocumentSeriesRepository>();
            services.AddScoped<IInventoryAdjustmentRepository, InventoryAdjustmentRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStockTransferRepository, StockTransferRepository>();
            services.AddScoped<IStockCountRepository, StockCountRepository>();
            services.AddScoped<ISystemLogsRepository, SystemLogsRepository>();
            services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<IUserAcountRepository, UserAcountRepository>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IItemPriceHistoryRepository, ItemPriceHistoryRepository>();
            services.AddScoped<IItemBarcodeRepository, ItemBarcodeRepository>();
            services.AddScoped<ICostingHistoryRepository, CostingHistoryRepository>();
            services.AddScoped<IItemWarehouseMappingRepository, ItemWarehouseMappingRepository>();
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<WebMappingProfile>();
                cfg.AddProfile<ServiceMappingProfile>();
                cfg.AddProfile<DomainMappingProfile>();
            });
        }
    }
}
