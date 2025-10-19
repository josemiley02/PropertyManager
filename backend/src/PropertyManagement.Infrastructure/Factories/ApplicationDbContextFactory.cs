using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace PropertyManagement.Infrastructure.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PropertyManagementDb;User Id=property_user;Password=StrongPass123!;Encrypt=True;TrustServerCertificate=True;");

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var dateTimeService = new DateTimeServiceMock();

            return new ApplicationDbContext(optionsBuilder.Options, dateTimeService, loggerFactory);
        }

        private class DateTimeServiceMock : IDateTimeService
        {
            public DateTime NowUtc { get => DateTime.UtcNow; set => value = DateTime.UtcNow; }
        }
    }
}
