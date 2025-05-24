namespace Secyud.Secits.Blazor.Components;

public interface ITable<TItem>
{
    void AddColumn(ITableColumnSetting<TItem> columnSetting);
    void RemoveColumn(ITableColumnSetting<TItem> columnSetting);
    Task OnDataLoadAsync();
    bool? EnableFilter { get; set; }
    bool? EnableSorter { get; set; }
    bool? EnableHeader { get; set; }
}