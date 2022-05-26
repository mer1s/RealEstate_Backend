using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ad>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );
        //  modelBuilder.Entity<Ad>()
        //      .HasOne(p => p.AppUser)
        //      .WithMany(b => b.MyAds)
        //      .OnDelete(DeleteBehavior.Cascade)
        //      .IsRequired()
        //      .HasForeignKey(p => p.AppUserId);
        
            /*  modelBuilder
                .Entity<Ad>()
                .HasMany(e => e.Images)
                .WithOne()
                .OnDelete(DeleteBehavior.ClientCascade); Cascade delete vec radi 
            */
        }
    }
}
