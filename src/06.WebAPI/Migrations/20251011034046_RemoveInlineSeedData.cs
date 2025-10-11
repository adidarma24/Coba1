using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInlineSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuCourseSchedules",
                keyColumn: "MSId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuCourseSchedules",
                keyColumn: "MSId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "PaymentMethodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
