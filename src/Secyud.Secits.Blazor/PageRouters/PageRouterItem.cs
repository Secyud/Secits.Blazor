using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.PageRouters;

public class PageRouterItem(Uri uri, Type pageType, Dictionary<string, object?> routeValues)
{
    public string Id { get; } = Guid.NewGuid().ToString("N");
    public Uri Uri { get; set; } = uri;
    public Type PageType { get; } = pageType;
    public Dictionary<string, object?> RouteValues { get; } = routeValues;
    public Type? ResourceType { get; set; }
    public string? Name { get; set; }
    public string[] Parameters { get; set; } = [];
    public RenderFragment? Body { get; set; }

    /// <summary>
    /// for other using
    /// </summary>
    public string? Key { get; set; }

    public void Refresh()
    {
        Body = null;
    }
}