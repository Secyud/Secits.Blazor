using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public partial class STable<TItem> : IDataComponent<TItem>, IFilteredComponent
{
    protected override string ComponentName => "table";

    [Parameter]
    public IEnumerable<TItem>? Data { get; set; }

    [Parameter]
    public EventCallback<DataRequest> DataLoad { get; set; }

    [Parameter]
    public bool? EnableFilter { get; set; }

    [Parameter]
    public bool? EnableSorter { get; set; }

    [Parameter]
    public bool? EnableHeader { get; set; }

    private readonly DataRequest _request = new();

    public async Task OnDataLoadAsync()
    {
        await DataLoad.InvokeAsync(_request);
    }

    #region Columns

    private List<ITableColumnSetting<TItem>> Columns { get; } = [];

    public void AddColumn(ITableColumnSetting<TItem> column)
    {
        if (Columns.Contains(column)) return;
        Columns.Add(column);
        _request.Filters.Add(column.Filter);
        _request.Sorters.Add(column.Sorter);
        StateHasChanged();
    }

    public void RemoveColumn(ITableColumnSetting<TItem> column)
    {
        Columns.Remove(column);
        _request.Filters.Remove(column.Filter);
        _request.Sorters.Remove(column.Sorter);
        StateHasChanged();
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