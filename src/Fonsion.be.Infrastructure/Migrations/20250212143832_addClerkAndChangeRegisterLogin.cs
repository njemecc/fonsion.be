using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fonsion.be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addClerkAndChangeRegisterLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e2b75bc-e540-45e7-964c-fd4362618054");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "850c29e5-5d6e-4a5d-b22d-fcd9838ea7cb");

            migrationBuilder.AddColumn<string>(
                name: "ClerkId",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41ca6711-b8f0-492d-897b-15f05db114a8", null, "Guest", "GUEST" },
                    { "adc1dab1-2ef1-4acc-a84c-06a8d0565a2c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41ca6711-b8f0-492d-897b-15f05db114a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adc1dab1-2ef1-4acc-a84c-06a8d0565a2c");

            migrationBuilder.DropColumn(
                name: "ClerkId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e2b75bc-e540-45e7-964c-fd4362618054", null, "Guest", "GUEST" },
                    { "850c29e5-5d6e-4a5d-b22d-fcd9838ea7cb", null, "Admin", "ADMIN" }
                });
        }
    }
}
