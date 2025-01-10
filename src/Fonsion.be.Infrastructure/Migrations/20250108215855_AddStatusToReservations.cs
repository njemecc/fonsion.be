using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fonsion.be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee8f3a27-289c-4821-9a71-5f58a566df67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbefa7f0-eac9-4073-8588-815b8ab3c19a");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e2b75bc-e540-45e7-964c-fd4362618054", null, "Guest", "GUEST" },
                    { "850c29e5-5d6e-4a5d-b22d-fcd9838ea7cb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e2b75bc-e540-45e7-964c-fd4362618054");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "850c29e5-5d6e-4a5d-b22d-fcd9838ea7cb");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ee8f3a27-289c-4821-9a71-5f58a566df67", null, "Guest", "GUEST" },
                    { "fbefa7f0-eac9-4073-8588-815b8ab3c19a", null, "Admin", "ADMIN" }
                });
        }
    }
}
