using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class TabContainer
{
    private string? _currentKey;

    [Parameter]
    public string? CurrentKey { get; set; }

    [Parameter]
    public EventCallback<string?> CurrentKeyChanged { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _currentKey = CurrentKey;
    }

    public SSettings<ITab> Tabs { get; } = new();

    public async Task SelectTabAsync(ITab tab)
    {
        _currentKey = tab.Key;
        await CurrentKeyChanged.InvokeAsync(_currentKey);
    }
}