using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MenuCourseSchedules",
                newName: "MSId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MenuCourses",
                newName: "MenuCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MSId",
                table: "MenuCourseSchedules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MenuCourseId",
                table: "MenuCourses",
                newName: "Id");
        }
    }
}
