using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciColumnRenderer<TItem> : IScSetting
{
    RenderFragment GenerateColumn(TItem item);
}