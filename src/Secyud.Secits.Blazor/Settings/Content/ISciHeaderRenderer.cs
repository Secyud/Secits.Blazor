using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciHeaderRenderer
{
    RenderFragment? GenerateHeader();
}