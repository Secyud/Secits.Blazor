using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciTableHeaderRenderer : IScSetting
{
    RenderFragment GenerateHeader(int index);
}