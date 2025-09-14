using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor;

public static class SecitsFontAwesomeIconExtensions
{
    public static IServiceCollection AddSecitsFontAwesome(this IServiceCollection services)
    {
        services.AddSingleton<IIconProvider, FontAwesomeIconProvider>();

        services.Configure<SecitsOptions>(options =>
        {
            options.CssPaths.Add("_content/Secyud.Secits.Blazor.Icon.FontAwesome/css/all.min.css");
            options.JsPaths.Add("_content/Secyud.Secits.Blazor.Icon.FontAwesome/js/all.min.js");
        });
        return services;
    }
}