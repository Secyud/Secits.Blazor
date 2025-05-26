namespace Secyud.Secits.Blazor.Components;

public interface ISTable<TItem> : ISList<TItem>
{
    void AddTableColumn(ISciTableColumn<TItem> column);

    void RemoveTableColumn(ISciTableColumn<TItem> column);
}