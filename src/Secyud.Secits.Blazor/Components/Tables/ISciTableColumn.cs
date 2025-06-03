using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciTableColumn<in TItem> : IScSetting
{
    string? Caption { get; set; }
    RenderFragment GenerateHeader();
    RenderFragment GenerateColumn(TItem item);
    RenderFragment GenerateFooter();
    int GetColumnWidth();
    int Width { get; set; }
}