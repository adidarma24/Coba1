using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_CreatedAt_UpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses");

            migrationBuilder.DropIndex(
                name: "IX_MyClasses_MenuCourseId",
                table: "MyClasses");

            migrationBuilder.DropColumn(
                name: "MenuCourseId",
                table: "MyClasses");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MenuCourses",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 1,
                column: "Price",
                value: 5.99m);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 2,
                column: "Price",
                value: 6.49m);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 3,
                column: "Price",
                value: 7.49m);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 4,
                column: "Price",
                value: 4.99m);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 5,
                column: "Price",
                value: 5.49m);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 6,
                column: "Price",
                value: 4.79m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuCourseId",
                table: "MyClasses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MenuCourses",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Invoices",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 1,
                column: "Price",
                value: 5.9900000000000002);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 2,
                column: "Price",
                value: 6.4900000000000002);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 3,
                column: "Price",
                value: 7.4900000000000002);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 4,
                column: "Price",
                value: 4.9900000000000002);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 5,
                column: "Price",
                value: 5.4900000000000002);

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 6,
                column: "Price",
                value: 4.79);

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_MenuCourseId",
                table: "MyClasses",
                column: "MenuCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses",
                column: "MenuCourseId",
                principalTable: "MenuCourses",
                principalColumn: "MenuCourseId");
        }
    }
}
