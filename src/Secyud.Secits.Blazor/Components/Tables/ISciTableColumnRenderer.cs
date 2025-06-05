using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciTableColumnRenderer<in TItem> : ISciColumnRenderer<TItem>
{
    RenderFragment GenerateHeader();
    RenderFragment GenerateFooter();
    int GetColumnWidth();
    int Width { get; set; }
}