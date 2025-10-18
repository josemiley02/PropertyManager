using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PropertyManagement.Domain.Entities.Identity;

namespace PropertyManagement.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, long>
{
    private readonly IDateTimeService _dateTime;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDateTimeService dateTime,
        ILoggerFactory loggerFactory) : base(options)
    {
        _dateTime = dateTime;
        _loggerFactory = loggerFactory;
    }

    // Constructor protegido para testing
    protected ApplicationDbContext()
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.NowUtc;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.NowUtc;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = _dateTime.NowUtc;
                    entry.Entity.StatusBaseEntity = Domain.Enums.StatusBaseEntity.Active;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = _dateTime.NowUtc;
                    break;
            }
        }
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Ignore<IdentityUserToken<long>>();
        builder.Ignore<IdentityUserLogin<long>>();

        builder.Entity<AppUser>().ToTable("AppUsers");
        builder.Entity<AppRole>().ToTable("AppRoles");
        builder.Entity<IdentityUserRole<long>>().ToTable("AppUserRole");
        builder.Entity<IdentityUserClaim<long>>();
        builder.Entity<IdentityRoleClaim<long>>();
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}