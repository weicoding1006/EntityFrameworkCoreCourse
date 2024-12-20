using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class 自己寫sql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW vw_TeamsAndLeagues AS 
    SELECT t.Name, l.Name AS LeagueName 
    FROM Teams AS t 
    LEFT JOIN Leagues AS l ON t.LeagueId = l.Id");
            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 55, 21, 601, DateTimeKind.Unspecified).AddTicks(8501));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 55, 21, 601, DateTimeKind.Unspecified).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 55, 21, 601, DateTimeKind.Unspecified).AddTicks(8510));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_TeamsAndLeagues");
            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 51, 36, 103, DateTimeKind.Unspecified).AddTicks(811));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 51, 36, 103, DateTimeKind.Unspecified).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 20, 1, 51, 36, 103, DateTimeKind.Unspecified).AddTicks(839));
        }
    }
}
