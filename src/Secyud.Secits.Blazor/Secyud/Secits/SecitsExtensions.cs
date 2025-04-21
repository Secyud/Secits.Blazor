using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Localization;

namespace Secyud.Secits;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services)
    {
        services.AddTransient<ISecitsLocalizationService, DefaultSecitsLocalizationService>();
        
        return services;
    }
}