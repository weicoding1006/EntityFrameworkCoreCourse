using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFrameworkCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class 增加初始資料 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4403), "測試隊伍1" },
                    { 2, new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4409), "測試隊伍2" },
                    { 3, new DateTime(2024, 12, 1, 13, 56, 57, 336, DateTimeKind.Unspecified).AddTicks(4410), "測試隊伍3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3);
        }
    }
}
