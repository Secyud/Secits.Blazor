using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class STabContainer : SPluggableBase
{
    protected override string ComponentName => "tab-container";

    public string? CurrentKey { get; set; }

    public SSettings<ITabsProvider> TabProviders { get; } = [];

    public SSettings<ITabListener> TabListeners { get; } = new();

    public async Task SelectTabAsync(object? sender, string? tabKey)
    {
        CurrentKey = tabKey;
        await TabListeners.InvokeAsync(u => u.TabChangedAsync(sender));
    }

    protected override void OnBuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
    }
}