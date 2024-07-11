using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodRestaurantApp_BE.Migrations
{
    /// <inheritdoc />
    public partial class SeedingPermissionsAndRolesPermissionsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 9, 54, 704, DateTimeKind.Local).AddTicks(7489),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 8, 18, 15, 20, 86, DateTimeKind.Local).AddTicks(272));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "ADM001", "Xem danh sách đơn hàng" },
                    { "ADM002", "Duyệt/hủy đơn hàng mới" },
                    { "ADM003", "In hóa đơn đơn hàng" },
                    { "ADM004", "Xem danh sách món ăn" },
                    { "ADM005", "Thêm/sửa/xóa món ăn" },
                    { "ADM006", "Xem danh sách đánh giá" },
                    { "ADM007", "Duyệt/Ẩn đánh giá" },
                    { "ADM008", "Quản lý nhân viên" },
                    { "ADM009", "Quản lý người dùng" },
                    { "ADM010", "Quản lý vai trò" },
                    { "ADM011", "Quản lý quyền" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password", "uuid" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 9, 54, 921, DateTimeKind.Local).AddTicks(6621), "$2a$11$hW81/Pir1bX7GefAtK5J4u5kljbY3/ySMD3OtBin3sjh5nBUP5vVi", "43c4a82d-7923-44f5-b8fd-d623a9ff6e8e" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "ADM001", 1 },
                    { "ADM002", 1 },
                    { "ADM003", 1 },
                    { "ADM004", 1 },
                    { "ADM005", 1 },
                    { "ADM006", 1 },
                    { "ADM007", 1 },
                    { "ADM008", 1 },
                    { "ADM009", 1 },
                    { "ADM010", 1 },
                    { "ADM011", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM001", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM002", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM003", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM004", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM005", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM006", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM007", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM008", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM009", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM010", 1 });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "ADM011", 1 });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM001");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM002");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM003");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM004");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM005");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM006");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM007");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM008");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM009");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM010");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: "ADM011");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 8, 18, 15, 20, 86, DateTimeKind.Local).AddTicks(272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 9, 54, 704, DateTimeKind.Local).AddTicks(7489));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "uid",
                keyValue: 1,
                columns: new[] { "created_date", "password", "uuid" },
                values: new object[] { new DateTime(2024, 7, 8, 18, 15, 20, 381, DateTimeKind.Local).AddTicks(3282), "$2a$11$cKdsDbdBVz19WTgIWZXA7...gjkORL40gsS/4fN574sko5wIa3Y3u", "a6d8a16f-db42-4afa-a716-b9f3511c5b0e" });
        }
    }
}
