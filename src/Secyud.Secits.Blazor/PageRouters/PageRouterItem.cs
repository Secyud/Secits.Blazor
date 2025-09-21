using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.PageRouters;

public class PageRouterItem
{
    public PageRouterItem(Uri uri, Type pageType, Dictionary<string, object?> routeValues)
    {
        Uri = uri;
        PageType = pageType;
        RouteValues = routeValues;
    }

    public string Id { get; } = Guid.NewGuid().ToString("N");
    public Uri Uri { get; set; }
    public Type PageType { get; }
    public Dictionary<string, object?> RouteValues { get; }
    public Type? ResourceType { get; set; }
    public string? Name { get; set; }
    public string[] Parameters { get; set; } = [];
    public Func<string>? DisplayNameGetter { get; set; }

    public string DisplayName
    {
        get
        {
            if (DisplayNameGetter is not null)
                return DisplayNameGetter();
            return Name ?? PageType.Name;
        }
    }

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