using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.PageRoutes;

public class PageRouteItem
{
    public PageRouteItem(Uri uri, Type pageType, IReadOnlyList<KeyValuePair<string, object>> pageParameters)
    {
        Uri = uri;
        PageType = pageType;
        PageParameters = pageParameters;
    }

    public string Id { get; } = Guid.NewGuid().ToString("N");
    public Uri Uri { get; set; }
    public Type PageType { get; }
    public IReadOnlyList<KeyValuePair<string, object>> PageParameters { get; }
    public Type? ResourceType { get; set; }
    public string? Name { get; set; }
    public string[] Parameters { get; set; } = [];
    public Func<string>? DisplayNameGetter { get; set; }
    public int Sequence { get; set; }

    public string DisplayName
    {
        get
        {
            if (DisplayNameGetter is not null)
                return DisplayNameGetter();
            return Name ?? PageType.Name;
        }
    }

    /// <summary>
    /// for other using
    /// </summary>
    public string? Key { get; set; }

    public void GenerateBody(RenderTreeBuilder builder)
    {
        builder.OpenComponent(0, PageType);
        foreach (var routeValue in PageParameters)
            builder.AddComponentParameter(1, routeValue.Key, routeValue.Value);
        builder.CloseComponent();
    }
}