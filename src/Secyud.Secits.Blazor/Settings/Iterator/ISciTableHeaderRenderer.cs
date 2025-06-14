using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciTableHeaderRenderer : IScSetting
{
    RenderFragment GenerateHeader(int index);
}