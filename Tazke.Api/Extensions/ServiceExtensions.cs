using Microsoft.EntityFrameworkCore;
using System;
using Tazke.Api.Data;
using Tazke.Api.Data.Repositories;
using Tazke.Api.Services;

namespace Tazke.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);

            // Configurações adicionais para desenvolvimento
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Pool de conexões para melhor performance
        //services.AddDbContextPool<AppDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IProjetoService, ProjetoService>();
        return services;
    }
}
