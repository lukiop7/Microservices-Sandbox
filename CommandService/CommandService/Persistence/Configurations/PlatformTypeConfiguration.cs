using CommandService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandService.Persistence.Configurations;

public class PlatformTypeConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.ToTable("Platforms");

        builder.Property(x => x.Id).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.ExternalId).IsRequired();

        builder
            .HasMany(x => x.Commands)
            .WithOne(x => x.Platform)
            .HasForeignKey(x => x.PlatformId);
    }
}