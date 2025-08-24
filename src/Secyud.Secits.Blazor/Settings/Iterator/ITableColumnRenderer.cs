using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// render settings for a table column.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ITableColumnRenderer<in TItem> : IIsPlugin, IListColumnRenderer<TItem>
{
    RenderFragment GenerateHeader();
    RenderFragment GenerateFooter();
    int GetColumnWidth();
    int Width { get; set; }
}