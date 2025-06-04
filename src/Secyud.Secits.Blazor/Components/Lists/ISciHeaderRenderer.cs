using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciHeaderRenderer : IScSetting
{
    RenderFragment GenerateHeader();
}