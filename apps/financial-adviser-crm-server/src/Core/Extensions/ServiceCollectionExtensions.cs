using FinancialAdviserCrm.APIs;

namespace FinancialAdviserCrm;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAdvisersService, AdvisersService>();
        services.AddScoped<IAppointmentsService, AppointmentsService>();
        services.AddScoped<IClientsService, ClientsService>();
        services.AddScoped<IFinancialProductsService, FinancialProductsService>();
    }
}
