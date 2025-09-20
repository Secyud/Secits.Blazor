using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.PageRouters;

public class PageRouterManager
{
    public EventHandler? StateChanged;
    private readonly List<PageRouterItem> _items = [];
    private PageRouterItem? _currentItem;

    public PageRouterItem? CurrentItem => _currentItem;
    public IReadOnlyList<PageRouterItem> Items => _items.AsReadOnly();

    public void ActivatePageRouteItem(RouteData routeData, string uriString)
    {
        if (!Uri.TryCreate(uriString, UriKind.Absolute, out var uri)) return;
        _currentItem = _items.FirstOrDefault(u => u.Uri == uri);
        if (_currentItem is null)
        {
            _currentItem = CreatePageRouterItem(routeData, uri);
            _items.Add(_currentItem);
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePageRouteItem(PageRouterItem item)
    {
        var index = _items.IndexOf(item);
        if (index < 0) return;
        _items.RemoveAt(index);
        if (_currentItem == item)
        {
            _currentItem = _items.Count > 0 ? _items[Math.Max(0, index - 1)] : null;
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    private PageRouterItem CreatePageRouterItem(RouteData routeData, Uri uri)
    {
        var pageType = routeData.PageType;
        var routeValues = routeData.RouteValues;
        var result = new PageRouterItem(uri, pageType, routeValues
            .ToDictionary(u => u.Key, u => u.Value));
        var attribute = pageType.GetCustomAttribute<PageRouterAttribute>();
        if (attribute is null) return result;
        if (attribute.Name is not null)
            result.Name = attribute.Name;
        result.ResourceType = attribute.ResourceType;

        var len = Math.Min(attribute.Parameters.Length, attribute.ParameterPrefixes.Length);
        var parameters = new string[len];
        for (var i = 0; i < len; i++)
        {
            var parameter = attribute.Parameters[i];
            var prefix = attribute.ParameterPrefixes[i];
            if (routeValues.TryGetValue(parameter, out var parameterValue))
            {
                parameters[i] = $"{prefix}{parameterValue}";
            }
            else
            {
                parameters[i] = string.Empty;
            }
        }

        result.Parameters = parameters;
        return result;
    }
}