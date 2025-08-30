using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public partial class SGrid<TValue>
{
    protected override string ComponentName => "grid";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    [Parameter]
    public bool FixFirstColumn { get; set; }

    [Parameter]
    public bool FixLastColumn { get; set; }

    #region Settings

    public SSettings<ITableColumnRenderer<TValue>> TableColumns { get; } = new();
    public SSettings<ITableHeaderRenderer> TableHeaders { get; } = new();
    public SSettings<ITableFooterRenderer> TableFooters { get; } = new();


    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);

        context.AppendStyle("--column-template", string.Join(' ',
            TableColumns.Select(u => u.RealWidth + "px")));
    }

    protected override string? GetRowClass(TValue value)
    {
        return base.GetRowClass(value) + " body";
    }

    #endregion
}