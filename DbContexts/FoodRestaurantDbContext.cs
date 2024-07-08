using Microsoft.EntityFrameworkCore;
using FoodRestaurantApp_BE.Models.Databases;

namespace FoodRestaurantApp_BE.DbContexts { 
    public class FoodRestaurantDbContext(DbContextOptions<FoodRestaurantDbContext> options): DbContext(options) {
        /// <summary>
        /// Dataset Sản phẩm
        /// </summary>
        public DbSet<Food> Foods { get; set; }
        /// <summary>
        /// Dataset Người dùng
        /// </summary>
        public DbSet<SystemUser> Users { get; set; }

        /// <summary>
        /// Dataset Chi tiết hóa đơn của hệ thống
        /// </summary>
        public DbSet<SystemOrderLine> SystemOrderLines { get; set; }

        /// <summary>
        /// Dataset Hóa đơn của hệ thống
        /// </summary>
        public DbSet<SystemOrder> SystemOrders { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public DbSet<FoodType> FoodTypes { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        
        public DbSet<RolePermission> RolesPermissions { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Mapping FoodVariantDto với table Var trong Database
            modelBuilder.Entity<FoodType>(e => {
                e.ToTable("FoodTypes");
                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
                e.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(100).IsRequired();
            });

            // Mapping FoodDto với table Product trong Database
            modelBuilder.Entity<Food>(e => {
                e.ToTable("Foods");

                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(e => e.Price).HasColumnName("price").IsRequired();
                e.Property(e => e.Description).HasColumnName("description").HasMaxLength(100).IsRequired();
                e.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(250).IsRequired();
                e.Property(e => e.TypeId).HasColumnName("food_type").IsRequired();
                e.Property(e => e.MaxToppings).HasColumnName("max_toppings").HasDefaultValue(0);
            });

            modelBuilder.Entity<Food>()
                        .HasOne(e => e.Type)
                        .WithMany(e => e.Foods)
                        .HasForeignKey(e => e.TypeId);

            // Mapping FoodVariantDto với table Var trong Database
            modelBuilder.Entity<Topping>(e => {
                e.ToTable("Toppings");

                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(e => e.Price).HasColumnName("price").IsRequired();
            });

            // Mapping Model Users với table Users trong database
            modelBuilder.Entity<SystemUser>(e => {
                e.ToTable("Users");

                e.HasKey(col => col.Id);

                e.Property(x => x.Id).HasColumnName("uid").UseIdentityColumn(1, 1);
                e.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
                e.Property(x => x.FullName).HasColumnName("full_name").IsRequired().HasMaxLength(150);
                e.Property(x => x.Password).HasColumnName("password").IsRequired().HasMaxLength(100);
                e.Property(x => x.Email).HasColumnName("email").IsRequired().HasMaxLength(150);
                e.Property(x => x.RoleId).HasColumnName("role_id").IsRequired().HasMaxLength(10);
                e.Property(x => x.CreatedDate).HasColumnName("created_date").HasDefaultValue(DateTime.Now);
                e.Property(x => x.IsActive).HasColumnName("is_active").IsRequired().HasDefaultValue(true);
                e.Property(x => x.LastLogin).HasColumnName("last_login");
                e.Property(x => x.Uuid).HasColumnName("uuid").IsRequired();
                e.Property(x => x.Avatar).HasColumnName("avatar").IsRequired(false);
                e.Property(x => x.IsAdmin).HasColumnName("is_admin").IsRequired(true);
            });

            modelBuilder.Entity<SystemUser>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<SystemUser>().HasIndex(e => e.Email).IsUnique();

            // Mapping Model Roles với table Roles trong database
            modelBuilder.Entity<Role>(e => {
                e.ToTable("Roles");

                e.HasKey(col => col.Id);

                e.Property(x => x.Id).HasColumnName("id").IsRequired();
                e.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(10);
                e.Property(x => x.Description).HasColumnName("description").IsRequired().HasMaxLength(50);
                e.Property(x => x.Editable).HasColumnName("editable").IsRequired();
            });

            modelBuilder.Entity<SystemUser>()
                        .HasOne(e => e.Role)
                        .WithMany(e => e.SystemUsers)
                        .HasForeignKey(e => e.RoleId);

            // Mapping Model SystemOrderLine với table SystemOrderLine trong database
            modelBuilder.Entity<SystemOrderLine>(e => {
                e.ToTable("SystemOrderLines");

                e.HasKey(col => new { col.SystemOrderId, col.FoodId });

                e.Property(x => x.SystemOrderId).HasColumnName("system_order_id").HasMaxLength(30).IsRequired();
                e.Property(x => x.FoodId).HasColumnName("food_id").IsRequired();
                e.Property(x => x.Quantity).HasColumnName("quantity").IsRequired();
                e.Property(x => x.Toppings).HasColumnName("toppings");
                e.Property(x => x.AdditionalCost).HasColumnName("additional_cost").IsRequired();
                e.Property(x => x.BaseCost).HasColumnName("base_cost").IsRequired();
            });

            modelBuilder.Entity<SystemOrder>()
                        .HasMany(e => e.Foods)
                        .WithMany(e => e.SystemOrders)
                        .UsingEntity<SystemOrderLine>(
                            l => l.HasOne(e => e.Food).WithMany().HasForeignKey(e => e.FoodId),
                            r => r.HasOne(e => e.SystemOrder).WithMany().HasForeignKey(e => e.SystemOrderId)
                        );

            // Mapping Model SystemOrder với table SystemOrder trong database
            modelBuilder.Entity<SystemOrder>(e => {
                e.ToTable("SystemOrders");

                e.HasKey(col => col.Id);

                e.Property(x => x.Id).HasColumnName("id").HasMaxLength(30).IsRequired();
                e.Property(x => x.RecipientName).HasColumnName("recipient_name").HasMaxLength(50).IsRequired();
                e.Property(x => x.RecipientAddress).HasColumnName("recipient_address").HasMaxLength(100).IsRequired();
                e.Property(x => x.RecipientPhoneNumber).HasColumnName("recipient_phone_number").HasMaxLength(10).IsRequired();
                e.Property(x => x.PaymentMethod).HasColumnName("payment_method").IsRequired();
                e.Property(x => x.Note).HasColumnName("note").IsRequired(false);
                e.Property(x => x.TotalPrice).HasColumnName("total_price").IsRequired();
                e.Property(x => x.Paid).HasColumnName("paid").IsRequired().HasDefaultValue(false);
                e.Property(x => x.ApprovedBy).HasColumnName("approved_by").IsRequired(false);
                e.Property(x => x.CancelReason).HasColumnName("cancel_reason").IsRequired(false);
                e.Property(x => x.PlacedBy).HasColumnName("placed_by").IsRequired();
                e.Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();
                e.Property(x => x.Status).HasColumnName("status").IsRequired();
            });

            modelBuilder.Entity<SystemOrder>()
                        .HasOne(e => e.UserApproved)
                        .WithMany()
                        .HasForeignKey(e => e.ApprovedBy);
            modelBuilder.Entity<SystemOrder>()
                        .HasOne(e => e.UserPlaced)
                        .WithMany(e => e.SystemOrders)
                        .HasForeignKey(e => e.PlacedBy);

            modelBuilder.Entity<Permission>(e => {
                e.ToTable("Permissions");

                e.HasKey(col => col.Id);

                e.Property(x => x.Id).HasColumnName("id").IsRequired().HasMaxLength(6);
                e.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(e => {
                e.ToTable("RolesPermissions");

                e.HasKey(col => new { col.RoleId, col.PermissionId });

                e.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
                e.Property(x => x.PermissionId).HasColumnName("permission_id").IsRequired().HasMaxLength(6);
            });

            modelBuilder.Entity<Role>()
                        .HasMany(e => e.Permissions)
                        .WithMany(e => e.Roles)
                        .UsingEntity<RolePermission>(
                            l => l.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId),
                            r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId)
                        );

            // Seeding data
            modelBuilder.Entity<Role>().HasData([
                new Role() {
                    Id = (int) Constants.Roles.ADMIN, 
                    Name = nameof(Constants.Roles.ADMIN), 
                    Description = "Quản trị hệ thống", 
                    Editable = false 
                },
                new Role()
                {
                    Id = (int) Constants.Roles.NVIEN,
                    Name = nameof(Constants.Roles.NVIEN),
                    Description = "Nhân viên",
                    Editable = false
                },
                new Role()
                {
                    Id = (int)Constants.Roles.KHHANG,
                    Name = nameof(Constants.Roles.ADMIN),
                    Description = "Khách hàng mua sắm",
                    Editable = false
                },
            ]);

            modelBuilder.Entity<SystemUser>().HasData([
                new SystemUser
                {
                    Id = 1,
                    Name = "admin",
                    Uuid = Guid.NewGuid().ToString(),
                    FullName = "Quản trị hệ thống",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsAdmin = true,
                    RoleId = (int) Constants.Roles.ADMIN
                }
            ]);
        }
    }
}
