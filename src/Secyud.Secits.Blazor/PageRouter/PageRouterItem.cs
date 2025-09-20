using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.PageRouter;

public class PageRouterItem(Uri uri, Type pageType, Dictionary<string, object?> routeValues)
{
    private RenderFragment? _body;
    public Uri Url { get; set; } = uri;
    public Type PageType { get; } = pageType;
    public Dictionary<string, object?> RouteValues { get; } = routeValues;
    public Type? ResourceType { get; set; }
    public string? Name { get; set; }
    public string[] Parameters { get; set; } = [];
    public RenderFragment Body => _body ??= CreateRouterTabsItemBody(this);

    /// <summary>
    /// for other using
    /// </summary>
    public string? Key { get; set; }

    public void Refresh()
    {
        _body = null;
    }

    private static RenderFragment CreateRouterTabsItemBody(PageRouterItem item)
    {
        return builder =>
        {
            builder.OpenComponent(0, item.PageType);
            int sequence = 1;
            foreach (var (key, value) in item.RouteValues)
                builder.AddAttribute(sequence++, key, value);
            builder.CloseComponent();
        };
    }
}