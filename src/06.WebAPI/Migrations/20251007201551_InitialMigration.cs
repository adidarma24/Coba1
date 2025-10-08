using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourses",
                columns: table => new
                {
                    MenuCourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCourses", x => x.MenuCourseId);
                    table.ForeignKey(
                        name: "FK_MenuCourses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoInvoice = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCourse = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserIdRef = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserIdRef",
                        column: x => x.UserIdRef,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourseSchedules",
                columns: table => new
                {
                    MSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableSlot = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MenuCourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCourseSchedules", x => x.MSId);
                    table.ForeignKey(
                        name: "FK_MenuCourseSchedules_MenuCourses_MenuCourseId",
                        column: x => x.MenuCourseId,
                        principalTable: "MenuCourses",
                        principalColumn: "MenuCourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCourseSchedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceMenuCourses",
                columns: table => new
                {
                    IMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    MSId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceMenuCourses", x => x.IMId);
                    table.ForeignKey(
                        name: "FK_InvoiceMenuCourses_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceMenuCourses_MenuCourseSchedules_MSId",
                        column: x => x.MSId,
                        principalTable: "MenuCourseSchedules",
                        principalColumn: "MSId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyClasses",
                columns: table => new
                {
                    MyClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdRef = table.Column<int>(type: "int", nullable: false),
                    MSId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClasses", x => x.MyClassId);
                    table.ForeignKey(
                        name: "FK_MyClasses_MenuCourseSchedules_MSId",
                        column: x => x.MSId,
                        principalTable: "MenuCourseSchedules",
                        principalColumn: "MSId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyClasses_Users_UserIdRef",
                        column: x => x.UserIdRef,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Image", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "asian.svg", "Asian", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "coldDrink.svg", "Cold Drink", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "cookies.svg", "Cookies", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedAt", "Logo", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "gopay.svg", "Gopay", 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "dana.svg", "Dana", 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "CreatedAt", "ScheduleDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MenuCourses",
                columns: new[] { "MenuCourseId", "CategoryId", "CreatedAt", "Description", "Image", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom Yum dari Thailand", "tomyum.jpg", "Tom Yum Thailand", 450000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minuman rasa strawberry", "strawberry_float.jpg", "Strawberry Float", 150000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chocholate Cookies", "cookies.jpg", "Chocholate Cookies", 200000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soto Banjar Limau Kuit", "greentea_cheesecake.jpg", "Soto Banjar Limau Kuit", 150000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Green Tea Cheesecake", "cheesecake.jpg", "Green Tea Cheesecake", 400000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spagetti", "spagetti.jpg", "Italian Spagetti Bolognese", 450000m, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MenuCourseSchedules",
                columns: new[] { "MSId", "AvailableSlot", "CreatedAt", "MenuCourseId", "ScheduleId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 12, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 10, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 9, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 10, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 5, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 10, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 0, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMenuCourses_InvoiceId",
                table: "InvoiceMenuCourses",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMenuCourses_MSId",
                table: "InvoiceMenuCourses",
                column: "MSId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_NoInvoice",
                table: "Invoices",
                column: "NoInvoice",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserIdRef",
                table: "Invoices",
                column: "UserIdRef");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_CategoryId",
                table: "MenuCourses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_Name_CategoryId",
                table: "MenuCourses",
                columns: new[] { "Name", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourseSchedules_MenuCourseId",
                table: "MenuCourseSchedules",
                column: "MenuCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourseSchedules_ScheduleId",
                table: "MenuCourseSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_MSId",
                table: "MyClasses",
                column: "MSId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_UserIdRef",
                table: "MyClasses",
                column: "UserIdRef");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceMenuCourses");

            migrationBuilder.DropTable(
                name: "MyClasses");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MenuCourseSchedules");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MenuCourses");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
