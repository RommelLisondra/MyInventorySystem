using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sample.Domain.Entities;
using Sample.Infrastructure.Repository;
using Sample.Infrastructure.EntityFramework;
using Sample.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerImageRepository CustomerImageRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IDeliveryReceiptRepository DeliveryReceiptRepository { get; }
        IEmployeeApproverRepository EmployeeApproverRepository { get; }
        IEmployeeCheckerRepository EmployeeCheckerRepository { get; }
        IEmployeeDeliveredRepository EmployeeDeliveredRepository { get; } 
        IEmployeeImageRepository EmployeeImageRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeSalesRefRepository EmployeeSalesRefRepository { get; }
        IItemDetailRepository ItemDetailRepository { get; }
        IItemImageRepository ItemImageRepository { get; }
        IItemRepository ItemRepository { get; }
        IItemSupplierRepository ItemSupplierRepository { get; }
        IItemUnitMeasureRepository ItemUnitMeasureRepository { get; }
        ILocationRepository LocationRepository { get; }
        IOfficialReceiptRepository OfficialReceiptRepository { get; }
        IPurchaseOrderRepository PurchaseOrderRepository { get; }
        IPurchaseRequisitionRepository PurchaseRequisitionRepository { get; }
        IPurchaseReturnRepository PurchaseReturnRepository { get; }
        IReceivingReportRepository ReceivingReportRepository { get; }
        ISalesInvoiceRepository SalesInvoiceRepository { get; }
        ISalesOrderRepository SalesOrderRepository { get; }
        ISalesReturnRepository SalesReturnRepository { get; }
        ISupplierImageRepository SupplierImageRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IWarehouseRepository WarehouseRepository { get; }
        IApprovalFlowRepository ApprovalFlowRepository { get; }
        IApprovalHistoryRepository ApprovalHistoryRepository { get; }
        IAuditTrailRepository AuditTrailRepository { get; }
        IDocumentReferenceRepository DocumentReferenceRepository { get; }
        IDocumentSeriesRepository DocumentSeriesRepository { get; }
        IInventoryAdjustmentRepository InventoryAdjustmentRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        IRoleRepository RoleRepository { get; }
        IStockTransferRepository StockTransferRepository { get; }
        ISystemLogsRepository SystemLogsRepository { get; }
        ISystemSettingsRepository SystemSettingsRepository { get; }
        IUserAcountRepository UserAcountRepository { get; }

        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IClassificationRepository ClassificationRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IItemInventoryRepository ItemInventoryRepository { get; }

        IAccountRepository AccountRepository { get; }
        IBranchRepository BranchRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICostingHistoryRepository CostingHistoryRepository { get; }
        IHolidayRepository HolidayRepository { get; }
        IItemBarcodeRepository ItemBarcodeRepository { get; }
        IItemWarehouseMappingRepository ItemWarehouseMappingRepository { get; }
        IItemPriceHistoryRepository ItemPriceHistoryRepository { get; }
        IInventoryBalanceRepository InventoryBalanceRepository { get; }
        IStockCountRepository StockCountRepository { get; }
        IInventoryTransactionRepository InventoryTransactionRepository { get; }

        INVENTORYDbContext DbContext { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
        Task<int> SaveWithIdAsync<T>(T value, string fieldName) where T : class;
    }

    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerImageRepository? _customerImageRepository;
        private ICustomerRepository? _customerRepository;
        private IDeliveryReceiptRepository? _deliveryReceiptRepository;
        private IEmployeeApproverRepository? _employeeApproverRepository;
        private IEmployeeCheckerRepository? _employeeCheckerRepository;
        private IEmployeeDeliveredRepository? _employeeDeliveredRepository;
        private IEmployeeImageRepository? _employeeImageRepository;
        private IEmployeeRepository? _employeeRepository;
        private IEmployeeSalesRefRepository? _employeeSalesRefRepository;
        private IItemDetailRepository? _itemDetailRepository;
        private IItemImageRepository? _itemImageRepository;
        private IItemRepository? _itemRepository;
        private IItemSupplierRepository? _itemSupplierRepository;
        private IItemUnitMeasureRepository? _itemUnitMeasureRepository;
        private ILocationRepository? _locationRepository;
        private IOfficialReceiptRepository? _officialReceiptRepository;
        private IPurchaseOrderRepository? _purchaseOrderRepository;
        private IPurchaseRequisitionRepository? _purchaseRequisitionRepository;
        private IPurchaseReturnRepository? _purchaseReturnRepository;
        private IReceivingReportRepository? _receivingReportRepository;
        private ISalesInvoiceRepository? _salesInvoiceRepository;
        private ISalesOrderRepository? _salesOrderRepository;
        private ISalesReturnRepository? _salesReturnRepository;
        private ISupplierImageRepository? _supplierImageRepository;
        private ISupplierRepository? _supplierRepository;
        private IWarehouseRepository? _warehouseRepository;
        private IApprovalFlowRepository? _approvalFlowRepository;
        private IApprovalHistoryRepository? _approvalHistoryRepository;
        private IAuditTrailRepository? _auditTrailRepository;
        private IDocumentReferenceRepository? _documentReferenceRepository;
        private IDocumentSeriesRepository? _documentSeriesRepository;
        private IInventoryAdjustmentRepository? _inventoryAdjustmentRepository;
        private IRolePermissionRepository? _rolePermissionRepository;
        private IRoleRepository? _roleRepository;
        private IStockTransferRepository? _stockTransferRepository;
        private ISystemLogsRepository? _systemLogsRepository;
        private ISystemSettingsRepository? _systemSettingsRepository;
        private IUserAcountRepository? _userAcountRepository;

        private IBrandRepository? _brandRepository;
        private ICategoryRepository? _categoryRepository;
        private IClassificationRepository? _classificationRepository;
        private IExpenseRepository? _expenseRepository;
        private IExpenseCategoryRepository? _expenseCategoryRepository;
        private ISubCategoryRepository? _subCategoryRepository;
        private IItemInventoryRepository? _itemInventoryRepository;

        private IAccountRepository? _accountRepository;
        private IBranchRepository? _branchRepository;
        private ICompanyRepository? _companyRepository;
        private ICostingHistoryRepository? _costingHistoryRepository;
        private IHolidayRepository? _holidayRepository;
        private IItemBarcodeRepository? _itemBarcodeRepository;
        private IItemWarehouseMappingRepository? _itemWarehouseMappingRepository;
        private IItemPriceHistoryRepository? _itemPriceHistoryRepository;
        private IInventoryBalanceRepository? _inventoryBalanceRepository;
        private IStockCountRepository? _stockCountRepository;
        private IInventoryTransactionRepository? _inventoryTransactionRepository;

        private IDbContextTransaction? _currentTransaction;

        private readonly INVENTORYDbContext _context;

        public UnitOfWork(INVENTORYDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public INVENTORYDbContext DbContext => _context;

        public ICustomerImageRepository CustomerImageRepository => _customerImageRepository ??= new CustomerImageRepository(DbContext);
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(DbContext);
        public IDeliveryReceiptRepository DeliveryReceiptRepository => _deliveryReceiptRepository ??= new DeliveryReceiptRepository(DbContext);
        public IEmployeeApproverRepository EmployeeApproverRepository => _employeeApproverRepository ??= new EmployeeApproverRepository(DbContext);
        public IEmployeeCheckerRepository EmployeeCheckerRepository => _employeeCheckerRepository ??= new EmployeeCheckerRepository(DbContext);
        public IEmployeeDeliveredRepository EmployeeDeliveredRepository => _employeeDeliveredRepository ??= new EmployeeDeliveredRepository(DbContext);
        public IEmployeeImageRepository EmployeeImageRepository => _employeeImageRepository ??= new EmployeeImageRepository(DbContext);
        public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(DbContext);
        public IEmployeeSalesRefRepository EmployeeSalesRefRepository => _employeeSalesRefRepository ??= new EmployeeSalesRefRepository(DbContext);
        public IItemDetailRepository ItemDetailRepository => _itemDetailRepository ??= new ItemDetailRepository(DbContext);
        public IItemImageRepository ItemImageRepository => _itemImageRepository ??= new ItemImageRepository(DbContext);
        public IItemRepository ItemRepository => _itemRepository ??= new ItemRepository(DbContext);
        public IItemSupplierRepository ItemSupplierRepository => _itemSupplierRepository ??= new ItemSupplierRepository(DbContext);
        public IItemUnitMeasureRepository ItemUnitMeasureRepository => _itemUnitMeasureRepository ??= new ItemUnitMeasureRepository(DbContext);
        public ILocationRepository LocationRepository => _locationRepository ??= new LocationRepository(DbContext);
        public IOfficialReceiptRepository OfficialReceiptRepository => _officialReceiptRepository ??= new OfficialReceiptRepository(DbContext);
        public IPurchaseOrderRepository PurchaseOrderRepository => _purchaseOrderRepository ??= new PurchaseOrderRepository(DbContext);
        public IPurchaseRequisitionRepository PurchaseRequisitionRepository => _purchaseRequisitionRepository ??= new PurchaseRequisitionRepository(DbContext);
        public IPurchaseReturnRepository PurchaseReturnRepository => _purchaseReturnRepository ??= new PurchaseReturnRepository(DbContext);
        public IReceivingReportRepository ReceivingReportRepository => _receivingReportRepository ??= new ReceivingReportRepository(DbContext);
        public ISalesInvoiceRepository SalesInvoiceRepository => _salesInvoiceRepository ??= new SalesInvoiceRepository(DbContext);
        public ISalesOrderRepository SalesOrderRepository => _salesOrderRepository ??= new SalesOrderRepository(DbContext);
        public ISalesReturnRepository SalesReturnRepository => _salesReturnRepository ??= new SalesReturnRepository(DbContext);
        public ISupplierImageRepository SupplierImageRepository => _supplierImageRepository ??= new SupplierImageRepository(DbContext);
        public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(DbContext);
        public IWarehouseRepository WarehouseRepository => _warehouseRepository ??= new WarehouseRepository(DbContext);
        public IApprovalFlowRepository ApprovalFlowRepository => _approvalFlowRepository ??= new ApprovalFlowRepository(DbContext);
        public IApprovalHistoryRepository ApprovalHistoryRepository => _approvalHistoryRepository ??= new ApprovalHistoryRepository(DbContext);
        public IAuditTrailRepository AuditTrailRepository => _auditTrailRepository ??= new AuditTrailRepository(DbContext);
        public IDocumentReferenceRepository DocumentReferenceRepository => _documentReferenceRepository ??= new DocumentReferenceRepository(DbContext);
        public IDocumentSeriesRepository DocumentSeriesRepository => _documentSeriesRepository ??= new DocumentSeriesRepository(DbContext);
        public IInventoryAdjustmentRepository InventoryAdjustmentRepository => _inventoryAdjustmentRepository ??= new InventoryAdjustmentRepository(DbContext);
        public IRolePermissionRepository RolePermissionRepository => _rolePermissionRepository ??= new RolePermissionRepository(DbContext);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(DbContext);
        public IStockTransferRepository StockTransferRepository => _stockTransferRepository ??= new StockTransferRepository(DbContext);
        public ISystemLogsRepository SystemLogsRepository => _systemLogsRepository ??= new SystemLogsRepository(DbContext);
        public ISystemSettingsRepository SystemSettingsRepository => _systemSettingsRepository ??= new SystemSettingsRepository(DbContext);
        public IUserAcountRepository UserAcountRepository => _userAcountRepository ??= new UserAcountRepository(DbContext);

        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(DbContext);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(DbContext);
        public IClassificationRepository ClassificationRepository => _classificationRepository ??= new ClassificationRepository(DbContext);
        public IExpenseCategoryRepository ExpenseCategoryRepository => _expenseCategoryRepository ??= new ExpenseCategoryRepository(DbContext);
        public IExpenseRepository ExpenseRepository => _expenseRepository ??= new ExpenseRepository(DbContext);
        public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository ??= new SubCategoryRepository(DbContext);
        public IItemInventoryRepository ItemInventoryRepository => _itemInventoryRepository ??= new ItemInventoryRepository(DbContext);

        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(DbContext);
        public IBranchRepository BranchRepository => _branchRepository ??= new BranchRepository(DbContext);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(DbContext);
        public ICostingHistoryRepository CostingHistoryRepository => _costingHistoryRepository ??= new CostingHistoryRepository(DbContext);
        public IHolidayRepository HolidayRepository => _holidayRepository ??= new HolidayRepository(DbContext);
        public IItemBarcodeRepository ItemBarcodeRepository => _itemBarcodeRepository ??= new ItemBarcodeRepository(DbContext);
        public IItemWarehouseMappingRepository ItemWarehouseMappingRepository => _itemWarehouseMappingRepository ??= new ItemWarehouseMappingRepository(DbContext);
        public IItemPriceHistoryRepository ItemPriceHistoryRepository => _itemPriceHistoryRepository ??= new ItemPriceHistoryRepository(DbContext);

        public IInventoryBalanceRepository InventoryBalanceRepository => _inventoryBalanceRepository ??= new InventoryBalanceRepository(DbContext);
        public IStockCountRepository StockCountRepository => _stockCountRepository ??= new StockCountRepository(DbContext);
        public IInventoryTransactionRepository InventoryTransactionRepository => _inventoryTransactionRepository ??= new InventoryTransactionRepository(DbContext);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message;
                throw new InvalidOperationException(inner ?? dbEx.Message, dbEx);
            }
        }

        public async Task<int> SaveWithIdAsync<T>(T value, string fieldName) where T : class
        {
            try
            {
                await _context.Set<T>().AddAsync(value);
                await _context.SaveChangesAsync();

                PropertyInfo? propertyInfo = value?.GetType().GetProperty(fieldName);
                if (propertyInfo == null)
                    throw new InvalidOperationException($"Property '{fieldName}' not found on type '{typeof(T).Name}'.");

                object? rawValue = propertyInfo.GetValue(value, null);
                if (rawValue == null)
                    throw new InvalidOperationException($"Property '{fieldName}' value is null on type '{typeof(T).Name}'.");

                return Convert.ToInt32(rawValue);
            }
            catch (DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message;
                throw new InvalidOperationException(inner ?? dbEx.Message, dbEx);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
