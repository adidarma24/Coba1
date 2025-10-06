using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_MyClass_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCourseId",
                table: "MyClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MSId",
                table: "MyClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_MSId",
                table: "MyClasses",
                column: "MSId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_MenuCourseSchedules_MSId",
                table: "MyClasses",
                column: "MSId",
                principalTable: "MenuCourseSchedules",
                principalColumn: "MSId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses",
                column: "MenuCourseId",
                principalTable: "MenuCourses",
                principalColumn: "MenuCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_MenuCourseSchedules_MSId",
                table: "MyClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses");

            migrationBuilder.DropIndex(
                name: "IX_MyClasses_MSId",
                table: "MyClasses");

            migrationBuilder.DropColumn(
                name: "MSId",
                table: "MyClasses");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCourseId",
                table: "MyClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MyClasses_MenuCourses_MenuCourseId",
                table: "MyClasses",
                column: "MenuCourseId",
                principalTable: "MenuCourses",
                principalColumn: "MenuCourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
