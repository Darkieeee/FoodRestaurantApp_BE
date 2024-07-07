using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRestaurantApp_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddUserUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 23, 40, 23, 333, DateTimeKind.Local).AddTicks(2036),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 1, 8, 22, 31, 661, DateTimeKind.Local).AddTicks(5059));

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uuid",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "avatar", "created_date", "password", "uuid" },
                values: new object[] { null, new DateTime(2024, 7, 7, 23, 40, 23, 611, DateTimeKind.Local).AddTicks(3385), "$2a$11$xcu1db8ssAUAH3xJ5H0z0OCzj1k8jNwE3sVOzT048j/fMgyqY9sLO", "25445b0c-f8fb-4fac-a807-71522a252f7b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "uuid",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 1, 8, 22, 31, 661, DateTimeKind.Local).AddTicks(5059),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 23, 40, 23, 333, DateTimeKind.Local).AddTicks(2036));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password" },
                values: new object[] { new DateTime(2024, 7, 1, 8, 22, 31, 960, DateTimeKind.Local).AddTicks(1433), "$2a$11$iPOmu3o8qi24C6pLCpjKGe7dzZI/sTB6rSM1uG2ID4DiKPQD9fMGG" });
        }
    }
}
