using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciRowRenderer<in TItem>
{
    RenderFragment GenerateRow(RenderFragment content);
}