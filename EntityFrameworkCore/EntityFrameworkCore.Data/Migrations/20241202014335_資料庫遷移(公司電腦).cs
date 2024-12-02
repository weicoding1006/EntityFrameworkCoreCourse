using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class 資料庫遷移公司電腦 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3667));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3675));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 1, 43, 35, 285, DateTimeKind.Unspecified).AddTicks(3676));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4409));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4410));
        }
    }
}
