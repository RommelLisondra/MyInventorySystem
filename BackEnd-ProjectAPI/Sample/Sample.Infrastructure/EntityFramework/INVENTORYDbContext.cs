using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class INVENTORYDbContext : DbContext
    {
        public INVENTORYDbContext()
        {
        }

        public INVENTORYDbContext(DbContextOptions<INVENTORYDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; } = null!;
        public virtual DbSet<ApprovalFlow> ApprovalFlow { get; set; } = null!;
        public virtual DbSet<ApprovalHistory> ApprovalHistory { get; set; } = null!;
        public virtual DbSet<Approver> Approver { get; set; } = null!;
        public virtual DbSet<Brand> Brand { get; set; } = null!;
        public virtual DbSet<Category> Category { get; set; } = null!;
        public virtual DbSet<Classification> Classification { get; set; } = null!;
        public virtual DbSet<AuditTrail> AuditTrail { get; set; } = null!;
        public virtual DbSet<Holiday> Holiday { get; set; } = null!;
        public virtual DbSet<Branch> Branch { get; set; } = null!;
        public virtual DbSet<Company> Company { get; set; } = null!;
        public virtual DbSet<CostingHistory> CostingHistory { get; set; } = null!;
        public virtual DbSet<Checker> Checker { get; set; } = null!;
        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<CustomerImage> CustomerImage { get; set; } = null!;
        public virtual DbSet<Deliverer> Deliverer { get; set; } = null!;
        public virtual DbSet<DeliveryReceiptDetailFile> DeliveryReceiptDetailFile { get; set; } = null!;
        public virtual DbSet<DeliveryReceiptMasterFile> DeliveryReceiptMasterFile { get; set; } = null!;
        public virtual DbSet<DocumentReference> DocumentReference { get; set; } = null!;
        public virtual DbSet<DocumentSeries> DocumentSeries { get; set; } = null!;
        public virtual DbSet<Employee> Employee { get; set; } = null!;
        public virtual DbSet<EmployeeImage> EmployeeImage { get; set; } = null!;
        public virtual DbSet<InventoryAdjustment> InventoryAdjustment { get; set; } = null!;
        public virtual DbSet<InventoryAdjustmentDetail> InventoryAdjustmentDetail { get; set; } = null!;
        public virtual DbSet<InventoryBalance> InventoryBalance { get; set; } = null!;
        public virtual DbSet<InventoryTransaction> InventoryTransaction { get; set; } = null!;
        public virtual DbSet<Item> Item { get; set; } = null!;
        public virtual DbSet<ItemDetail> ItemDetail { get; set; } = null!;
        public virtual DbSet<ItemImage> ItemImage { get; set; } = null!;
        public virtual DbSet<ItemInventory> ItemInventory { get; set; } = null!;
        public virtual DbSet<ItemSupplier> ItemSupplier { get; set; } = null!;
        public virtual DbSet<ItemUnitMeasure> ItemUnitMeasure { get; set; } = null!;
        public virtual DbSet<ItemWarehouseMapping> ItemWarehouseMapping { get; set; } = null!;
        public virtual DbSet<ItemPriceHistory> ItemPriceHistory { get; set; } = null!;
        public virtual DbSet<ItemBarcode> ItemBarcode { get; set; } = null!;       
        public virtual DbSet<Location> Location { get; set; } = null!;
        public virtual DbSet<Expense> Expense { get; set; } = null!;
        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategory { get; set; } = null!;
        public virtual DbSet<OfficialReceiptDetailFile> OfficialReceiptDetailFile { get; set; } = null!;
        public virtual DbSet<OfficialReceiptMasterFile> OfficialReceiptMasterFile { get; set; } = null!;
        public virtual DbSet<PurchaseOrderDetailFile> PurchaseOrderDetailFile { get; set; } = null!;
        public virtual DbSet<PurchaseOrderMasterFile> PurchaseOrderMasterFile { get; set; } = null!;
        public virtual DbSet<PurchaseRequisitionDetailFile> PurchaseRequisitionDetailFile { get; set; } = null!;
        public virtual DbSet<PurchaseRequisitionMasterFile> PurchaseRequisitionMasterFile { get; set; } = null!;
        public virtual DbSet<PurchaseReturnDetailFile> PurchaseReturnDetailFile { get; set; } = null!;
        public virtual DbSet<PurchaseReturnMasterFile> PurchaseReturnMasterFile { get; set; } = null!;
        public virtual DbSet<ReceivingReportDetailFile> ReceivingReportDetailFile { get; set; } = null!;
        public virtual DbSet<ReceivingReportMasterFile> ReceivingReportMasterFile { get; set; } = null!;
        public virtual DbSet<Role> Role { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermission { get; set; } = null!;
        public virtual DbSet<SalesInvoiceDetailFile> SalesInvoiceDetailFile { get; set; } = null!;
        public virtual DbSet<SalesInvoiceMasterFile> SalesInvoiceMasterFile { get; set; } = null!;
        public virtual DbSet<SalesOrderDetailFile> SalesOrderDetailFile { get; set; } = null!;
        public virtual DbSet<SalesOrderMasterFile> SalesOrderMasterFile { get; set; } = null!;
        public virtual DbSet<SalesRef> SalesRef { get; set; } = null!;
        public virtual DbSet<SalesReturnDetailFile> SalesReturnDetailFile { get; set; } = null!;
        public virtual DbSet<SalesReturnMasterFile> SalesReturnMasterFile { get; set; } = null!;
        public virtual DbSet<StockCountDetail> StockCountDetail { get; set; } = null!;
        public virtual DbSet<StockCountMaster> StockCountMaster { get; set; } = null!;
        public virtual DbSet<StockTransfer> StockTransfer { get; set; } = null!;
        public virtual DbSet<StockTransferDetail> StockTransferDetail { get; set; } = null!;
        public virtual DbSet<Supplier> Supplier { get; set; } = null!;
        public virtual DbSet<SupplierImage> SupplierImage { get; set; } = null!;
        public virtual DbSet<SystemLog> SystemLog { get; set; } = null!;
        public virtual DbSet<SystemSetting> SystemSetting { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccount { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouse { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-F98CQH3A;Database=INVENTORY;User ID=sa;Password=kate@20152018;TrustServerCertificate=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalFlow>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Approval__328477F4AB8F2DEA");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApprovalHistory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Approval__328477D4E53D987F");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.ApprovalHistory)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_ApprovalHistory_Employee");
            });

            modelBuilder.Entity<Approver>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__Approver__F1AF4341B8E79E9C");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpIdnoNavigation)
                    .WithOne(p => p.Approver)
                    .HasForeignKey<Approver>(d => d.EmpIdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Approver_Employee");
            });

            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NewValue).IsUnicode(false);

                entity.Property(e => e.OldValue).IsUnicode(false);

                entity.Property(e => e.PrimaryKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Checker>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__Checker__F1AF43414B09EA43");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpIdnoNavigation)
                    .WithOne(p => p.Checker)
                    .HasForeignKey<Checker>(d => d.EmpIdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checker_Employee");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustNo)
                    .HasName("PK__Customer__049E631A03F11273");

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AcountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.Address3).IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardExpiry)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastDrno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastDRNo");

                entity.Property(e => e.LastOr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastOR");

                entity.Property(e => e.LastSino)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastSINo");

                entity.Property(e => e.LastSono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastSONo");

                entity.Property(e => e.LastSrno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastSRNo");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerImage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath).IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerImage_Customer");
            });

            modelBuilder.Entity<Deliverer>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__Delivere__F1AF4341883C675B");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpIdnoNavigation)
                    .WithOne(p => p.Deliverer)
                    .HasForeignKey<Deliverer>(d => d.EmpIdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deliverer_Employee");
            });

            modelBuilder.Entity<DeliveryReceiptDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Drdno, e.ItemDetailCode });

                entity.Property(e => e.Drdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DRDNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.DrdnoNavigation)
                    .WithMany(p => p.DeliveryReceiptDetailFile)
                    .HasPrincipalKey(p => p.Drmno)
                    .HasForeignKey(d => d.Drdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DRDF_DRMF");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.DeliveryReceiptDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode) 
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DRDF_ItemDetail");
            });

            modelBuilder.Entity<DeliveryReceiptMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Simno, "UQ__Delivery__0CCABF0CF5B0954F")
                    .IsUnique();

                entity.HasIndex(e => e.Drmno, "UQ__Delivery__59BA730CC90D05B3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeliveryCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Drmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DRMNo");

                entity.Property(e => e.FooterText)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrepBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Recuring)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Simno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SIMNo");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Terms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypesOfPay)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ApprByNavigation)
                    .WithMany(p => p.DeliveryReceiptMasterFileApprByNavigation)
                    .HasForeignKey(d => d.ApprBy)
                    .HasConstraintName("FK_DRMF_ApprBy_Employee");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany(p => p.DeliveryReceiptMasterFile)
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DRMF_Customer");

                entity.HasOne(d => d.PrepByNavigation)
                    .WithMany(p => p.DeliveryReceiptMasterFilePrepByNavigation)
                    .HasForeignKey(d => d.PrepBy)
                    .HasConstraintName("FK_DRMF_PrepBy_Employee");
            });

            modelBuilder.Entity<DocumentReference>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Document__00AFA65AF267FF37");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateReferenced).HasColumnType("datetime");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentSeries>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Document__F3A1C161267DBA9B");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NextNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Suffix)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__Employee__F1AF4341291D5E9B");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateHired).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EduAttentment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mstatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MStatus");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Religion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeImage>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__Employee__F1AF43410F4AF797");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpIdnoNavigation)
                    .WithOne(p => p.EmployeeImage)
                    .HasForeignKey<EmployeeImage>(d => d.EmpIdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpImage_Employee");
            });

            modelBuilder.Entity<InventoryAdjustment>(entity =>
            {
                entity.HasKey(e => e.Id)  
                    .HasName("PK__Inventor__E60DB893D347800C");

                entity.HasIndex(e => e.WarehouseId, "IX_InventoryAdjustment_WarehouseId");

                entity.Property(e => e.AdjustmentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AdjustmentNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InventoryAdjustmentDetail>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Inventor__135C316DECB280CA");

                entity.HasIndex(e => e.ItemDetailNo, "IX_InventoryAdjustmentDetail_ItemId");

                entity.Property(e => e.ItemDetailNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MovementType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Adjustment)
                    .WithMany(p => p.InventoryAdjustmentDetail)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_InventoryAdjustmentDetail_Adjustment");
            });

            modelBuilder.Entity<InventoryBalance>(entity =>
            {
                entity.HasKey(e => new { e.ItemDetailNo, e.WarehouseId });

                entity.HasIndex(e => e.WarehouseId, "IX_InventoryBalance_WarehouseId");

                entity.Property(e => e.ItemDetailNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.QuantityOnHand).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Inventor__55433A6B09B21C7C");

                entity.HasIndex(e => e.ItemDetailNo, "IX_InventoryTransaction_ItemId");

                entity.HasIndex(e => new { e.RefModule, e.RefNo }, "IX_InventoryTransaction_RefModule_RefNo");

                entity.HasIndex(e => e.WarehouseId, "IX_InventoryTransaction_WarehouseId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ItemDetailNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MovementType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RefModule)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Company)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseId)
                    .OnDelete(DeleteBehavior.NoAction);
                        });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.ItemName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Desc)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                // FK relationships
                entity.HasOne(e => e.Brand)
                    .WithMany()
                    .HasForeignKey(e => e.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Navigation
                entity.HasMany(e => e.ItemDetails)
                    .WithOne()
                    .HasForeignKey(d => d.ItemId)
                    .HasPrincipalKey(i => i.ItemCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemDetail>(entity =>
            {
                entity.ToTable("ItemDetail");

                entity.HasKey(e => e.Id); // PK

                entity.HasAlternateKey(e => e.ItemDetailCode).HasName("AK_ItemDetail_ItemDetailCode"); // Alternate Key

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.ItemDetailNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.ItemId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.UnitMeasure)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Unitprice)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Eoq).HasColumnName("EOQ");
                entity.Property(e => e.Rop).HasColumnName("ROP");

                entity.Property(e => e.ExpiryDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ModifiedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasAlternateKey(e => e.ItemDetailCode);

                entity.HasOne(d => d.ItemMaster)
                    .WithMany(m => m.ItemDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<ItemImage>(entity =>
            {
                entity.HasKey(e => e.ItemDetailCode)
                    .HasName("PK__ItemImag__67E9794A501DB4FC");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithOne(p => p.ItemImage)
                    .HasPrincipalKey<ItemDetail>(p => p.ItemDetailCode)
                    .HasForeignKey<ItemImage>(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemImage_Item");
            });

            modelBuilder.Entity<ItemInventory>(entity =>
            {
                entity.HasKey(e => new { e.ItemDetailCode, e.WarehouseCode });

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.ItemInventory)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_II_Item");

                entity.HasOne(d => d.WarehouseCodeNavigation)
                    .WithMany(p => p.ItemInventory)
                    .HasForeignKey(d => d.WarehouseCode)
                    .HasPrincipalKey(p => p.WarehouseCode) // <-- important
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_II_Warehouse");
            });

            modelBuilder.Entity<ItemSupplier>(entity =>
            {
                entity.HasKey(e => new { e.ItemDetailCode, e.SupplierNo });

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LeadTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Terms)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.ItemSupplier)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemSupplier_Item");

                entity.HasOne(d => d.SupplierNoNavigation)
                    .WithMany(p => p.ItemSupplier)
                    .HasForeignKey(d => d.SupplierNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemSupplier_Supplier");
            });

            modelBuilder.Entity<ItemUnitMeasure>(entity =>
            {
                entity.HasKey(e => new { e.ItemDetailCode, e.UnitCode });

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ConversionRate).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.ItemUnitMeasure)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IUM_Item");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationCode)
                    .HasName("PK__Location__DDB144D4630D953F");

                entity.Property(e => e.LocationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.WarehouseCodeNavigation)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.WarehouseCode)
                    .HasPrincipalKey(p => p.WarehouseCode) // <-- important
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Warehouse");
            });

            modelBuilder.Entity<OfficialReceiptDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Orno, e.Simno })
                    .HasName("PK_ORD_File");

                entity.Property(e => e.Orno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ORNo");

                entity.Property(e => e.Simno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SIMNo");

                entity.Property(e => e.AmountDue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrnoNavigation)
                    .WithMany(p => p.OfficialReceiptDetailFile)
                    .HasPrincipalKey(p => p.Orno)
                    .HasForeignKey(d => d.Orno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORD_File_Master");

                entity.HasOne(d => d.SimnoNavigation)
                    .WithMany(p => p.OfficialReceiptDetailFile)
                    .HasPrincipalKey(p => p.Simno)
                    .HasForeignKey(d => d.Simno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORD_File_SalesInvoice");
            });

            modelBuilder.Entity<OfficialReceiptMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Orno, "UQ__Official__A9A8848C9A1441F0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CashAmt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckAmt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CheckNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DisPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FooterText)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FormPay)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Orno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ORNo");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Recuring)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TotAmtPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.OfficialReceiptMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_ORMF_Approver");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany(p => p.OfficialReceiptMasterFile)
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORMF_Customer");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.OfficialReceiptMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_ORMF_Preparer");
            });

            modelBuilder.Entity<PurchaseOrderDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Pono, e.ItemDetailCode })
                    .HasName("PK_PODF");

                entity.Property(e => e.Pono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PONo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.PurchaseOrderDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PODF_Item");

                entity.HasOne(d => d.PonoNavigation)
                    .WithMany(p => p.PurchaseOrderDetailFile)
                    .HasPrincipalKey(p => p.Pono)
                    .HasForeignKey(d => d.Pono)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PODF_Master");
            });

            modelBuilder.Entity<PurchaseOrderMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Pono, "UQ__Purchase__5F02AA86C53F42D1")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateNeeded).HasColumnType("datetime");

                entity.Property(e => e.DisPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FooterText)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PONo");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PreturnRecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PReturnRecStatus");

                entity.Property(e => e.Prno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRNo");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Recuring)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RrrecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("RRRecStatus");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Terms)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypesofPay)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.PurchaseOrderMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_POMF_Approver");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.PurchaseOrderMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_POMF_Preparer");

                entity.HasOne(d => d.SupplierNoNavigation)
                    .WithMany(p => p.PurchaseOrderMasterFile)
                    .HasForeignKey(d => d.SupplierNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POMF_Supplier");
            });

            modelBuilder.Entity<PurchaseRequisitionDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Prno, e.ItemDetailCode })
                    .HasName("PK_PRDF");

                entity.Property(e => e.Prno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UOM");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.PurchaseRequisitionDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRDF_Item");

                entity.HasOne(d => d.PrnoNavigation)
                    .WithMany(p => p.PurchaseRequisitionDetailFile)
                    .HasPrincipalKey(p => p.Prno)
                    .HasForeignKey(d => d.Prno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRDF_Master");
            });

            modelBuilder.Entity<PurchaseRequisitionMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Prno, "UQ__Purchase__BC4023E35EA22ADE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateRequested).HasColumnType("datetime");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRNo");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RequestedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.PurchaseRequisitionMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_PRMF_ApprovedBy");

                entity.HasOne(d => d.RequestedByNavigation)
                    .WithMany(p => p.PurchaseRequisitionMasterFileRequestedByNavigation)
                    .HasForeignKey(d => d.RequestedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMF_RequestedBy");
            });

            modelBuilder.Entity<PurchaseReturnDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Prmno, e.ItemDetailCode })
                    .HasName("PK_PRD_File");

                entity.Property(e => e.Prmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRMNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.PurchaseReturnDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRD_File_Item");

                entity.HasOne(d => d.PrmnoNavigation)
                    .WithMany(p => p.PurchaseReturnDetailFile)
                    .HasPrincipalKey(p => p.Prmno)
                    .HasForeignKey(d => d.Prmno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRD_File_Master");
            });

            modelBuilder.Entity<PurchaseReturnMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Prmno, "UQ__Purchase__04B69824CEE638A4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DisPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FooterText)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Prmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRMNo");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Recuring)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rrno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RRNo");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Terms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypesOfPay)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.PurchaseReturnMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_PRMF_Approver");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.PurchaseReturnMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_PRMF_Preparer");

                entity.HasOne(d => d.SupplierNoNavigation)
                    .WithMany(p => p.PurchaseReturnMasterFile)
                    .HasForeignKey(d => d.SupplierNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRMF_Supplier");
            });

            modelBuilder.Entity<ReceivingReportDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Rrno, e.ItemDetailCode })
                    .HasName("PK_RRD_File");

                entity.Property(e => e.Rrno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RRNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.PretrunRecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PRetrunRecStatus");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.ReceivingReportDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RRD_File_Item");

                entity.HasOne(d => d.RrnoNavigation)
                    .WithMany(p => p.ReceivingReportDetailFile)
                    .HasPrincipalKey(p => p.Rrno)
                    .HasForeignKey(d => d.Rrno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RRD_File_Master");
            });

            modelBuilder.Entity<ReceivingReportMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Rrno, "UQ__Receivin__E305359DADD44A77")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DisPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DisTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FooterText)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PONo");

                entity.Property(e => e.PreturnRecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PReturnRecStatus");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ReceivedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Recuring)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rrno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RRNo");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Terms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TermsAndCondition)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TypesOfPay)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VerifiedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PonoNavigation)
                    .WithMany(p => p.ReceivingReportMasterFile)
                    .HasPrincipalKey(p => p.Pono)
                    .HasForeignKey(d => d.Pono)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RRMF_PurchaseOrder");

                entity.HasOne(d => d.ReceivedByNavigation)
                    .WithMany(p => p.ReceivingReportMasterFileReceivedByNavigation)
                    .HasForeignKey(d => d.ReceivedBy)
                    .HasConstraintName("FK_RRMF_ReceivedBy");

                entity.HasOne(d => d.VerifiedByNavigation)
                    .WithMany(p => p.ReceivingReportMasterFileVerifiedByNavigation)
                    .HasForeignKey(d => d.VerifiedBy)
                    .HasConstraintName("FK_RRMF_VerifiedBy");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__AppRole__8A2B61609FEEAC07")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RolePermi__RoleI__214BF109");
            });

            modelBuilder.Entity<SalesInvoiceDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Simno, e.ItemDetailCode })
                    .HasName("PK_SIDF");

                entity.Property(e => e.Simno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SIMNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.SalesInvoiceDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIDF_Item");

                entity.HasOne(d => d.SimnoNavigation)
                    .WithMany(p => p.SalesInvoiceDetailFile)
                    .HasPrincipalKey(p => p.Simno)
                    .HasForeignKey(d => d.Simno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIDF_Master");
            });

            modelBuilder.Entity<SalesInvoiceMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Simno, "UQ__SalesInv__0CCABF0CCC8FF18F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Simno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SIMNo");

                entity.Property(e => e.Somno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SOMNo");

                entity.Property(e => e.Terms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.SalesInvoiceMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_SIMF_Approver");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany(p => p.SalesInvoiceMasterFile)
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIMF_Customer");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.SalesInvoiceMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_SIMF_Preparer");
            });

            modelBuilder.Entity<SalesOrderDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Somno, e.ItemDetailCode })
                    .HasName("PK_SODF");

                entity.Property(e => e.Somno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SOMNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.SalesOrderDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SODF_Item");

                entity.HasOne(d => d.SomnoNavigation)
                    .WithMany(p => p.SalesOrderDetailFile)
                    .HasPrincipalKey(p => p.Somno)
                    .HasForeignKey(d => d.Somno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SODF_Master");
            });

            modelBuilder.Entity<SalesOrderMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Somno, "UQ__SalesOrd__8376E1065DDBEA7E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Somno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SOMNo");

                entity.Property(e => e.Terms)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.SalesOrderMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_SOMF_Approver");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany(p => p.SalesOrderMasterFile)
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOMF_Customer");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.SalesOrderMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_SOMF_Preparer");
            });

            modelBuilder.Entity<SalesRef>(entity =>
            {
                entity.HasKey(e => e.EmpIdno)
                    .HasName("PK__SalesRef__F1AF43410DC2F0F9");

                entity.Property(e => e.EmpIdno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EmpIDNo");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpIdnoNavigation)
                    .WithOne(p => p.SalesRef)
                    .HasForeignKey<SalesRef>(d => d.EmpIdno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesRef_Employee");
            });

            modelBuilder.Entity<SalesReturnDetailFile>(entity =>
            {
                entity.HasKey(e => new { e.Srmno, e.ItemDetailCode })
                    .HasName("PK_SRDDF");

                entity.Property(e => e.Srmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SRMNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.SalesReturnDetailFile)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SRDDF_Item");

                entity.HasOne(d => d.SrmnoNavigation)
                    .WithMany(p => p.SalesReturnDetailFile)
                    .HasPrincipalKey(p => p.Srmno)
                    .HasForeignKey(d => d.Srmno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SRDDF_Master");
            });

            modelBuilder.Entity<SalesReturnMasterFile>(entity =>
            {
                entity.HasIndex(e => e.Srmno, "UQ__SalesRet__682B1C887B38B18F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Srmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SRMNo");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.SalesReturnMasterFileApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_SRMF_Approver");

                entity.HasOne(d => d.CustNoNavigation)
                    .WithMany(p => p.SalesReturnMasterFile)
                    .HasForeignKey(d => d.CustNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SRMF_Customer");

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.SalesReturnMasterFilePreparedByNavigation)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_SRMF_Preparer");
            });

            modelBuilder.Entity<StockCountDetail>(entity =>
            {
                entity.HasKey(e => new { e.Scmno, e.ItemDetailCode })
                    .HasName("PK_SCD_File");

                entity.Property(e => e.Scmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SCMNo");

                entity.Property(e => e.ItemDetailCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemDetailCodeNavigation)
                    .WithMany(p => p.StockCountDetail)
                    .HasPrincipalKey(p => p.ItemDetailCode)
                    .HasForeignKey(d => d.ItemDetailCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SCD_File_Item");

                entity.HasOne(d => d.ScmnoNavigation)
                    .WithMany(p => p.StockCountDetail)
                    .HasForeignKey(d => d.Scmno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SCD_File_Master");
            });

            modelBuilder.Entity<StockCountMaster>(entity =>
            {
                entity.HasKey(e => e.Scmno)
                    .HasName("PK__StockCou__C486910131F54653");

                entity.Property(e => e.Scmno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SCMNo");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.PreparedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PreparedByNavigation)
                    .WithMany(p => p.StockCountMaster)
                    .HasForeignKey(d => d.PreparedBy)
                    .HasConstraintName("FK_SCM_Preparer");

                entity.HasOne(d => d.WarehouseCodeNavigation)
                    .WithMany(p => p.StockCountMaster)
                    .HasForeignKey(d => d.WarehouseCode)
                    .HasPrincipalKey(p => p.WarehouseCode) // <-- important
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SCM_Warehouse");
            });

            modelBuilder.Entity<StockTransfer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__StockTra__954900917D862FE9");

                entity.HasIndex(e => e.FromWarehouseId, "IX_StockTransfer_FromWarehouseId");

                entity.HasIndex(e => e.ToWarehouseId, "IX_StockTransfer_ToWarehouseId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransferDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransferNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StockTransferDetail>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__StockTra__135C316DF907A263");

                entity.HasIndex(e => e.ItemDetailNo, "IX_StockTransferDetail_ItemId");

                entity.Property(e => e.ItemDetailNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Transfer)
                    .WithMany(p => p.StockTransferDetail)
                    .HasForeignKey(d => d.TransferId)
                    .HasConstraintName("FK_StockTransferDetail_Transfer");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierNo)
                    .HasName("PK__Supplier__4BE69980703CEA42");

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Address1)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FaxNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastPono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LastPONo");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierImage>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FilePath).IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.SupplierNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.SupplierNoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SupplierNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierImage_Supplier");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LoggedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoggedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.StackTrace).IsUnicode(false);
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__SystemSe__54372B1D49877DA8");

                entity.HasIndex(e => e.SettingKey, "UQ__SystemSe__01E719AD3AB117F7")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SettingKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SettingValue)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__AppUser__1788CCAC4FA3E258");

                entity.HasIndex(e => e.Username, "UQ__AppUser__536C85E41B998E2D")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__AppUser__ID__4316F928");
            });

            //modelBuilder.Entity<Warehouse>(entity =>
            //{
            //    entity.HasKey(e => e.WarehouseCode)
            //        .HasName("PK__Warehous__1686A0577848ADE6");

            //    entity.Property(e => e.WarehouseCode)
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Address)
            //        .HasMaxLength(200)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Id)
            //        .ValueGeneratedOnAdd()
            //        .HasColumnName("ID");

            //    entity.Property(e => e.Name)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.RecStatus)
            //        .HasMaxLength(5)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Remarks)
            //        .HasMaxLength(150)
            //        .IsUnicode(false);

            //});
            modelBuilder.Entity<Warehouse>(entity =>
            {
                // PK
                entity.HasKey(e => e.Id)
                    .HasName("PK_Warehouse");

                // Alternate key for WarehouseCode
                entity.HasAlternateKey(e => e.WarehouseCode)
                    .HasName("AK_Warehouse_WarehouseCode");

                // Columns
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.WarehouseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RecStatus)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
