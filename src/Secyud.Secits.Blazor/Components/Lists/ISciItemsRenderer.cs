using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciItemsRenderer : IScSetting, ISccRefresh
{
    RenderFragment GenerateItemsContent();
}