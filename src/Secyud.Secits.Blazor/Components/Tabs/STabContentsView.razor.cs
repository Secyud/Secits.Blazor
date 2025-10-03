using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class STabContentsView : IHasCustomStyle, ITabListener, IDisposable
{
    private STabContainer? _tabContainer;

    [CascadingParameter]
    public STabContainer? TabContainer
    {
        get => _tabContainer;
        set
        {
            _tabContainer?.TabListeners.Forgo(this);
            _tabContainer = value;
            _tabContainer?.TabListeners.Apply(this);
        }
    }

    [Parameter]
    public TabRenderMode RenderMode { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public bool AllowEmptyContent { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-contents", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    protected RenderFragment RenderTabContentsByMode(IReadOnlyList<ITab> allTabs, string? key)
    {
        if (RenderMode == TabRenderMode.LoadAll)
            return RenderTabContents(allTabs, key);

        var tabs = new List<ITab>();
        foreach (var tab in allTabs)
        {
            if (tab.Key == key)
            {
                tab.IsRendered = true;
                tabs.Add(tab);
                continue;
            }

            if (RenderMode != TabRenderMode.LazyLoad) continue;
            if (tab.IsRendered) tabs.Add(tab);
        }

        return RenderTabContents(tabs, key);
    }

    protected RenderFragment RenderTabContents(IReadOnlyList<ITab> tabs, string? key)
    {
        return builder =>
        {
            foreach (var tab in tabs)
            {
                builder.AddContent(tab.Index, RenderTabContent(tab, key));
            }
        };
    }

    public Task TabChangedAsync(object? sender)
    {
        return sender == this ? Task.CompletedTask : InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        TabContainer = null;
    }
}