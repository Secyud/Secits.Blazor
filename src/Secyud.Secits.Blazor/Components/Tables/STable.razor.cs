using Microsoft.AspNetCore.Components;
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
public partial class STable<TItem, TValue> : IScsTheme, IScTable<TItem>
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Columns

    private readonly List<ISciTableColumn<TItem>> _columns = [];

    public void AddTableColumn(ISciTableColumn<TItem> column)
    {
        RemoveTableColumn(column);
        _columns.Add(column);
    }

    public void RemoveTableColumn(ISciTableColumn<TItem> column)
    {
        _columns.Remove(column);
    }

    #endregion

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }
}