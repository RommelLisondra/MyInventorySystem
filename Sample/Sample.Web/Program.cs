using Microsoft.Extensions.DependencyInjection;
using Sample.ApplicationService;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.Services;
using Sample.Infrastructure;
using Sample.Infrastructure.Repository;
using Sample.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperProfiles();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IDeliveryReceiptService, DeliveryReceiptService>();
builder.Services.AddScoped<IItemDetailService, ItemDetailService>();
builder.Services.AddScoped<IItemImageService, ItemImageService>();
builder.Services.AddScoped<IItemSupplierService, ItemSupplierService>();
builder.Services.AddScoped<IItemUnitMeasureService, ItemUnitMeasureService>();
builder.Services.AddScoped<IOfficialReceiptService, OfficialReceiptService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IPurchaseRequisitionService, PurchaseRequisitionService>();
builder.Services.AddScoped<IPurchaseReturnService, PurchaseReturnService>();
builder.Services.AddScoped<IReceivingReportService, ReceivingReportService>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
builder.Services.AddScoped<ISalesReturnService, SalesReturnService>();

//Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
app.UseCors("AllowAll");

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
