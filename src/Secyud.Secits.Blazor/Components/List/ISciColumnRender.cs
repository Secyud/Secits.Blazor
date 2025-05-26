using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciColumnRender<TItem> : IScSetting
{
    RenderFragment GenerateColumn(TItem item);
}