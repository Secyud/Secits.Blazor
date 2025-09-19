using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor;

public static class SecitsFontAwesomeIconsExtensions
{
    public static IServiceCollection AddSecitsFontAwesome(this IServiceCollection services)
    {
        services.AddSingleton<IIconProvider, FontAwesomeIconProvider>();

        services.Configure<SecitsScriptsOptions>(options =>
        {
            options.Scripts.Add(opt =>
                [new HtmlPathResource("_content/Secyud.Secits.Blazor.Icons.FontAwesome/js/all.min.js", "secits-icon-font-awesome")]);
        });
        services.Configure<SecitsStylesOptions>(options =>
        {
            options.Styles.Add(opt =>
                [new HtmlPathResource("_content/Secyud.Secits.Blazor.Icons.FontAwesome/css/all.min.css", "secits-icon-font-awesome-js")]);
        });
        return services;
    }
}