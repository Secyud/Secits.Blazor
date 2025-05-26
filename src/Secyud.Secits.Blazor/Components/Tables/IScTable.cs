namespace Secyud.Secits.Blazor.Components;

public interface IScTable<TItem> : IScList<TItem>
{
    void AddTableColumn(ISciTableColumn<TItem> column);

    void RemoveTableColumn(ISciTableColumn<TItem> column);
}