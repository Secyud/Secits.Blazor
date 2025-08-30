using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public sealed partial class STable<TValue>
{
    protected override string ComponentName => "table";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    #region Settings

    public SSettings<ITableColumnRenderer<TValue>> TableColumns { get; } = new();
    public SSettings<ITableHeaderRenderer> TableHeaders { get; } = new();
    public SSettings<ITableFooterRenderer> TableFooters { get; } = new();

    #endregion
}