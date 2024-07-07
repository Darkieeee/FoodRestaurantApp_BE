﻿// <auto-generated />
using System;
using FoodRestaurantApp_BE.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodRestaurantApp_BE.Migrations
{
    [DbContext(typeof(FoodRestaurantDbContext))]
    partial class FoodRestaurantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("description");

                    b.Property<int>("MaxToppings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("max_toppings");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("slug");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("food_type");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Foods", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.FoodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("slug");

                    b.HasKey("Id");

                    b.ToTable("FoodTypes", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Quản trị hệ thống",
                            Name = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Nhân viên",
                            Name = "NVIEN"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Khách hàng mua sắm",
                            Name = "KHHANG"
                        });
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("PermissionId")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("permission_id");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolesPermissions", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemOrder", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("id");

                    b.Property<int?>("ApprovedBy")
                        .HasColumnType("int")
                        .HasColumnName("approved_by");

                    b.Property<string>("CancelReason")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cancel_reason");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("note");

                    b.Property<bool>("Paid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("paid");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("payment_method");

                    b.Property<int>("PlacedBy")
                        .HasColumnType("int")
                        .HasColumnName("placed_by");

                    b.Property<string>("RecipientAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("recipient_address");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("recipient_name");

                    b.Property<string>("RecipientPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("recipient_phone_number");

                    b.Property<short>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int")
                        .HasColumnName("total_price");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedBy");

                    b.HasIndex("PlacedBy");

                    b.ToTable("SystemOrders", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemOrderLine", b =>
                {
                    b.Property<string>("SystemOrderId")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("system_order_id");

                    b.Property<int>("FoodId")
                        .HasColumnType("int")
                        .HasColumnName("food_id");

                    b.Property<int>("AdditionalCost")
                        .HasColumnType("int")
                        .HasColumnName("additional_cost");

                    b.Property<int>("BaseCost")
                        .HasColumnType("int")
                        .HasColumnName("base_cost");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("Toppings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("toppings");

                    b.HasKey("SystemOrderId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("SystemOrderLines", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("uid");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 7, 7, 23, 40, 23, 333, DateTimeKind.Local).AddTicks(2036))
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("password");

                    b.Property<int>("RoleId")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 7, 7, 23, 40, 23, 611, DateTimeKind.Local).AddTicks(3385),
                            Email = "admin@gmail.com",
                            FullName = "Quản trị hệ thống",
                            IsActive = true,
                            Name = "admin",
                            Password = "$2a$11$xcu1db8ssAUAH3xJ5H0z0OCzj1k8jNwE3sVOzT048j/fMgyqY9sLO",
                            RoleId = 1,
                            Uuid = "25445b0c-f8fb-4fac-a807-71522a252f7b"
                        });
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Topping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("Toppings", (string)null);
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Food", b =>
                {
                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.FoodType", "Type")
                        .WithMany("Foods")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.RolePermission", b =>
                {
                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemOrder", b =>
                {
                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.SystemUser", "UserApproved")
                        .WithMany()
                        .HasForeignKey("ApprovedBy");

                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.SystemUser", "UserPlaced")
                        .WithMany("SystemOrders")
                        .HasForeignKey("PlacedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserApproved");

                    b.Navigation("UserPlaced");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemOrderLine", b =>
                {
                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.SystemOrder", "SystemOrder")
                        .WithMany()
                        .HasForeignKey("SystemOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("SystemOrder");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemUser", b =>
                {
                    b.HasOne("FoodRestaurantApp_BE.Models.Databases.Role", "Role")
                        .WithMany("SystemUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.FoodType", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.Role", b =>
                {
                    b.Navigation("SystemUsers");
                });

            modelBuilder.Entity("FoodRestaurantApp_BE.Models.Databases.SystemUser", b =>
                {
                    b.Navigation("SystemOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
