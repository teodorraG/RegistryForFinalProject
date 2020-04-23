using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;

namespace RegistryForFinalProject.Contexts
{
    public class RegistryDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Registry> Registries { get; set; }
        public DbSet<RegistryItems> RegistryItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.AccountConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>(entity =>
            {

                entity.HasMany(x => x.ItemsSold)
                      .WithOne(x => x.Seller)
                      .HasForeignKey(x => x.SellerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.OrdersBought)
                      .WithOne(x => x.Buyer)
                      .HasForeignKey(x => x.BuyerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.ShoppingCarts)
                      .WithOne(x => x.Account)
                      .HasForeignKey(x => x.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.Registries)
                      .WithOne(x => x.Account)
                      .HasForeignKey(x => x.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.OrdersSold)
                      .WithOne(x => x.Seller)
                      .HasForeignKey(x => x.SellerId)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<ShoppingCart>()
            .Property("IsPurchased")
            .HasDefaultValue(false);

            modelBuilder.Entity<RegistryItems>(entity =>
            {
                entity.HasKey(x => new { x.RegistryId, x.ItemId });
            });
        }

    }
}
