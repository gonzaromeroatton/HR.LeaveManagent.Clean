using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class testmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 25, 12, 3, 50, 756, DateTimeKind.Local).AddTicks(18), new DateTime(2025, 11, 25, 12, 3, 50, 756, DateTimeKind.Local).AddTicks(79) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 13, 17, 22, 14, 988, DateTimeKind.Local).AddTicks(7776), new DateTime(2025, 11, 13, 17, 22, 14, 988, DateTimeKind.Local).AddTicks(7826) });
        }
    }
}
