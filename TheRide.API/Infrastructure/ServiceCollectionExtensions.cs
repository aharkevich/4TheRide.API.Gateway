using TheRide.API.Interfaces.Settings;

namespace TheRide.API.Infrastructure;

/// <summary>
/// Service Collection Helpers.
/// </summary>
internal static class ServiceCollectionExtensions
{
    private const string CarsStoreSettings = "CarsStoreSettings";
    private const string ModelsStoreSettings = "ModelsStoreSettings";

    /// <summary>
    /// Add <see cref="CarsStoreSettings"/> object from configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    internal static IServiceCollection AddCarsStoreSettings(this IServiceCollection services, IConfiguration config)
    {
        var carsStoreSettings = config.GetSection(CarsStoreSettings)?.Get<CarsStoreSettings>();

        if (carsStoreSettings != null)
        {
            services.AddSingleton(carsStoreSettings);
        }

        return services;
    }
    
    /// <summary>
    /// Add <see cref="ModelsStoreSettings"/> object from configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns></returns>
    internal static IServiceCollection AddModelsStoreSettings(this IServiceCollection services, IConfiguration config)
    {
        var modelsStoreSettings = config.GetSection(ModelsStoreSettings)?.Get<ModelsStoreSettings>();

        if (modelsStoreSettings != null)
        {
            services.AddSingleton(modelsStoreSettings);
        }

        return services;
    }
}