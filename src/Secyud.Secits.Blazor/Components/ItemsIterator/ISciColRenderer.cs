using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciColRenderer<in TItem>
{
    RenderFragment GenerateCol(TItem item);
}