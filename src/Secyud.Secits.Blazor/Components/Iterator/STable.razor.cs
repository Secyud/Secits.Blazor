using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TItem"></typeparam>
[CascadingTypeParameter(nameof(TItem))]
public sealed partial class STable<TItem>
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Settings

    public SSettings<ITableColumnRenderer<TItem>> TableColumns { get; } = new();
    public SSettings<ITableHeaderRenderer> TableHeaders { get; } = new();
    public SSettings<ITableFooterRenderer> TableFooters { get; } = new();

    #endregion
}