using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISList<TItem> : IScwItems<TItem>
{
    RenderFragment GenerateRow(TItem item);

    void SetItemsRender(ISciItemsRenderer renderer);
    void UnsetItemsRender(ISciItemsRenderer renderer);
    void AddHeaderRender(ISciHeaderRender renderer);
    void RemoveHeaderRender(ISciHeaderRender renderer);
    void AddColumnRender(ISciColumnRender<TItem> renderer);
    void RemoveColumnRender(ISciColumnRender<TItem> renderer);
    void AddFooterRender(ISciFooterRender renderer);
    void RemoveFooterRender(ISciFooterRender renderer);
}