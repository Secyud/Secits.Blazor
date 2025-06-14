using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciTableFooterRenderer : IScSetting
{
    RenderFragment GenerateFooter(int index);
}