using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.HostId)
               .HasDatabaseName("IX_Property_HostId");

        builder.HasOne(p => p.Host)
               .WithMany(h => h.Properties)
               .HasForeignKey(p => p.HostId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Bookings)
               .WithOne(b => b.Property)
               .HasForeignKey(b => b.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);
               
        builder.HasMany(p => p.DomainEvents)
               .WithOne(de => de.Property)
               .HasForeignKey(de => de.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
