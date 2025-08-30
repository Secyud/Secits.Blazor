using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// render settings for a table column.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface ITableColumnRenderer<in TValue> : IIsPlugin, IListColumnRenderer<TValue>
{
    RenderFragment GenerateHeader();
    RenderFragment GenerateFooter();
    int GetColumnWidth();
    int Width { get; set; }
}