using FastEndpoints.Swagger;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.Extensions.Options;
using PropertyManagement.Api;
using PropertyManagement.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints(o =>
{
    o.SourceGeneratorDiscoveredTypes.AddRange(PropertyManagement.Api.DiscoveredTypes.All);
    o.SourceGeneratorDiscoveredTypes.AddRange(PropertyManagement.Application.DiscoveredTypes.All);
});

builder.Services.AddSwaggerGen();
builder.Services.AddCustomServicesExtensions();
builder.Services.AddDbContextExtension(builder.Configuration);
builder.Services.AddCorsExtension();
builder.Services.AddAuthorization();

builder.Services.SwaggerDocument(o =>
{
    o.MinEndpointVersion = 1;
    o.MaxEndpointVersion = 1;
    o.AutoTagPathSegmentIndex = 0;
});

// builder.Logging.AddApplicationInsights(
//     configureTelemetryConfiguration: (config) => config.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"],
//     configureApplicationInsightsLoggerOptions: (options) => { }
// );
builder.Services.AddIdentityExtension();
builder.Services.AddGenericRepositoryExtension();
builder.Services.AddJWTAuthenticationExtension(builder.Configuration);
builder.Services.AddAuthorizationPoliciesExtension(builder.Configuration);
builder.Services.SetSettingsExtension(builder.Configuration);

builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

var app = builder.Build();
app.UseCors();
var hangFire = app.Services.GetService<IOptions<HangFireSettings>>()!.Value;

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] {
            new HangfireCustomBasicAuthenticationFilter { User = hangFire.Username, Pass = hangFire.Password }
        }
})
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c =>
    {
        c.Errors.UseProblemDetails();
        c.Versioning.Prefix = "v";
        c.Versioning.PrependToRoute = true;
        c.Versioning.DefaultVersion = 1;
    })
    .UseSwaggerGen()
    .ApplicationServices.ApplyMigrationsExtension();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Run();