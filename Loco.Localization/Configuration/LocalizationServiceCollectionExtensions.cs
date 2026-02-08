using Loco.Localization.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Loco.Localization.Configuration;

public static class LocalizationServiceCollectionExtensions
{
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization();                // достатъчно за IStringLocalizer<T>
        services.AddScoped<ILocalizer, LocalizerAdapter>();
        return services;
    }
}