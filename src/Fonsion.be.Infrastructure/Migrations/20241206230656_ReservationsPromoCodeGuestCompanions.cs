using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fonsion.be.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReservationsPromoCodeGuestCompanions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9855c8de-3230-466c-a1da-4ed30223a069");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99d410b6-09d9-4ac3-b7db-6be289be99ed");

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FromDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    GuestID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoomID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PromoCodeID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_PromoCodes_PromoCodeID",
                        column: x => x.PromoCodeID,
                        principalTable: "PromoCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GuestCompanions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReservationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestCompanions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestCompanions_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "147a8190-c972-4f71-8507-0525413713f3", null, "Guest", "GUEST" },
                    { "ba6efdde-d29e-46fe-9111-a3be9c058166", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestCompanions_ReservationId",
                table: "GuestCompanions",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PromoCodeID",
                table: "Reservations",
                column: "PromoCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomID",
                table: "Reservations",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestCompanions");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "147a8190-c972-4f71-8507-0525413713f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba6efdde-d29e-46fe-9111-a3be9c058166");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9855c8de-3230-466c-a1da-4ed30223a069", null, "Guest", "GUEST" },
                    { "99d410b6-09d9-4ac3-b7db-6be289be99ed", null, "Admin", "ADMIN" }
                });
        }
    }
}
