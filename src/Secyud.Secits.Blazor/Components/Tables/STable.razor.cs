using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Abstraction;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// table组件,可以展示多行数据.
/// 具备选择功能,由于泛型选择,必须要指定TValue,
/// 可通过ValueField指定.
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class STable<TItem, TValue> : IScsTheme, ISchFilter, ITable<TItem>
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool? EnableFilter { get; set; }

    [Parameter]
    public bool? EnableSorter { get; set; }

    [Parameter]
    public bool? EnableHeader { get; set; }

    private readonly DataRequest _request = new();

    public async Task OnDataLoadAsync()
    {
        await ItemsLoad.InvokeAsync(_request);
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

    /// <summary>
    /// 指定是否渲染Header标题, 任意一个子组件需要header时, 都要渲染.
    /// 为了由子组件决定是否渲染, 不直接获取EnableHeader.
    /// </summary>
    /// <returns></returns>
    private bool RenderHeader()
    {
        return Columns.Any(u => u.RenderHeader());
    }

    /// <summary>
    /// 指定是否渲染Filter过滤
    /// 类似Header
    /// </summary>
    /// <returns></returns>
    private bool RenderFilter()
    {
        return Columns.Any(u => u.RenderFilter());
    }

    #endregion

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }
}