using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Core.Application.Services;
using SistemaDeVentas.Infrastructure.Data;
using SistemaDeVentas.Infrastructure.Data.Repositories;
using SistemaDeVentas.Infrastructure.Services.DTE;
using SistemaDeVentas.Infrastructure.Services.SII;

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

        // Registrar servicios DTE
        services.AddScoped<FacturaAfectaBuilder>();
        services.AddScoped<FacturaExentaBuilder>();
        services.AddScoped<DteBuilderService, FacturaAfectaBuilder>(); // Default implementation

        // Registrar servicios de firma digital y timbrado
        services.AddHttpClient<StampingService>();
        services.AddScoped<ICertificateService, CertificateService>();
        services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
        services.AddScoped<IStampingService, StampingService>();
        services.AddScoped<IDteProcessingService, DteProcessingService>();
        services.AddScoped<IPdf417Service, Pdf417Service>();

        // Registrar configuración DTE
        services.AddOptions<DteSettings>();

        // Registrar repositorios DTE
        services.AddScoped<ICafRepository, CafRepository>();

        // Registrar servicios SII
        services.AddHttpClient<ISiiAuthenticationService, SiiAuthenticationService>();
        services.AddHttpClient<ISiiDteSubmissionService, SiiDteSubmissionService>();
        services.AddHttpClient<ISiiStatusQueryService, SiiStatusQueryService>();
        services.AddScoped<ISiiValidationService, SiiValidationService>();
        services.AddScoped<ISiiDteWorkflowService, SiiDteWorkflowService>();
        services.AddScoped<ISiiDteWorkflowService, SiiDteWorkflowService>();

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
        services.AddScoped<IDteSaleService, DteSaleService>();
        services.AddScoped<IDteSaleService, DteSaleService>();

        // Registrar servicio de autenticación
        services.AddScoped<IAuthService, UserService>();

        // Registrar servicios DTE
        services.AddScoped<FacturaAfectaBuilder>();
        services.AddScoped<FacturaExentaBuilder>();
        services.AddScoped<DteBuilderService, FacturaAfectaBuilder>(); // Default implementation

        // Registrar servicios de firma digital y timbrado
        services.AddHttpClient<StampingService>();
        services.AddScoped<ICertificateService, CertificateService>();
        services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
        services.AddScoped<IStampingService, StampingService>();
        services.AddScoped<IDteProcessingService, DteProcessingService>();
        services.AddScoped<IPdf417Service, Pdf417Service>();

        // Registrar configuración DTE
        services.AddOptions<DteSettings>();

        // Registrar repositorios DTE
        services.AddScoped<ICafRepository, CafRepository>();

        // Registrar servicios SII
        services.AddHttpClient<ISiiAuthenticationService, SiiAuthenticationService>();
        services.AddHttpClient<ISiiDteSubmissionService, SiiDteSubmissionService>();
        services.AddHttpClient<ISiiStatusQueryService, SiiStatusQueryService>();
        services.AddScoped<ISiiValidationService, SiiValidationService>();

        return services;
    }
}