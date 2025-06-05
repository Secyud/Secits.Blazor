using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciFooterRenderer
{
    RenderFragment GenerateFooter();
}