using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fonsion.be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addReservationsToRoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1af4d67d-bae2-4841-88f0-ed786d99f6d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8398bf8-afd7-4629-811a-638ee69e31a6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ee8f3a27-289c-4821-9a71-5f58a566df67", null, "Guest", "GUEST" },
                    { "fbefa7f0-eac9-4073-8588-815b8ab3c19a", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8f3a27-289c-4821-9a71-5f58a566df67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbefa7f0-eac9-4073-8588-815b8ab3c19a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1af4d67d-bae2-4841-88f0-ed786d99f6d3", null, "Admin", "ADMIN" },
                    { "c8398bf8-afd7-4629-811a-638ee69e31a6", null, "Guest", "GUEST" }
                });
        }
    }
}
