using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings.Tabs;

public interface ITab
{
    string Key { get; set; }
    int Index { get; set; }
    bool IsRendered { get; set; }
    RenderFragment? RenderTab();
    RenderFragment? RenderTabContent();
}