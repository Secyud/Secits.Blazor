using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciItemsRenderer<TItem>
{
    RenderFragment GenerateItems(RenderFragment<TItem> itemTemplate);
    Task RefreshAsync();
}