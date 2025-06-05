using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciColumnRenderer<in TItem>
{
    RenderFragment GenerateColumn(TItem item);
}