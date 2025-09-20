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
    public string Key { get; set; } = null!;

    public int Index { get; set; }
    public bool IsRendered { get; set; }

    public RenderFragment? RenderTab() => Tag;
    
    public RenderFragment? RenderTabContent() => Content;

    protected override void ApplySetting()
    {
        var maxIndex = Master.Tabs.Max(u => u.Index);
        Index = maxIndex + 1;
        Master.Tabs.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Tabs.Forgo(this);
    }
}