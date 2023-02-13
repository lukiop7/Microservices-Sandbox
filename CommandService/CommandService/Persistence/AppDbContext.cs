using CommandService.Models;
using CommandService.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<Platform> Platforms { get; set; } = default!;
    public DbSet<Command> Commands { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlatformTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}