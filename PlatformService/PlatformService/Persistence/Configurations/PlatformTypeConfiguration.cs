using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformService.Models;

namespace PlatformService.Persistence.Configurations;

public class PlatformTypeConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.ToTable("Platforms");

        builder.Property(x => x.Id).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Cost).IsRequired();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Publisher).IsRequired();
    }
}