using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatatoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 4, 30, 17, 16, 38, 770, DateTimeKind.Local).AddTicks(7229), new DateTime(2026, 4, 30, 17, 16, 38, 771, DateTimeKind.Local).AddTicks(9957) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(693), new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(697) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(700), new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(701) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(705), new DateTime(2026, 4, 30, 17, 16, 38, 772, DateTimeKind.Local).AddTicks(706) });
        }
    }
}
