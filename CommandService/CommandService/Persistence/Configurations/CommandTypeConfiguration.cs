using CommandService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandService.Persistence.Configurations;

public class CommandTypeConfiguration : IEntityTypeConfiguration<Command>
{
    public void Configure(EntityTypeBuilder<Command> builder)
    {
        builder
            .ToTable("Commands");

        builder
            .Property(x => x.Id)
            .IsRequired();

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.HowTo)
            .IsRequired();

        builder
            .Property(x => x.CommandLine)
            .IsRequired();

        builder
            .Property(x => x.PlatformId)
            .IsRequired();

        builder
            .HasOne(x => x.Platform)
            .WithMany(x => x.Commands)
            .HasForeignKey(x => x.PlatformId);
    }
}