using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciTableFooterRenderer : IScSetting
{
    RenderFragment GenerateFooter(int index);
}