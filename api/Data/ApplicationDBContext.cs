using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // one to one (category, product)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            
            // one to many (user, order)
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.AppUser)
                .HasForeignKey(o => o.AppUserId);

            // many to many (order, product)
            modelBuilder.Entity<OrderProduct>(x => x.HasKey(p => new { p.OrderId, p.ProductId }));

            modelBuilder.Entity<OrderProduct>()
                .HasOne(u => u.Order)
                .WithMany(u => u.OrderProducts)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(u => u.Product)
                .WithMany(u => u.OrderProducts)
                .HasForeignKey(p => p.ProductId);

            // default category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Men Clothes"},
                new Category { Id = 2, Name = "Men Trousers"},
                new Category { Id = 3, Name = "Men Shoes"},
                new Category { Id = 4, Name = "Women Clothes"},
                new Category { Id = 5, Name = "Women Trousers"},
                new Category { Id = 6, Name = "Women Shoes"},
                new Category { Id = 7, Name = "Watches & Glasses"},
                new Category { Id = 8, Name = "Health & Wellness"},
                new Category { Id = 9, Name = "Mobile & Gadgets"},
                new Category { Id = 10, Name = "Computers & Laptops"},
                new Category { Id = 11, Name = "Home Entertainment"},
                new Category { Id = 12, Name = "Cameras"},
                new Category { Id = 13, Name = "Electronics"},
                new Category { Id = 14, Name = "Home Appliances"},
                new Category { Id = 15, Name = "Beauty & Personal Care"},
                new Category { Id = 16, Name = "Sports & Outdoors"},
                new Category { Id = 17, Name = "Toys & Games"},
                new Category { Id = 18, Name = "Books & Stationery"},
                new Category { Id = 19, Name = "Food & Beverage"},
                new Category { Id = 20, Name = "Pets & Pet Supplies"},
                new Category { Id = 21, Name = "Fashion Accessories"}
            );

            // default role
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER"}
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}