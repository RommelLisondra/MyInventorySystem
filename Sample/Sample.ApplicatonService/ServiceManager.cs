using AutoMapper;

using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.Services;
using Sample.Domain.Contracts;
using Sample.Infrastructure;
using Sample.Infrastructure.EntityFramework;

namespace Sample.ApplicationService
{
    public interface IServiceManager : IDisposable
    {
        IEmployeeService EmployeeService { get; }
        ICustomerService CustomerService { get; }
        ISalesInvoiceService SalesInvoiceService { get; }
        ISalesOrderService SalesOrderService { get; }
        IItemDetailService ItemDetailService { get; }
        IEmployeeApproverService EmployeeApproverService { get; }
        IEmployeeCheckerService EmployeeCheckerService { get; }
        IEmployeeDeliveredService EmployeeDeliveredService { get; }
        IEmployeeImageService EmployeeImageService { get; }
        IEmployeeSalesRefService EmployeeSalesRefService { get; }
        IItemService ItemService { get; }
        IItemImageService ItemImageService { get; }
        IItemSupplierService ItemSupplierService { get; }
        IItemUnitMeasureService ItemUnitMeasureService { get; }
        IOfficialReceiptService OfficialReceiptService { get; }
        IPurchaseOrderService PurchaseOrderService { get; }
        IPurchaseRequisitionService PurchaseRequisitionService { get; }
        IPurchaseReturnService PurchaseReturnService { get; }
        IReceivingReportService ReceivingReportService { get; }
        ISalesReturnService SalesReturnService { get; }
        ISupplierService SupplierService { get; }
        ISupplierImageService SupplierImageService { get; }
        IWarehouseService WarehouseService { get; }
        IDeliveryReceiptService DeliveryReceiptService { get; }
        ILocationService LocationService { get; }
        IApprovalFlowService ApprovalFlowService  { get; }
        IApprovalHistoryService ApprovalHistoryService  { get; }
        IAuditTrailService AuditTrailService  { get; }
        IDocumentReferenceService DocumentReferenceService  { get; }
        IDocumentSeriesService DocumentSeriesService  { get; }
        IInventoryAdjustmentService InventoryAdjustmentService  { get; }
        IInventoryBalanceService InventoryBalanceService { get; }
        IInventoryTransactionService InventoryTransactionService { get; }
        IRolePermissionService  RolePermissionService  { get; }
        IRoleService  RoleService  { get; }
        IStockTransferService  StockTransferService  { get; }
        IStockCountService StockCountService { get; }
        ISystemLogsService SystemLogsService  { get; }
        ISystemSettingsService SystemSettingsService  { get; }
        IUserAcountService UserAcountService  { get; }

        IBrandService BrandService { get; }
        ICategoryService CategoryService { get; }
        IClassificationService ClassificationService { get; }
        IExpenseService ExpenseService { get; }
        IExpenseCategoryService ExpenseCategoryService { get; }
        ISubCategoryService SubCategoryService { get; }
        IItemInventoryService ItemInventoryService { get; }

        IAccountService AccountService { get; }
        IBranchService BranchService { get; }
        ICompanyService CompanyService { get; }
        ICostingHistoryService CostingHistoryService { get; }
        IHolidayService HolidayService { get; }
        IItemBarcodeService ItemBarcodeService { get; }
        IItemWarehouseMappingService ItemWarehouseMappingService { get; }
        IItemPriceHistoryService ItemPriceHistoryService { get; }
    }
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private IEmployeeService? _employeeService;
        private ICustomerService? _customerService;
        private ISalesInvoiceService? _salesInvoiceService;
        private ISalesOrderService? _salesOrderService;
        private IItemDetailService? _itemDetailService;
        private IEmployeeApproverService? _employeeApproverService;
        private IEmployeeCheckerService? _employeeCheckerService;
        private IEmployeeDeliveredService? _employeeDeliveredService;
        private IEmployeeImageService? _employeeImageService;
        private IEmployeeSalesRefService? _employeeSalesRefService;
        private IItemService? _itemService;
        private IItemImageService? _itemImageService;
        private IItemSupplierService? _itemSupplierService;
        private IItemUnitMeasureService? _itemUnitMeasureService;
        private IOfficialReceiptService? _officialReceiptService;
        private IDeliveryReceiptService? _deliveryReceiptService;
        private IPurchaseOrderService? _purchaseOrderService;
        private IPurchaseRequisitionService? _purchaseRequisitionService;
        private IPurchaseReturnService? _purchaseReturnService;
        private IReceivingReportService? _receivingReportService;
        private ISalesReturnService? _salesReturnService;
        private ISupplierService? _supplierService;
        private ISupplierImageService? _supplierImageService;
        private IWarehouseService? _warehouseService;
        private ILocationService? _locationService;
        private IApprovalFlowService ? _approvalFlowService ;
        private IApprovalHistoryService ? _approvalHistoryService ;
        private IAuditTrailService ? _auditTrailService ;
        private IDocumentReferenceService ? _documentReferenceService ;
        private IDocumentSeriesService ? _documentSeriesService ;
        private IInventoryAdjustmentService ? _inventoryAdjustmentService ;
        private IRolePermissionService ? _rolePermissionService ;
        private IRoleService ? _roleService ;
        private IStockTransferService ? _stockTransferService ;
        private ISystemLogsService ? _systemLogsService ;
        private ISystemSettingsService ? _systemSettingsService ;
        private IUserAcountService ? _userAcountService ;

        private IBrandService? _brandService;
        private ICategoryService? _categoryService;
        private IClassificationService? _classificationService;
        private IExpenseService? _expenseService;
        private IExpenseCategoryService? _expenseCategoryService;
        private ISubCategoryService? _subCategoryService;
        private IItemInventoryService? _itemInventoryService;

        private IAccountService? _accountService;
        private IBranchService? _branchService;
        private ICompanyService? _companyService;
        private ICostingHistoryService? _costingHistoryService;
        private IHolidayService? _holidayService;
        private IItemBarcodeService? _itemBarcodeService;
        private IItemWarehouseMappingService? _itemWarehouseMappingService;
        private IItemPriceHistoryService? _itemPriceHistoryService;
        private IStockCountService? _stockCountService;
        private IInventoryBalanceService? _inventoryBalanceService;
        private IInventoryTransactionService? _inventoryTransactionService;

        private bool disposedValue;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEmployeeService EmployeeService => _employeeService ??= new EmployeeService(_unitOfWork, _mapper);
        public ICustomerService CustomerService => _customerService ??= new CustomerService(_unitOfWork, _mapper);
        public ISalesInvoiceService SalesInvoiceService => _salesInvoiceService ??= new SalesInvoiceService(_unitOfWork, _mapper);
        public ISalesOrderService SalesOrderService => _salesOrderService ??= new SalesOrderService(_unitOfWork, _mapper);
        public IItemDetailService ItemDetailService => _itemDetailService ??= new ItemDetailService(_unitOfWork, _mapper);
        public IEmployeeApproverService EmployeeApproverService => _employeeApproverService ??= new EmployeeApproverService(_unitOfWork, _mapper);
        public IEmployeeCheckerService EmployeeCheckerService => _employeeCheckerService ??= new EmployeeCheckerService(_unitOfWork, _mapper);
        public IEmployeeDeliveredService EmployeeDeliveredService => _employeeDeliveredService ??= new EmployeeDeliveredService(_unitOfWork, _mapper);
        public IEmployeeImageService EmployeeImageService => _employeeImageService ??= new EmployeeImageService(_unitOfWork, _mapper);
        public IEmployeeSalesRefService EmployeeSalesRefService => _employeeSalesRefService ??= new EmployeeSalesRefService(_unitOfWork, _mapper);
        public IItemService ItemService => _itemService ??= new ItemService(_unitOfWork, _mapper);
        public IItemImageService ItemImageService => _itemImageService ??= new ItemImageService(_unitOfWork, _mapper);
        public IItemSupplierService ItemSupplierService => _itemSupplierService ??= new ItemSupplierService(_unitOfWork, _mapper);
        public IItemUnitMeasureService ItemUnitMeasureService => _itemUnitMeasureService ??= new ItemUnitMeasureService(_unitOfWork, _mapper);
        public IOfficialReceiptService OfficialReceiptService => _officialReceiptService ??= new OfficialReceiptService(_unitOfWork, _mapper);
        public IPurchaseOrderService PurchaseOrderService => _purchaseOrderService ??= new PurchaseOrderService(_unitOfWork, _mapper);
        public IPurchaseRequisitionService PurchaseRequisitionService => _purchaseRequisitionService ??= new PurchaseRequisitionService(_unitOfWork, _mapper);
        public IPurchaseReturnService PurchaseReturnService => _purchaseReturnService ??= new PurchaseReturnService(_unitOfWork, _mapper);
        public IReceivingReportService ReceivingReportService => _receivingReportService ??= new ReceivingReportService(_unitOfWork, _mapper);
        public ISalesReturnService SalesReturnService => _salesReturnService ??= new SalesReturnService(_unitOfWork, _mapper);
        public ISupplierService SupplierService => _supplierService ??= new SupplierService(_unitOfWork, _mapper);
        public ISupplierImageService SupplierImageService => _supplierImageService ??= new SupplierImageService(_unitOfWork, _mapper);
        public IWarehouseService WarehouseService => _warehouseService ??= new WarehouseService(_unitOfWork, _mapper);
        public IDeliveryReceiptService DeliveryReceiptService => _deliveryReceiptService ??= new DeliveryReceiptService(_unitOfWork, _mapper);
        public ILocationService LocationService => _locationService ??= new LocationService(_unitOfWork, _mapper);
        public IApprovalFlowService ApprovalFlowService => _approvalFlowService ??= new ApprovalFlowService(_unitOfWork, _mapper);
        public IApprovalHistoryService ApprovalHistoryService => _approvalHistoryService ??= new ApprovalHistoryService(_unitOfWork, _mapper);
        public IAuditTrailService AuditTrailService => _auditTrailService ??= new AuditTrailService(_unitOfWork, _mapper);
        public IDocumentReferenceService DocumentReferenceService => _documentReferenceService ??= new DocumentReferenceService(_unitOfWork, _mapper);
        public IDocumentSeriesService DocumentSeriesService => _documentSeriesService ??= new DocumentSeriesService(_unitOfWork, _mapper);
        public IInventoryAdjustmentService InventoryAdjustmentService => _inventoryAdjustmentService ??= new InventoryAdjustmentService(_unitOfWork, _mapper);
        public IRolePermissionService RolePermissionService => _rolePermissionService ??= new RolePermissionService(_unitOfWork, _mapper);
        public IRoleService RoleService => _roleService  ??= new RoleService(_unitOfWork, _mapper);
        public IStockTransferService StockTransferService => _stockTransferService ??= new StockTransferService(_unitOfWork, _mapper);
        public ISystemLogsService SystemLogsService => _systemLogsService ??= new SystemLogsService(_unitOfWork, _mapper);
        public ISystemSettingsService SystemSettingsService => _systemSettingsService ??= new SystemSettingService(_unitOfWork, _mapper);
        public IUserAcountService UserAcountService => _userAcountService ??= new UserAccountService(_unitOfWork, _mapper);

        public IBrandService BrandService => _brandService ??= new BrandService(_unitOfWork, _mapper);
        public ICategoryService CategoryService => _categoryService ??= new CategoryService(_unitOfWork, _mapper);
        public IClassificationService ClassificationService => _classificationService ??= new ClassificationService(_unitOfWork, _mapper);
        public IExpenseCategoryService ExpenseCategoryService => _expenseCategoryService ??= new ExpenseCategoryService(_unitOfWork, _mapper);
        public IExpenseService ExpenseService => _expenseService ??= new ExpenseService(_unitOfWork, _mapper);
        public ISubCategoryService SubCategoryService => _subCategoryService ??= new SubCategoryService(_unitOfWork, _mapper);
        public IItemInventoryService ItemInventoryService => _itemInventoryService ??= new ItemInventoryService(_unitOfWork, _mapper);

        public IAccountService AccountService => _accountService ??= new AccountService(_unitOfWork, _mapper);
        public IBranchService BranchService => _branchService ??= new BranchService(_unitOfWork, _mapper);
        public ICompanyService CompanyService => _companyService ??= new CompanyService(_unitOfWork, _mapper);
        public ICostingHistoryService CostingHistoryService => _costingHistoryService ??= new CostingHistoryService(_unitOfWork, _mapper);
        public IHolidayService HolidayService => _holidayService ??= new HolidayService(_unitOfWork, _mapper);
        public IItemBarcodeService ItemBarcodeService => _itemBarcodeService ??= new ItemBarcodeService(_unitOfWork, _mapper);
        public IItemWarehouseMappingService ItemWarehouseMappingService => _itemWarehouseMappingService ??= new ItemWarehouseMappingService(_unitOfWork, _mapper);
        public IItemPriceHistoryService ItemPriceHistoryService => _itemPriceHistoryService ??= new ItemPriceHistoryService(_unitOfWork, _mapper);
        public IStockCountService StockCountService => _stockCountService ??= new StockCountService(_unitOfWork, _mapper);
        public IInventoryBalanceService InventoryBalanceService => _inventoryBalanceService ??= new InventoryBalanceService(_unitOfWork, _mapper);
        public IInventoryTransactionService InventoryTransactionService => _inventoryTransactionService ??= new InventoryTransactionService(_unitOfWork, _mapper);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;

            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ServiceManager()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        // Update the constructor to accept IMapper and assign it to _mapper
    }
}
