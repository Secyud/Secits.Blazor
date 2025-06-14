using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciFooterRenderer
{
    RenderFragment? GenerateFooter();
}