using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciRowRenderer<TItem>
{
    RenderFragment GenerateRow(TItem item, RenderFragment<TItem> content);
}