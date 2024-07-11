using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodRestaurantApp_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodImageColumnAndDefaultPermissionsForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 12, 1, 16, 46, 923, DateTimeKind.Local).AddTicks(7945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 9, 54, 704, DateTimeKind.Local).AddTicks(7489));

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "ADM001", 2 },
                    { "ADM002", 2 },
                    { "ADM003", 2 },
                    { "ADM004", 2 },
                    { "ADM006", 2 },
                    { "ADM007", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password", "uuid" },
                values: new object[] { new DateTime(2024, 7, 12, 1, 16, 47, 232, DateTimeKind.Local).AddTicks(9689), "$2a$11$VlHV6iXHayBOhkmLM0gy/eMVt6yltcekVTjepoYEnReDI2AME0J3e", "f3264c05-9963-4e17-9dba-67ce3d2f1924" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM001", 2 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM002", 2 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM003", 2 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM004", 2 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM006", 2 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM007", 2 });

            migrationBuilder.DropColumn(
                name: "image",
                table: "Foods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 9, 54, 704, DateTimeKind.Local).AddTicks(7489),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 12, 1, 16, 46, 923, DateTimeKind.Local).AddTicks(7945));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password", "uuid" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 9, 54, 921, DateTimeKind.Local).AddTicks(6621), "$2a$11$hW81/Pir1bX7GefAtK5J4u5kljbY3/ySMD3OtBin3sjh5nBUP5vVi", "43c4a82d-7923-44f5-b8fd-d623a9ff6e8e" });
        }
    }
}
