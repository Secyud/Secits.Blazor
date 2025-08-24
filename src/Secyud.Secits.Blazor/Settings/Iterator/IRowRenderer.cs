using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// represent the row setting.
/// click action is useful for selection.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IRowRenderer<in TItem> : IIsPlugin
{
    string? GetRowClass(TItem item);
    string? GetRowStyle(TItem item);
    void OnRowClick(MouseEventArgs args, TItem item);
}