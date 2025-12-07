using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: false),
                    IsFinalLevel = table.Column<bool>(type: "bit", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Approval__328477F4AB8F2DEA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PrimaryKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    OldValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NewValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Address1 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Address2 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Address3 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AcountNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CreditCardNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CreditCardType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreditCardName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreditCardExpiry = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSONo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LastSINo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LastDRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LastOR = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LastSRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__049E631A03F11273", x => x.CustNo);
                });

            migrationBuilder.CreateTable(
                name: "DocumentReference",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ModuleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DateReferenced = table.Column<DateTime>(type: "datetime", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__00AFA65AF267FF37", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Prefix = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    NextNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Suffix = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__F3A1C161267DBA9B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBalance",
                columns: table => new
                {
                    ItemDetailNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBalance", x => new { x.ItemDetailNo, x.WarehouseId });
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ContactPerson = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Address1 = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Address2 = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FaxNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LastPONo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__4BE69980703CEA42", x => x.SupplierNo);
                });

            migrationBuilder.CreateTable(
                name: "SystemLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    StackTrace = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    LoggedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LoggedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SystemSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingKey = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SystemSe__54372B1D49877DA8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Picture = table.Column<byte[]>(type: "image", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CustomerImage_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseCategory_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PermissionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK__RolePermi__RoleI__214BF109",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppUser__1788CCAC4FA3E258", x => x.ID);
                    table.ForeignKey(
                        name: "FK__AppUser__ID__4316F928",
                        column: x => x.ID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SupplierImage_Supplier",
                        column: x => x.SupplierNo,
                        principalTable: "Supplier",
                        principalColumn: "SupplierNo");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Desc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.UniqueConstraint("AK_Item_ItemCode", x => x.ItemCode);
                    table.ForeignKey(
                        name: "FK_Item_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    MStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Religion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EduAttentment = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DateHired = table.Column<DateTime>(type: "datetime", nullable: true),
                    Department = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Position = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MobileNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__F1AF4341291D5E9B", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_Employee_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holiday_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Holiday_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustmentNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AdjustmentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__E60DB893D347800C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustment_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FromWarehouseId = table.Column<int>(type: "int", nullable: false),
                    ToWarehouseId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StockTra__954900917D862FE9", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransfer_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.ID);
                    table.UniqueConstraint("AK_Warehouse_WarehouseCode", x => x.WarehouseCode);
                    table.ForeignKey(
                        name: "FK_Warehouse_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostingHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostingHistory_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostingHistory_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemBarcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBarcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBarcode_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ItemId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SerialNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    LocationCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UnitMeasure = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Unitprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    EOQ = table.Column<int>(type: "int", nullable: true),
                    ROP = table.Column<int>(type: "int", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetail", x => x.ID);
                    table.UniqueConstraint("AK_ItemDetail_ItemDetailCode", x => x.ItemDetailCode);
                    table.ForeignKey(
                        name: "FK_ItemDetail_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPriceHistory_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPriceHistory_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RefNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Approval__328477D4E53D987F", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalHistory_Employee",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "Approver",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalLevel = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Approver__F1AF4341B8E79E9C", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_Approver_Employee",
                        column: x => x.EmpIDNo,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "Checker",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Checker__F1AF43414B09EA43", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_Checker_Employee",
                        column: x => x.EmpIDNo,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "Deliverer",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivere__F1AF4341883C675B", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_Deliverer_Employee",
                        column: x => x.EmpIDNo,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryReceiptMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DRMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SIMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Terms = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypesOfPay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Comments = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TermsAndCondition = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FooterText = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Recuring = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeliveryCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PrepBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryReceiptMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_DeliveryReceiptMasterFile_DRMNo", x => x.DRMNo);
                    table.ForeignKey(
                        name: "FK_DeliveryReceiptMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DRMF_ApprBy_Employee",
                        column: x => x.ApprBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_DRMF_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                    table.ForeignKey(
                        name: "FK_DRMF_PrepBy_Employee",
                        column: x => x.PrepBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeImage",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__F1AF43410F4AF797", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_EmpImage_Employee",
                        column: x => x.EmpIDNo,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "OfficialReceiptMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TotAmtPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FormPay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CashAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    BankName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Comments = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TermsAndCondition = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FooterText = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Recuring = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialReceiptMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_OfficialReceiptMasterFile_ORNo", x => x.ORNo);
                    table.ForeignKey(
                        name: "FK_OfficialReceiptMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_ORMF_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                    table.ForeignKey(
                        name: "FK_ORMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PONo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Terms = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    PRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TypesofPay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateNeeded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReferenceNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comments = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TermsAndCondition = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FooterText = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Recuring = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RRRecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    PReturnRecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_PurchaseOrderMasterFile_PONo", x => x.PONo);
                    table.ForeignKey(
                        name: "FK_POMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_POMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_POMF_Supplier",
                        column: x => x.SupplierNo,
                        principalTable: "Supplier",
                        principalColumn: "SupplierNo");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequisitionMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RequestedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Department = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateRequested = table.Column<DateTime>(type: "datetime", nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequisitionMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_PurchaseRequisitionMasterFile_PRNo", x => x.PRNo);
                    table.ForeignKey(
                        name: "FK_PRMF_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_PRMF_RequestedBy",
                        column: x => x.RequestedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitionMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    RRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RefNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Terms = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypesOfPay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Comments = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TermsAndCondition = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FooterText = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Recuring = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturnMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_PurchaseReturnMasterFile_PRMNo", x => x.PRMNo);
                    table.ForeignKey(
                        name: "FK_PRMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_PRMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_PRMF_Supplier",
                        column: x => x.SupplierNo,
                        principalTable: "Supplier",
                        principalColumn: "SupplierNo");
                    table.ForeignKey(
                        name: "FK_PurchaseReturnMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SOMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Terms = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypesOfPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeliveryCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_SalesInvoiceMasterFile_SIMNo", x => x.SIMNo);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SIMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_SIMF_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                    table.ForeignKey(
                        name: "FK_SIMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SOMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Terms = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypesOfPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_SalesOrderMasterFile_SOMNo", x => x.SOMNo);
                    table.ForeignKey(
                        name: "FK_SalesOrderMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_SOMF_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                    table.ForeignKey(
                        name: "FK_SOMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesRef",
                columns: table => new
                {
                    EmpIDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalesRef__F1AF43410DC2F0F9", x => x.EmpIDNo);
                    table.ForeignKey(
                        name: "FK_SalesRef_Employee",
                        column: x => x.EmpIDNo,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SRMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CustNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApprovedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Simno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypesOfPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturnMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_SalesReturnMasterFile_SRMNo", x => x.SRMNo);
                    table.ForeignKey(
                        name: "FK_SalesReturnMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SRMF_Approver",
                        column: x => x.ApprovedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_SRMF_Customer",
                        column: x => x.CustNo,
                        principalTable: "Customer",
                        principalColumn: "CustNo");
                    table.ForeignKey(
                        name: "FK_SRMF_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AdjustmentId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__135C316DECB280CA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustmentDetail_Adjustment",
                        column: x => x.Id,
                        principalTable: "InventoryAdjustment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StockTra__135C316DF907A263", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferDetail_Transfer",
                        column: x => x.TransferId,
                        principalTable: "StockTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemWarehouseMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouseMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouseMapping_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouseMapping_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemWarehouseMapping_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__DDB144D4630D953F", x => x.LocationCode);
                    table.ForeignKey(
                        name: "FK_Location_Warehouse",
                        column: x => x.WarehouseCode,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseCode");
                });

            migrationBuilder.CreateTable(
                name: "StockCountMaster",
                columns: table => new
                {
                    SCMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PreparedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StockCou__C486910131F54653", x => x.SCMNo);
                    table.ForeignKey(
                        name: "FK_SCM_Preparer",
                        column: x => x.PreparedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_SCM_Warehouse",
                        column: x => x.WarehouseCode,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseCode");
                    table.ForeignKey(
                        name: "FK_StockCountMaster_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RefModule = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RefNo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RefId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Remarks = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    ItemDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventor__55433A6B09B21C7C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_ItemDetail_ItemDetailId",
                        column: x => x.ItemDetailId,
                        principalTable: "ItemDetail",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ItemImage",
                columns: table => new
                {
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ItemImag__67E9794A501DB4FC", x => x.ItemDetailCode);
                    table.ForeignKey(
                        name: "FK_ItemImage_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                });

            migrationBuilder.CreateTable(
                name: "ItemSupplier",
                columns: table => new
                {
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LeadTime = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Terms = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSupplier", x => new { x.ItemDetailCode, x.SupplierNo });
                    table.ForeignKey(
                        name: "FK_ItemSupplier_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_ItemSupplier_Supplier",
                        column: x => x.SupplierNo,
                        principalTable: "Supplier",
                        principalColumn: "SupplierNo");
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitMeasure",
                columns: table => new
                {
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    UnitCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnitMeasure", x => new { x.ItemDetailCode, x.UnitCode });
                    table.ForeignKey(
                        name: "FK_IUM_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryReceiptDetailFile",
                columns: table => new
                {
                    DRDNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    QtyDel = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryReceiptDetailFile", x => new { x.DRDNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_DRDF_DRMF",
                        column: x => x.DRDNo,
                        principalTable: "DeliveryReceiptMasterFile",
                        principalColumn: "DRMNo");
                    table.ForeignKey(
                        name: "FK_DRDF_ItemDetail",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetailFile",
                columns: table => new
                {
                    PONo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtyOrder = table.Column<int>(type: "int", nullable: true),
                    QtyReceived = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PODF", x => new { x.PONo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_PODF_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_PODF_Master",
                        column: x => x.PONo,
                        principalTable: "PurchaseOrderMasterFile",
                        principalColumn: "PONo");
                });

            migrationBuilder.CreateTable(
                name: "ReceivingReportMasterFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PONo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceivedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    VerifiedBy = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    SupplierNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    RefNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Terms = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TypesOfPay = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Comments = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TermsAndCondition = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FooterText = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Recuring = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DisTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PReturnRecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingReportMasterFile", x => x.ID);
                    table.UniqueConstraint("AK_ReceivingReportMasterFile_RRNo", x => x.RRNo);
                    table.ForeignKey(
                        name: "FK_ReceivingReportMasterFile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RRMF_PurchaseOrder",
                        column: x => x.PONo,
                        principalTable: "PurchaseOrderMasterFile",
                        principalColumn: "PONo");
                    table.ForeignKey(
                        name: "FK_RRMF_ReceivedBy",
                        column: x => x.ReceivedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                    table.ForeignKey(
                        name: "FK_RRMF_VerifiedBy",
                        column: x => x.VerifiedBy,
                        principalTable: "Employee",
                        principalColumn: "EmpIDNo");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequisitionDetailFile",
                columns: table => new
                {
                    PRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtyRequested = table.Column<int>(type: "int", nullable: true),
                    UOM = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    QtyOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRDF", x => new { x.PRNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_PRDF_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_PRDF_Master",
                        column: x => x.PRNo,
                        principalTable: "PurchaseRequisitionMasterFile",
                        principalColumn: "PRNo");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturnDetailFile",
                columns: table => new
                {
                    PRMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRD_File", x => new { x.PRMNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_PRD_File_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_PRD_File_Master",
                        column: x => x.PRMNo,
                        principalTable: "PurchaseReturnMasterFile",
                        principalColumn: "PRMNo");
                });

            migrationBuilder.CreateTable(
                name: "OfficialReceiptDetailFile",
                columns: table => new
                {
                    ORNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SIMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORD_File", x => new { x.ORNo, x.SIMNo });
                    table.ForeignKey(
                        name: "FK_ORD_File_Master",
                        column: x => x.ORNo,
                        principalTable: "OfficialReceiptMasterFile",
                        principalColumn: "ORNo");
                    table.ForeignKey(
                        name: "FK_ORD_File_SalesInvoice",
                        column: x => x.SIMNo,
                        principalTable: "SalesInvoiceMasterFile",
                        principalColumn: "SIMNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceDetailFile",
                columns: table => new
                {
                    SIMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtyInv = table.Column<int>(type: "int", nullable: true),
                    QtyRet = table.Column<int>(type: "int", nullable: true),
                    QtyDel = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrrecStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIDF", x => new { x.SIMNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_SIDF_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_SIDF_Master",
                        column: x => x.SIMNo,
                        principalTable: "SalesInvoiceMasterFile",
                        principalColumn: "SIMNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDetailFile",
                columns: table => new
                {
                    SOMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtyOnOrder = table.Column<int>(type: "int", nullable: true),
                    QtyInvoice = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SODF", x => new { x.SOMNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_SODF_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_SODF_Master",
                        column: x => x.SOMNo,
                        principalTable: "SalesOrderMasterFile",
                        principalColumn: "SOMNo");
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnDetailFile",
                columns: table => new
                {
                    SRMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRDDF", x => new { x.SRMNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_SRDDF_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_SRDDF_Master",
                        column: x => x.SRMNo,
                        principalTable: "SalesReturnMasterFile",
                        principalColumn: "SRMNo");
                });

            migrationBuilder.CreateTable(
                name: "ItemInventory",
                columns: table => new
                {
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    WarehouseCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyOnHand = table.Column<int>(type: "int", nullable: true),
                    QtyOnOrder = table.Column<int>(type: "int", nullable: true),
                    QtyReserved = table.Column<int>(type: "int", nullable: true),
                    ExtendedQtyOnHand = table.Column<int>(type: "int", nullable: true),
                    SalesReturnItemQty = table.Column<int>(type: "int", nullable: true),
                    PurchaseReturnItemQty = table.Column<int>(type: "int", nullable: true),
                    LastStockCount = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    LocationCodeNavigationLocationCode = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventory", x => new { x.ItemDetailCode, x.WarehouseCode });
                    table.ForeignKey(
                        name: "FK_II_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_II_Warehouse",
                        column: x => x.WarehouseCode,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseCode");
                    table.ForeignKey(
                        name: "FK_ItemInventory_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemInventory_Location_LocationCodeNavigationLocationCode",
                        column: x => x.LocationCodeNavigationLocationCode,
                        principalTable: "Location",
                        principalColumn: "LocationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCountDetail",
                columns: table => new
                {
                    SCMNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountedQty = table.Column<int>(type: "int", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCD_File", x => new { x.SCMNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_SCD_File_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_SCD_File_Master",
                        column: x => x.SCMNo,
                        principalTable: "StockCountMaster",
                        principalColumn: "SCMNo");
                });

            migrationBuilder.CreateTable(
                name: "ReceivingReportDetailFile",
                columns: table => new
                {
                    RRNo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ItemDetailCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtyReceived = table.Column<int>(type: "int", nullable: true),
                    Uprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    QtyReturn = table.Column<int>(type: "int", nullable: true),
                    PRetrunRecStatus = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RRD_File", x => new { x.RRNo, x.ItemDetailCode });
                    table.ForeignKey(
                        name: "FK_RRD_File_Item",
                        column: x => x.ItemDetailCode,
                        principalTable: "ItemDetail",
                        principalColumn: "ItemDetailCode");
                    table.ForeignKey(
                        name: "FK_RRD_File_Master",
                        column: x => x.RRNo,
                        principalTable: "ReceivingReportMasterFile",
                        principalColumn: "RRNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistory_ApprovedBy",
                table: "ApprovalHistory",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanyId",
                table: "Branch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_BrandId",
                table: "Category",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingHistory_BranchId",
                table: "CostingHistory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CostingHistory_ItemId",
                table: "CostingHistory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerImage_CustNo",
                table: "CustomerImage",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReceiptDetailFile_ItemDetailCode",
                table: "DeliveryReceiptDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReceiptMasterFile_ApprBy",
                table: "DeliveryReceiptMasterFile",
                column: "ApprBy");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReceiptMasterFile_BranchId",
                table: "DeliveryReceiptMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReceiptMasterFile_CustNo",
                table: "DeliveryReceiptMasterFile",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryReceiptMasterFile_PrepBy",
                table: "DeliveryReceiptMasterFile",
                column: "PrepBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__0CCABF0CF5B0954F",
                table: "DeliveryReceiptMasterFile",
                column: "SIMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Delivery__59BA730CC90D05B3",
                table: "DeliveryReceiptMasterFile",
                column: "DRMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BranchId",
                table: "Employee",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseCategoryId",
                table: "Expense",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_BranchId",
                table: "Holiday",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Holiday_CompanyId",
                table: "Holiday",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_BranchId",
                table: "InventoryAdjustment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustment_WarehouseId",
                table: "InventoryAdjustment",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentDetail_ItemId",
                table: "InventoryAdjustmentDetail",
                column: "ItemDetailNo");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBalance_WarehouseId",
                table: "InventoryBalance",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_AccountId",
                table: "InventoryTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_BranchId",
                table: "InventoryTransaction",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_CompanyId",
                table: "InventoryTransaction",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_ItemDetailId",
                table: "InventoryTransaction",
                column: "ItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_ItemId",
                table: "InventoryTransaction",
                column: "ItemDetailNo");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_RefModule_RefNo",
                table: "InventoryTransaction",
                columns: new[] { "RefModule", "RefNo" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_WarehouseId",
                table: "InventoryTransaction",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandId",
                table: "Item",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ClassificationId",
                table: "Item",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBarcode_ItemId",
                table: "ItemBarcode",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetail_ItemId",
                table: "ItemDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventory_BranchId",
                table: "ItemInventory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventory_LocationCodeNavigationLocationCode",
                table: "ItemInventory",
                column: "LocationCodeNavigationLocationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventory_WarehouseCode",
                table: "ItemInventory",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceHistory_BranchId",
                table: "ItemPriceHistory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceHistory_ItemId",
                table: "ItemPriceHistory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSupplier_SupplierNo",
                table: "ItemSupplier",
                column: "SupplierNo");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseMapping_BranchId",
                table: "ItemWarehouseMapping",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseMapping_ItemId",
                table: "ItemWarehouseMapping",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseMapping_WarehouseId",
                table: "ItemWarehouseMapping",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_WarehouseCode",
                table: "Location",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialReceiptDetailFile_SIMNo",
                table: "OfficialReceiptDetailFile",
                column: "SIMNo");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialReceiptMasterFile_ApprovedBy",
                table: "OfficialReceiptMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialReceiptMasterFile_BranchId",
                table: "OfficialReceiptMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialReceiptMasterFile_CustNo",
                table: "OfficialReceiptMasterFile",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialReceiptMasterFile_PreparedBy",
                table: "OfficialReceiptMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Official__A9A8848C9A1441F0",
                table: "OfficialReceiptMasterFile",
                column: "ORNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetailFile_ItemDetailCode",
                table: "PurchaseOrderDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderMasterFile_ApprovedBy",
                table: "PurchaseOrderMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderMasterFile_BranchId",
                table: "PurchaseOrderMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderMasterFile_PreparedBy",
                table: "PurchaseOrderMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderMasterFile_SupplierNo",
                table: "PurchaseOrderMasterFile",
                column: "SupplierNo");

            migrationBuilder.CreateIndex(
                name: "UQ__Purchase__5F02AA86C53F42D1",
                table: "PurchaseOrderMasterFile",
                column: "PONo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionDetailFile_ItemDetailCode",
                table: "PurchaseRequisitionDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionMasterFile_ApprovedBy",
                table: "PurchaseRequisitionMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionMasterFile_BranchId",
                table: "PurchaseRequisitionMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitionMasterFile_RequestedBy",
                table: "PurchaseRequisitionMasterFile",
                column: "RequestedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Purchase__BC4023E35EA22ADE",
                table: "PurchaseRequisitionMasterFile",
                column: "PRNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnDetailFile_ItemDetailCode",
                table: "PurchaseReturnDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnMasterFile_ApprovedBy",
                table: "PurchaseReturnMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnMasterFile_BranchId",
                table: "PurchaseReturnMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnMasterFile_PreparedBy",
                table: "PurchaseReturnMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnMasterFile_SupplierNo",
                table: "PurchaseReturnMasterFile",
                column: "SupplierNo");

            migrationBuilder.CreateIndex(
                name: "UQ__Purchase__04B69824CEE638A4",
                table: "PurchaseReturnMasterFile",
                column: "PRMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingReportDetailFile_ItemDetailCode",
                table: "ReceivingReportDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingReportMasterFile_BranchId",
                table: "ReceivingReportMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingReportMasterFile_PONo",
                table: "ReceivingReportMasterFile",
                column: "PONo");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingReportMasterFile_ReceivedBy",
                table: "ReceivingReportMasterFile",
                column: "ReceivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingReportMasterFile_VerifiedBy",
                table: "ReceivingReportMasterFile",
                column: "VerifiedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Receivin__E305359DADD44A77",
                table: "ReceivingReportMasterFile",
                column: "RRNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__AppRole__8A2B61609FEEAC07",
                table: "Role",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleID",
                table: "RolePermission",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceDetailFile_ItemDetailCode",
                table: "SalesInvoiceDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceMasterFile_ApprovedBy",
                table: "SalesInvoiceMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceMasterFile_BranchId",
                table: "SalesInvoiceMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceMasterFile_CustNo",
                table: "SalesInvoiceMasterFile",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceMasterFile_PreparedBy",
                table: "SalesInvoiceMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__SalesInv__0CCABF0CCC8FF18F",
                table: "SalesInvoiceMasterFile",
                column: "SIMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetailFile_ItemDetailCode",
                table: "SalesOrderDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderMasterFile_ApprovedBy",
                table: "SalesOrderMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderMasterFile_BranchId",
                table: "SalesOrderMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderMasterFile_CustNo",
                table: "SalesOrderMasterFile",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderMasterFile_PreparedBy",
                table: "SalesOrderMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__SalesOrd__8376E1065DDBEA7E",
                table: "SalesOrderMasterFile",
                column: "SOMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetailFile_ItemDetailCode",
                table: "SalesReturnDetailFile",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnMasterFile_ApprovedBy",
                table: "SalesReturnMasterFile",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnMasterFile_BranchId",
                table: "SalesReturnMasterFile",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnMasterFile_CustNo",
                table: "SalesReturnMasterFile",
                column: "CustNo");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnMasterFile_PreparedBy",
                table: "SalesReturnMasterFile",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__SalesRet__682B1C887B38B18F",
                table: "SalesReturnMasterFile",
                column: "SRMNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCountDetail_ItemDetailCode",
                table: "StockCountDetail",
                column: "ItemDetailCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountMaster_BranchId",
                table: "StockCountMaster",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountMaster_PreparedBy",
                table: "StockCountMaster",
                column: "PreparedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountMaster_WarehouseCode",
                table: "StockCountMaster",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfer_BranchId",
                table: "StockTransfer",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfer_FromWarehouseId",
                table: "StockTransfer",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfer_ToWarehouseId",
                table: "StockTransfer",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetail_ItemId",
                table: "StockTransferDetail",
                column: "ItemDetailNo");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetail_TransferId",
                table: "StockTransferDetail",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierImage_SupplierNo",
                table: "SupplierImage",
                column: "SupplierNo");

            migrationBuilder.CreateIndex(
                name: "UQ__SystemSe__01E719AD3AB117F7",
                table: "SystemSetting",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__AppUser__536C85E41B998E2D",
                table: "UserAccount",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouse",
                column: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalFlow");

            migrationBuilder.DropTable(
                name: "ApprovalHistory");

            migrationBuilder.DropTable(
                name: "Approver");

            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "Checker");

            migrationBuilder.DropTable(
                name: "CostingHistory");

            migrationBuilder.DropTable(
                name: "CustomerImage");

            migrationBuilder.DropTable(
                name: "Deliverer");

            migrationBuilder.DropTable(
                name: "DeliveryReceiptDetailFile");

            migrationBuilder.DropTable(
                name: "DocumentReference");

            migrationBuilder.DropTable(
                name: "DocumentSeries");

            migrationBuilder.DropTable(
                name: "EmployeeImage");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentDetail");

            migrationBuilder.DropTable(
                name: "InventoryBalance");

            migrationBuilder.DropTable(
                name: "InventoryTransaction");

            migrationBuilder.DropTable(
                name: "ItemBarcode");

            migrationBuilder.DropTable(
                name: "ItemImage");

            migrationBuilder.DropTable(
                name: "ItemInventory");

            migrationBuilder.DropTable(
                name: "ItemPriceHistory");

            migrationBuilder.DropTable(
                name: "ItemSupplier");

            migrationBuilder.DropTable(
                name: "ItemUnitMeasure");

            migrationBuilder.DropTable(
                name: "ItemWarehouseMapping");

            migrationBuilder.DropTable(
                name: "OfficialReceiptDetailFile");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetailFile");

            migrationBuilder.DropTable(
                name: "PurchaseRequisitionDetailFile");

            migrationBuilder.DropTable(
                name: "PurchaseReturnDetailFile");

            migrationBuilder.DropTable(
                name: "ReceivingReportDetailFile");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SalesInvoiceDetailFile");

            migrationBuilder.DropTable(
                name: "SalesOrderDetailFile");

            migrationBuilder.DropTable(
                name: "SalesRef");

            migrationBuilder.DropTable(
                name: "SalesReturnDetailFile");

            migrationBuilder.DropTable(
                name: "StockCountDetail");

            migrationBuilder.DropTable(
                name: "StockTransferDetail");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "SupplierImage");

            migrationBuilder.DropTable(
                name: "SystemLog");

            migrationBuilder.DropTable(
                name: "SystemSetting");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "DeliveryReceiptMasterFile");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "InventoryAdjustment");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "OfficialReceiptMasterFile");

            migrationBuilder.DropTable(
                name: "PurchaseRequisitionMasterFile");

            migrationBuilder.DropTable(
                name: "PurchaseReturnMasterFile");

            migrationBuilder.DropTable(
                name: "ReceivingReportMasterFile");

            migrationBuilder.DropTable(
                name: "SalesInvoiceMasterFile");

            migrationBuilder.DropTable(
                name: "SalesOrderMasterFile");

            migrationBuilder.DropTable(
                name: "SalesReturnMasterFile");

            migrationBuilder.DropTable(
                name: "ItemDetail");

            migrationBuilder.DropTable(
                name: "StockCountMaster");

            migrationBuilder.DropTable(
                name: "StockTransfer");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "PurchaseOrderMasterFile");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
