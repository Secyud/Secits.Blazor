using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Localization;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services,
        Action<SecitsOptions>? optionAction = null)
    {
        services.AddTransient<ILocalizationService, DefaultLocalizationService>();
        services.AddTransient<IElementService, SecitsElementService>();


        if (optionAction is not null)
            services.Configure(optionAction);

        return services;
    }
}