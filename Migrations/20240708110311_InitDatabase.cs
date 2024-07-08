using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodRestaurantApp_BE.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    editable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Toppings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toppings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    max_toppings = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    slug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    food_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.id);
                    table.ForeignKey(
                        name: "FK_Foods_FoodTypes_food_type",
                        column: x => x.food_type,
                        principalTable: "FoodTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false),
                    permission_id = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 8, 18, 3, 11, 248, DateTimeKind.Local).AddTicks(7923)),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    is_admin = table.Column<bool>(type: "bit", nullable: false),
                    role_id = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.uid);
                    table.ForeignKey(
                        name: "FK_Users_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemOrders",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    recipient_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    recipient_address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    recipient_phone_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    placed_by = table.Column<int>(type: "int", nullable: false),
                    total_price = table.Column<int>(type: "int", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    cancel_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK_SystemOrders_Users_approved_by",
                        column: x => x.approved_by,
                        principalTable: "Users",
                        principalColumn: "uid");
                    table.ForeignKey(
                        name: "FK_SystemOrders_Users_placed_by",
                        column: x => x.placed_by,
                        principalTable: "Users",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemOrderLines",
                columns: table => new
                {
                    system_order_id = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    toppings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    base_cost = table.Column<int>(type: "int", nullable: false),
                    additional_cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOrderLines", x => new { x.system_order_id, x.food_id });
                    table.ForeignKey(
                        name: "FK_SystemOrderLines_Foods_food_id",
                        column: x => x.food_id,
                        principalTable: "Foods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemOrderLines_SystemOrders_system_order_id",
                        column: x => x.system_order_id,
                        principalTable: "SystemOrders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Quản trị hệ thống", "ADMIN" },
                    { 2, "Nhân viên", "NVIEN" },
                    { 3, "Khách hàng mua sắm", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "uid", "avatar", "created_date", "email", "full_name", "is_active", "is_admin", "last_login", "name", "password", "role_id", "uuid" },
                values: new object[] { 1, null, new DateTime(2024, 7, 8, 18, 3, 11, 439, DateTimeKind.Local).AddTicks(6732), "admin@gmail.com", "Quản trị hệ thống", true, true, null, "admin", "$2a$11$1XbctSqrLxhhFXj9RFfkHOAvAeIgIo.2lIwAW4b3tqUqg.MS98yLy", 1, "3e1e1155-3617-4b7b-9044-a79ff6bce168" });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_food_type",
                table: "Foods",
                column: "food_type");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_permission_id",
                table: "RolesPermissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOrderLines_food_id",
                table: "SystemOrderLines",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOrders_approved_by",
                table: "SystemOrders",
                column: "approved_by");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOrders_placed_by",
                table: "SystemOrders",
                column: "placed_by");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "SystemOrderLines");

            migrationBuilder.DropTable(
                name: "Toppings");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "SystemOrders");

            migrationBuilder.DropTable(
                name: "FoodTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
