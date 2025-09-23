using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public class Tab<TItem> : ITab
{
    public Tab(TItem item)
    {
        Item = item;
    }

    public TItem Item { get;}
    public RenderFragment? Tag { get; set; }

    public RenderFragment? Content { get; set; }

    public string Key { get; set; } = Guid.NewGuid().ToString("N");

    public Func<Task>? Click { get; set; }

    public bool PreventDefaultClick { get; set; }

    public int Index { get; set; }

    public bool IsRendered { get; set; }

    public RenderFragment? RenderTab() => Tag;

    public RenderFragment? RenderTabContent() => Content;
}