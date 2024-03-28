using Microsoft.EntityFrameworkCore;
using FoodRestaurantApp_BE.Models.Dto;

namespace FoodRestaurantApp_BE.Contexts { 
    public class FoodRestaurantDbContext(DbContextOptions<FoodRestaurantDbContext> options): DbContext(options) {
        /// <summary>
        /// Dataset Sản phẩm
        /// </summary>
        public DbSet<FoodDto> Foods { get; set; }
        /// <summary>
        /// Dataset Người dùng
        /// </summary>
        public DbSet<UserDto> Users { get; set; }

        /// <summary>
        /// Dataset Chi tiết hóa đơn của hệ thống
        /// </summary>
        //public DbSet<SystemOrderLineDto> SystemOrderLines { get; set; }

        /// <summary>
        /// Dataset Hóa đơn của hệ thống
        /// </summary>
        //public DbSet<SystemOrderDto> SystemOrders { get; set; }

        public DbSet<FoodVariantDto> FoodVariants { get; set; }

        public DbSet<FoodTypeDto> FoodTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Mapping FoodVariantDto với table Var trong Database
            modelBuilder.Entity<FoodTypeDto>(e => {
                e.ToTable("FoodTypes");
                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
                e.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(100).IsRequired();
            });

            // Mapping FoodDto với table Product trong Database
            modelBuilder.Entity<FoodDto>(e => {
                e.ToTable("Foods");

                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(e => e.Price).HasColumnName("price").IsRequired();
                e.Property(e => e.Description).HasColumnName("description").HasMaxLength(100).IsRequired();
                e.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(250).IsRequired();
                e.Property(e => e.FoodType).HasColumnName("food_type").IsRequired();
            });

            // Mapping FoodVariantDto với table Var trong Database
            modelBuilder.Entity<FoodVariantDto>(e => {
                e.ToTable("FoodVariants");

                e.HasKey(col => col.Id);

                e.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(1, 1);
                e.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(e => e.FoodId).HasColumnName("food_id").IsRequired();
            });

            // Mapping Model Users với table Users trong database
            modelBuilder.Entity<UserDto>(e => {
                e.ToTable("Users");

                e.HasKey(col => col.Uid);

                e.Property(x => x.Uid).HasColumnName("uid").IsRequired().HasMaxLength(50);
                e.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
                e.Property(x => x.Email).HasColumnName("email").IsRequired().HasMaxLength(50);
                e.Property(x => x.Password).HasColumnName("password").IsRequired().HasMaxLength(50);
                e.Property(x => x.CreatedDate).HasColumnName("created_date").HasDefaultValue(DateTime.Now);
                e.Property(x => x.IsActive).HasColumnName("is_active").IsRequired().HasDefaultValue(true);
                e.Property(x => x.LastLogin).HasColumnName("last_login");
            });

            

            // Mapping Model SystemOrderLine với table SystemOrderLine trong database
            //modelBuilder.Entity<SystemOrderLineDto>(e => {

            //});

            // Mapping Model SystemOrder với table SystemOrder trong database
            //modelBuilder.Entity<SystemOrderDto>(e => { 
            
            //});
        }
    }
}
