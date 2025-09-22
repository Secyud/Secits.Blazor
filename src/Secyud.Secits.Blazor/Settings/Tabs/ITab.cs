using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings.Tabs;

public interface ITab : ICanClick
{
    string Key { get; }
    int Index { get; }
    bool IsRendered { get; set; }
    bool PreventDefaultClick { get; }
    RenderFragment? RenderTab();
    RenderFragment? RenderTabContent();
}