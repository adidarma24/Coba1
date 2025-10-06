using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_InvoiceMenuCourse_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceMenuCourses_MenuCourses_MenuCourseId",
                table: "InvoiceMenuCourses");

            migrationBuilder.RenameColumn(
                name: "MenuCourseId",
                table: "InvoiceMenuCourses",
                newName: "MSId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceMenuCourses_MenuCourseId",
                table: "InvoiceMenuCourses",
                newName: "IX_InvoiceMenuCourses_MSId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceMenuCourses_MenuCourseSchedules_MSId",
                table: "InvoiceMenuCourses",
                column: "MSId",
                principalTable: "MenuCourseSchedules",
                principalColumn: "MSId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceMenuCourses_MenuCourseSchedules_MSId",
                table: "InvoiceMenuCourses");

            migrationBuilder.RenameColumn(
                name: "MSId",
                table: "InvoiceMenuCourses",
                newName: "MenuCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceMenuCourses_MSId",
                table: "InvoiceMenuCourses",
                newName: "IX_InvoiceMenuCourses_MenuCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceMenuCourses_MenuCourses_MenuCourseId",
                table: "InvoiceMenuCourses",
                column: "MenuCourseId",
                principalTable: "MenuCourses",
                principalColumn: "MenuCourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
