using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public partial class SList<TItem>
{
    protected override string ComponentName => "list";

    #region Settings

    public SSettings<ISciColumnRenderer<TItem>> Columns { get; } = new();

    #endregion
}