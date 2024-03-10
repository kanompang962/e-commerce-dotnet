using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0765a42-5966-4a22-8be6-3cabcecde99a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca149cca-7407-4162-8af0-5c5eb2d1221a");

            migrationBuilder.AddColumn<bool>(
                name: "Payment",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "098ed41f-d3a7-4a0f-8f79-2cd1084f52e0", null, "Admin", "ADMIN" },
                    { "f0bee065-c933-4de2-9666-077b3df4c9f2", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "098ed41f-d3a7-4a0f-8f79-2cd1084f52e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0bee065-c933-4de2-9666-077b3df4c9f2");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0765a42-5966-4a22-8be6-3cabcecde99a", null, "Admin", "ADMIN" },
                    { "ca149cca-7407-4162-8af0-5c5eb2d1221a", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
