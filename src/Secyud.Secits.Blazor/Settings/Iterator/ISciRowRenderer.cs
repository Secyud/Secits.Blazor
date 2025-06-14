using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public interface ISciRowRenderer<in TItem> : IScSetting
{
    string? GetRowClass(TItem item);
    string? GetRowStyle(TItem item);
    void OnRowClick(MouseEventArgs args, TItem item);
}