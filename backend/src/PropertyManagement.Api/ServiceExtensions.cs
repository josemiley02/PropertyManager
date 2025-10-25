using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using PropertyManagement.Application;
using PropertyManagement.Application.Abstractions;
using PropertyManagement.Application.Abstractions.Authentication;
using PropertyManagement.Application.Services;
using PropertyManagement.Application.Services.Authentication;
using PropertyManagement.Domain.Entities.Identity;
using PropertyManagement.Domain.Settings;
using PropertyManagement.Infrastructure;
using PropertyManagement.Infrastructure.Abstractions;
using PropertyManagement.Infrastructure.Concrete;
using PropertyManagement.Infrastructure.Factories;

namespace PropertyManagement.Api;

public static class ServiceExtensions
{
    public static void AddCustomServicesExtensions(this IServiceCollection services)
    {
        services.AddScoped<IUserValidator<AppUser>, CustomUserValidator<AppUser>>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICheckUniqueService, CheckUniqueService>();
        services.AddScoped<ITokenHandler, JwtHandler>();
        services.AddScoped<IDomainEventService, DomainEventService>();
    }
    public static void AddGenericRepositoryExtension(this IServiceCollection services)
    {
        services.TryAddScoped(typeof(PropertyManagement.Infrastructure.Abstractions.IGenericCoreRepository<>), typeof(GenericCoreRepository<>));
    }
public static void AddJWTAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
{
    // Leer la clave de seguridad desde TokenOptions:SecurityKey
    var securityKey = configuration["TokenOptions:SecurityKey"];
    if (string.IsNullOrWhiteSpace(securityKey))
        throw new InvalidOperationException("❌ SecurityKey not found in configuration. Check appsettings.json -> TokenOptions:SecurityKey");

    var key = Encoding.UTF8.GetBytes(securityKey);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["TokenOptions:Issuer"],
            ValidAudience = configuration["TokenOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        // (Opcional) Log de errores o mensajes de token
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"❌ Token inválido: {context.Exception}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"✅ Token válido para usuario: {context.Principal?.Identity?.Name}");
                return Task.CompletedTask;
            }
        };
    });
}


    public static void AddIdentityExtension(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
            options.SignIn.RequireConfirmedEmail = false;
        })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
    }
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                builder => { builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); }));
    }
    public static void AddAuthorizationPoliciesExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
        });
    }
    public static IServiceProvider ApplyMigrationsExtension(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigration = db.Database.GetPendingMigrations();
        if (pendingMigration.Any())
        {
            db.Database.Migrate();
        }

        return serviceProvider;
    }
    public static IServiceCollection SetSettingsExtension(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.Configure<HangFireSettings>(configuration.GetSection("HangFire"));
        return services;
    }
    public static void AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(cors =>
        {
            cors.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });
    }
}
