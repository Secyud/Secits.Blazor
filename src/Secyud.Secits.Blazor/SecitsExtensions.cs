using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Options;
using Secyud.Secits.Blazor.Services;

namespace Secyud.Secits.Blazor;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services,
        Action<SecitsOptions>? optionAction = null)
    {
        services.AddTransient<ILocalizationService, DefaultLocalizationService>();
        services.AddTransient<IJsElementService, SecitsJsElementService>();
        services.AddTransient<IJsWindowService, SecitsJsWindowService>();


        if (optionAction is not null)
            services.Configure(optionAction);

        return services;
    }
}