using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciBodyRenderer
{
    RenderFragment? GenerateBody();
}