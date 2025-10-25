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

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PropertyManagementDb;User Id= SA;Password=Josemiley_02;Encrypt=True;TrustServerCertificate=True;");

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var dateTimeService = new DateTimeService();

            return new ApplicationDbContext(optionsBuilder.Options, dateTimeService, loggerFactory);
        }
    }
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc { get => DateTime.UtcNow; set => value = DateTime.UtcNow; }
    }
}
