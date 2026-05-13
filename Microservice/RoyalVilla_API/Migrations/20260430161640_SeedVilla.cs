using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoyalVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Villa",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "Color", "CreatedDate", "Details", "Name", "Occupancy", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Red", new DateTime(2026, 4, 30, 17, 16, 38, 770, DateTimeKind.Local).AddTicks(7229), "This is the Royal Villa with stuning ocean view", "Royal Villa", 6, 200m, new DateTime(2026, 4, 30, 17, 16, 38, 771, DateTimeKind.Local).AddTicks(9957) },
                    { 2, "Black", new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(693), "This is the Royal Villa with pool and bar view", "Royal Villa", 6, 200m, new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(697) },
                    { 3, "White", new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(700), "This is the Royal Villa with pool and bar view", "Royal Villa", 6, 200m, new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(701) },
                    { 4, "Green", new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(705), "This is the Royal Villa with football feild and view", "Royal Villa", 6, 200m, new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(706) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Villa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
