using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciHeaderRender : IScSetting
{
    RenderFragment GenerateHeader();
}