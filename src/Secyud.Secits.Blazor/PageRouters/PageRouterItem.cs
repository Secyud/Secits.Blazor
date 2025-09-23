using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.PageRouters;

public class PageRouterItem
{
    public PageRouterItem(Uri uri, Type pageType, RenderFragment body)
    {
        Uri = uri;
        PageType = pageType;
        Body = body;
    }

    public string Id { get; } = Guid.NewGuid().ToString("N");
    public Uri Uri { get; set; }
    public Type PageType { get; }
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

    public RenderFragment Body { get; }

    /// <summary>
    /// for other using
    /// </summary>
    public string? Key { get; set; }
}