using AutoMapper;

using Sample.Infrastructure.EntityFramework;
using Sample.Infrastructure.Repository;
using Sample.ApplicationService.Services;
using Sample.ApplicationService.ServiceContract;
using Sample.Domain.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace Sample.Web
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //===== Application Services =====
            services.AddScoped<IEmployeeService, EmployeeService>();
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

            //// ===== Repository Layer ====

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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
