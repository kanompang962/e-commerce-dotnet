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
                new Category { Id = 1, Name = "shirt" },
                new Category { Id = 2, Name = "trousers" }
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