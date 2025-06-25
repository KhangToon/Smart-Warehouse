using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Smart_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class Updatedefaultrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8ac1cbe3-b288-473f-a808-bad7b62c8fbb");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ff0c8fe5-24fc-4258-a86a-08c7caafaf45");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "056e20e7-3ed2-4690-bff2-a963b8dd94c2", null, "User", "USER" },
                    { "adbcd661-886b-4f9d-9a47-4a74cf73a721", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "056e20e7-3ed2-4690-bff2-a963b8dd94c2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "adbcd661-886b-4f9d-9a47-4a74cf73a721");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ac1cbe3-b288-473f-a808-bad7b62c8fbb", null, "client", "client" },
                    { "ff0c8fe5-24fc-4258-a86a-08c7caafaf45", null, "admin", "admin" }
                });
        }
    }
}
