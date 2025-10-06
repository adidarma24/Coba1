using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Change_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 1,
                columns: new[] { "Description", "Image", "Name", "Price" },
                values: new object[] { "Tom Yum dari Thailand", "tomyum.jpg", "Tom Yum Thailand", 450000m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 2, "Minuman rasa strawberry", "strawberry_float.jpg", "Strawberry Float", 150000m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 3, "Chocholate Cookies", "cookies.jpg", "Chocholate Cookies", 200000m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 1, "Soto Banjar Limau Kuit", "greentea_cheesecake.jpg", "Soto Banjar Limau Kuit", 150000m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Green Tea Cheesecake", "Green Tea Cheesecake", 400000m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 6,
                columns: new[] { "Description", "Image", "Name", "Price" },
                values: new object[] { "Spagetti", "spagetti.jpg", "Italian Spagetti Bolognese", 450000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 1,
                columns: new[] { "Description", "Image", "Name", "Price" },
                values: new object[] { "Fresh tomato soup with basil.", "tomato_soup.jpg", "Tomato Soup", 5.99m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 1, "Creamy mushroom soup.", "mushroom_soup.jpg", "Mushroom Soup", 6.49m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 1, "Classic chicken noodle soup.", "chicken_soup.jpg", "Chicken Soup", 7.49m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[] { 2, "Rich dark chocolate cake.", "chocolate_cake.jpg", "Chocolate Cake", 4.99m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Creamy New York-style cheesecake.", "Cheesecake", 5.49m });

            migrationBuilder.UpdateData(
                table: "MenuCourses",
                keyColumn: "MenuCourseId",
                keyValue: 6,
                columns: new[] { "Description", "Image", "Name", "Price" },
                values: new object[] { "Tart filled with custard and fresh fruit.", "fruit_tart.jpg", "Fruit Tart", 4.79m });
        }
    }
}
