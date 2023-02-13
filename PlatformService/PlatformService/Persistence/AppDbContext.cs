using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using PlatformService.Persistence.Configurations;

namespace PlatformService.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<Platform> Platforms { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlatformTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}