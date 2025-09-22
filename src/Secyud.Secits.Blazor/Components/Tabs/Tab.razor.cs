using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class Tab : ITab
{
    [Parameter]
    public RenderFragment? Tag { get; set; }

    [Parameter]
    public RenderFragment? Content { get; set; }

    [Parameter]
    public string Key { get; set; } = Guid.NewGuid().ToString("N");

    [Parameter]
    public EventCallback Click { get; set; }

    [Parameter]
    public bool PreventDefaultClick { get; set; }

    public int Index { get; set; }
    public bool IsRendered { get; set; }

    public RenderFragment? RenderTab() => Tag;

    public RenderFragment? RenderTabContent() => Content;

    protected override void ApplySetting()
    {
        var tabs = Master.Tabs;
        Index = tabs.Count > 0 ? tabs.Max(u => u.Index) + 1 : 0;
        tabs.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Tabs.Forgo(this);
    }
}