using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRestaurantApp_BE.Migrations
{
    /// <inheritdoc />
    public partial class AlterSomeUserColumnsToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 1, 8, 22, 31, 661, DateTimeKind.Local).AddTicks(5059),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 15, 14, 45, 47, 349, DateTimeKind.Local).AddTicks(9516));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password" },
                values: new object[] { new DateTime(2024, 7, 1, 8, 22, 31, 960, DateTimeKind.Local).AddTicks(1433), "$2a$11$iPOmu3o8qi24C6pLCpjKGe7dzZI/sTB6rSM1uG2ID4DiKPQD9fMGG" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_name",
                table: "Users",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_name",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 15, 14, 45, 47, 349, DateTimeKind.Local).AddTicks(9516),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 1, 8, 22, 31, 661, DateTimeKind.Local).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password" },
                values: new object[] { new DateTime(2024, 5, 15, 14, 45, 47, 524, DateTimeKind.Local).AddTicks(5613), "$2a$11$g9FCoWk0uYvBqwChw3QuZehUx2Cxn6HdZYmqti7/TsUhjRr.HxMT." });
        }
    }
}
