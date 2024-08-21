using ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ShoppingCartAPI.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartDetails>()
                .HasOne(p => p.CartHeader)
                .WithMany(pd => pd.CartDetailsList)
                .HasForeignKey(p => p.CartHeaderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
