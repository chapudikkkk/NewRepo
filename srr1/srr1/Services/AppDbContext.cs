using srr1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace srr1.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<ParkingZoneEntity> ParkingZones => Set<ParkingZoneEntity>();
        public DbSet<ParkingSessionEntity> ParkingSessions => Set<ParkingSessionEntity>();
        public DbSet<PaymentEntity> Payments => Set<PaymentEntity>();
        public DbSet<PenaltyEntity> Penalties => Set<PenaltyEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkingSessionEntity>()
                .HasOne(s => s.ParkingZone)
                .WithMany(z => z.ParkingSessions)
                .HasForeignKey(s => s.ParkingZoneId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(p => p.ParkingSession)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.ParkingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PenaltyEntity>()
                .HasOne(p => p.ParkingSession)
                .WithMany(s => s.Penalties)
                .HasForeignKey(p => p.ParkingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ParkingZoneEntity>()
                .HasIndex(z => z.ParkingZoneId)
                .IsUnique();

            modelBuilder.Entity<ParkingSessionEntity>()
                .HasIndex(s => s.SessionId)
                .IsUnique();

            modelBuilder.Entity<PaymentEntity>()
                .HasIndex(p => p.PaymentId)
                .IsUnique();

            modelBuilder.Entity<PenaltyEntity>()
                .HasIndex(p => p.PenaltyId)
                .IsUnique();
        }
    }
}