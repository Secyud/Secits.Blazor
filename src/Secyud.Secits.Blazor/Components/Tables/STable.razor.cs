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
public partial class STable<TItem> : IScsTheme
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Columns

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


    private readonly List<ISciTableHeaderRenderer> _headers = [];

    public IReadOnlyList<ISciTableHeaderRenderer> TableHeaders => _headers;

    public virtual void AddTableHeaderRender(ISciTableHeaderRenderer renderer)
    {
        RemoveTableHeaderRender(renderer);
        _headers.Add(renderer);
    }

    public virtual void RemoveTableHeaderRender(ISciTableHeaderRenderer renderer)
    {
        _headers.Remove(renderer);
    }

    private readonly List<ISciTableFooterRenderer> _footers = [];

    public IReadOnlyList<ISciTableFooterRenderer> TableFooters => _footers;

    public virtual void AddTableFooterRender(ISciTableFooterRenderer renderer)
    {
        RemoveTableFooterRender(renderer);
        _footers.Add(renderer);
    }

    public virtual void RemoveTableFooterRender(ISciTableFooterRenderer renderer)
    {
        _footers.Remove(renderer);
    }

    public void RefreshUi()
    {
        StateHasChanged();
    }
    
    #endregion

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }
}