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
    string GetColClass();
    int RealWidth { get; set; }
    int MaxWidth { get; }
    int MinWidth { get; }
}