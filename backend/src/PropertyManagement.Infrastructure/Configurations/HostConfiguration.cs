using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Configurations;

public class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder.ToTable("Hosts");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.FullName)
            .IsRequired();

        builder.Property(h => h.Email)
            .IsRequired();

        builder.Property(h => h.Phone)
            .IsRequired();

        builder.HasMany(h => h.Properties)
            .WithOne(p => p.Host)
            .HasForeignKey(p => p.HostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
