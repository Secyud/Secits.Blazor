using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciFooterRender: IScSetting
{
    RenderFragment GenerateFooter();
}