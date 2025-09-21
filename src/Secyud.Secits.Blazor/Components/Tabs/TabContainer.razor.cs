using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class TabContainer
{
    public string? CurrentKey { get; set; }
    public SSettings<ITab> Tabs { get; } = new();
    public SSettings<ITabListener> TabListeners { get; } = new();

    public async Task SelectTabAsync(ITab tab)
    {
        CurrentKey = tab.Key;
        await TabListeners.InvokeAsync(u => u.TabChangedAsync());
    }
}