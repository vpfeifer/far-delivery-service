using System;
using DeliveryService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Data
{
    public class DeliveryContext : DbContext
    {
        public DeliveryContext(DbContextOptions<DeliveryContext> options)
            : base(options)
        {

        }

        public DbSet<Point> Points { get; set; }
        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Point>()
                .HasMany(p => p.Routes)
                .WithOne()
                .HasForeignKey(r => r.FromId);
        }
    }
}
