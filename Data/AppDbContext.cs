using Microsoft.EntityFrameworkCore;
using BackendPractice.Models;

namespace BackendPractice.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Meter> Meters => Set<Meter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Zone configuration
        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(z => z.Id);
            entity.HasIndex(z => z.Code).IsUnique();
            entity.Property(z => z.Name).IsRequired().HasMaxLength(100);
            entity.Property(z => z.Code).IsRequired().HasMaxLength(50);
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);

            // User -> Zone (One-to-Many: Zone has many Users)
            entity.HasOne(u => u.Zone)
                  .WithMany(z => z.Users)
                  .HasForeignKey(u => u.ZoneId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Meter configuration
        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.SerialNumber).IsUnique();
            entity.Property(m => m.SerialNumber).IsRequired().HasMaxLength(100);
            entity.Property(m => m.MeterType).HasMaxLength(50);
            entity.Property(m => m.Status).HasMaxLength(50);

            // Meter -> Zone (One-to-Many: Zone has many Meters)
            entity.HasOne(m => m.Zone)
                  .WithMany(z => z.Meters)
                  .HasForeignKey(m => m.ZoneId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Meter -> User (One-to-Many: User has many Meters)
            entity.HasOne(m => m.User)
                  .WithMany(u => u.Meters)
                  .HasForeignKey(m => m.UserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
