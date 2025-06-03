using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciTableColumnRenderer<in TItem> : IScSetting
{
    string? Caption { get; set; }
    RenderFragment GenerateHeader();
    RenderFragment GenerateColumn(TItem item);
    RenderFragment GenerateFooter();
    int GetColumnWidth();
    int Width { get; set; }
}