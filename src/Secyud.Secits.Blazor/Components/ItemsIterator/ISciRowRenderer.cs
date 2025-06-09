using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Components;

public interface ISciRowRenderer<in TItem> : IScSetting
{
    string? GetRowClass(TItem item);
    string? GetRowStyle(TItem item);
    void OnRowClick(MouseEventArgs args, TItem item);
    bool ClickEnabled { get; }
}