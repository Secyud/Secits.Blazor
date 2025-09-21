using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class TabTagsView : IHasCustomCss, ITabListener
{
    private TabContainer? _tabContainer;

    [CascadingParameter]
    public TabContainer? TabContainer
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
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-tags", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    public Task TabChangedAsync()
    {
        return InvokeAsync(StateHasChanged);
    }

    protected async Task SelectTabAsync(ITab tab)
    {
        if (TabContainer is null || TabContainer.CurrentKey == tab.Key) return;
        await TabContainer.SelectTabAsync(tab);
    }
}