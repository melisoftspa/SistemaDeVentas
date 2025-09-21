using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Core.Application.Services;
using SistemaDeVentas.Infrastructure.Data;
using SistemaDeVentas.Infrastructure.Data.Repositories;

namespace SistemaDeVentas.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        // Configurar Entity Framework
        services.AddDbContext<SalesSystemDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Registrar repositorios
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IDetailRepository, DetailRepository>();

        // Registrar servicios de aplicación
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITaxService, TaxService>();
        services.AddScoped<IDetailService, DetailService>();

        // Registrar servicio de autenticación
        services.AddScoped<IAuthService, UserService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, Action<DbContextOptionsBuilder> configureDbContext)
    {
        // Configurar Entity Framework con configuración personalizada
        services.AddDbContext<SalesSystemDbContext>(configureDbContext);

        // Registrar repositorios
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IDetailRepository, DetailRepository>();

        // Registrar servicios de aplicación
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITaxService, TaxService>();
        services.AddScoped<IDetailService, DetailService>();

        // Registrar servicio de autenticación
        services.AddScoped<IAuthService, UserService>();

        return services;
    }
}