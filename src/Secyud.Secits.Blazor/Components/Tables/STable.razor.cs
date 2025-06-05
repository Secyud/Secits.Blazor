using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Arguments;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public partial class STable<TItem> 
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Settings

    private readonly List<ISciTableColumnRenderer<TItem>> _columns = [];

    public IReadOnlyList<ISciTableColumnRenderer<TItem>> TableColumns => _columns;

    public void AddTableColumn(ISciTableColumnRenderer<TItem> column)
    {
        RemoveTableColumn(column);
        _columns.Add(column);
    }

    public void RemoveTableColumn(ISciTableColumnRenderer<TItem> column)
    {
        _columns.Remove(column);
    }


    private readonly List<ISciTableHeaderRenderer> _tableHeaders = [];

    public IReadOnlyList<ISciTableHeaderRenderer> TableHeaders => _tableHeaders;

    public virtual void AddTableHeaderRender(ISciTableHeaderRenderer renderer)
    {
        RemoveTableHeaderRender(renderer);
        _tableHeaders.Add(renderer);
    }

    public virtual void RemoveTableHeaderRender(ISciTableHeaderRenderer renderer)
    {
        _tableHeaders.Remove(renderer);
    }

    private readonly List<ISciTableFooterRenderer> _tableFooters = [];

    public IReadOnlyList<ISciTableFooterRenderer> TableFooters => _tableFooters;

    public virtual void AddTableFooterRender(ISciTableFooterRenderer renderer)
    {
        RemoveTableFooterRender(renderer);
        _tableFooters.Add(renderer);
    }

    public virtual void RemoveTableFooterRender(ISciTableFooterRenderer renderer)
    {
        _tableFooters.Remove(renderer);
    }

    public void RefreshUi()
    {
        StateHasChanged();
    }
    
    #endregion
}