using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Components;

public partial class STable<TItem> : IDataComponent<TItem>, IFilteredComponent
{
    protected override string ComponentName => "table";

    [Parameter]
    public IEnumerable<TItem>? Data { get; set; }

    [Parameter]
    public EventCallback<DataRequest> RefreshCallback { get; set; }

    [Parameter]
    public bool? EnableFilter { get; set; }

    [Parameter]
    public bool? EnableSorter { get; set; }

    private readonly DataRequest _request = new();

    #region Columns

    private List<ITableColumnSetting<TItem>> Columns { get; } = [];

    public void AddColumn(ITableColumnSetting<TItem> column)
    {
        if (Columns.Contains(column)) return;
        Columns.Add(column);
        _request.Filters.Add(column.Filter);
        _request.Sorters.Add(column.Sorter);
    }

    public void RemoveColumn(ITableColumnSetting<TItem> column)
    {
        Columns.Remove(column);
        _request.Filters.Remove(column.Filter);
        _request.Sorters.Remove(column.Sorter);
    }

    private bool RenderHeader()
    {
        return Columns.Any(u => u.RenderHeader());
    }

    private bool RenderFilter()
    {
        return Columns.Any(u => u.RenderFilter());
    }

    #endregion
}