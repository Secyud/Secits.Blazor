using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Localization;

namespace Secyud.Secits.Blazor;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services)
    {
        services.AddTransient<ISecitsLocalizationService, DefaultSecitsLocalizationService>();
        
        return services;
    }
}