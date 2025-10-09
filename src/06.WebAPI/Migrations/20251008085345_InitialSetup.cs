using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
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
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Row = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourses",
                columns: table => new
                {
                    MenuCourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCourses", x => x.MenuCourseId);
                    table.ForeignKey(
                        name: "FK_MenuCourses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoInvoice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCourse = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourseSchedules",
                columns: table => new
                {
                    MSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MenuCourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyClasses",
                columns: table => new
                {
                    MyClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MenuCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClasses", x => x.MyClassId);
                    table.ForeignKey(
                        name: "FK_MyClasses_MenuCourses_MenuCourseId",
                        column: x => x.MenuCourseId,
                        principalTable: "MenuCourses",
                        principalColumn: "MenuCourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyClasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceMenuCourses",
                columns: table => new
                {
                    IMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    MSId = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Image", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "asian.svg", "Asian", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "western.svg", "Western", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "PaymentMethodId", "CreatedAt", "Logo", "Name", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "bca.png", "BCA", "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "gopay.png", "GoPay", "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "CreatedAt", "ScheduleDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Name", "Password", "Row", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Admin User", "admin123", "Admin", "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "John Doe", "password", "User", "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreatedAt", "Date", "NoInvoice", "TotalCourse", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-001", 1, 100000.0, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "MenuCourses",
                columns: new[] { "MenuCourseId", "CategoryId", "CreatedAt", "Description", "Image", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "tomyum", "tomyum.svg", "Tomyum", 100000.0, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn design principles", "pizza.svg", "Pizza", 150000.0, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MenuCourseSchedules",
                columns: new[] { "MSId", "Available", "CreatedAt", "MenuCourseId", "ScheduleId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 8, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Active", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MyClasses",
                columns: new[] { "MyClassId", "CreatedAt", "MenuCourseId", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "InvoiceMenuCourses",
                columns: new[] { "IMId", "CreatedAt", "InvoiceId", "MSId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMenuCourses_InvoiceId",
                table: "InvoiceMenuCourses",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMenuCourses_MSId",
                table: "InvoiceMenuCourses",
                column: "MSId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_CategoryId",
                table: "MenuCourses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourseSchedules_MenuCourseId",
                table: "MenuCourseSchedules",
                column: "MenuCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourseSchedules_ScheduleId",
                table: "MenuCourseSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_MenuCourseId",
                table: "MyClasses",
                column: "MenuCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_UserId",
                table: "MyClasses",
                column: "UserId");
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
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MenuCourseSchedules");

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
