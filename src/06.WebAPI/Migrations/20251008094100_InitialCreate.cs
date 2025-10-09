using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Nama kategori - harus unik"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Nama dari menu course"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "URL path ke gambar menu course"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Harga menu course dalam format decimal"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Deskripsi detail dari menu course"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key ke tabel Categories")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuCourses_Categories",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuCourse_Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableSlot = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MenuCourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCourse_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuCourse_Schedules_MenuCourses_MenuCourseId",
                        column: x => x.MenuCourseId,
                        principalTable: "MenuCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCourse_Schedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Image", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Asian", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cold Drink", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cookies", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Desert", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Eastern", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hot Drink", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Junkfood", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Western", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "CreatedAt", "ScheduleDate", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 20, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 22, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 25, 19, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "MenuCourses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Image", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nasi goreng klasik dengan telur dan ayam.", null, "Nasi Goreng Spesial", 25000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Minuman teh dingin yang menyegarkan.", null, "Es Teh Manis", 5000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kue kering dengan taburan choco chip.", null, "Choco Chip Cookies", 15000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dessert puding rasa coklat.", null, "Pudding Coklat", 12000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Daging panggang dengan sayuran dalam roti pita.", null, "Kebab Turki", 30000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kopi hitam panas tanpa gula.", null, "Kopi Hitam", 8000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Potongan kentang yang digoreng renyah.", null, "Kentang Goreng", 18000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pasta dengan saus daging tomat klasik.", null, "Spaghetti Bolognese", 45000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "MenuCourse_Schedules",
                columns: new[] { "Id", "AvailableSlot", "CreatedAt", "MenuCourseId", "ScheduleId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, "Active", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 20, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, "Active", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 15, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 3, "Active", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourse_Schedules_MenuCourseId",
                table: "MenuCourse_Schedules",
                column: "MenuCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourse_Schedules_ScheduleId",
                table: "MenuCourse_Schedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_CategoryId",
                table: "MenuCourses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_Name",
                table: "MenuCourses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCourses_Price",
                table: "MenuCourses",
                column: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuCourse_Schedules");

            migrationBuilder.DropTable(
                name: "MenuCourses");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
