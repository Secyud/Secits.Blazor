using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

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

    public SSettings<ISciTableColumnRenderer<TItem>> TableColumns { get; } = new();
    public SSettings<ISciTableHeaderRenderer> TableHeaders { get; } = new();
    public SSettings<ISciTableFooterRenderer> TableFooters { get; } = new();

    public void RefreshUi()
    {
        StateHasChanged();
    }

    #endregion
}