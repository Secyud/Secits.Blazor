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
        services.AddTransient<ILocalizationService, NullLocalizationService>();
        services.AddTransient<IJsElement, SJsElement>();
        services.AddTransient<IJsWindow, SJsWindow>();
        services.AddTransient<IJsDocument, SJsDocument>();
        services.AddScoped<JsEventHandler>();

        if (optionAction is not null)
            services.Configure(optionAction);

        return services;
    }
}