using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public partial class SList<TItem>
{
    protected override string ComponentName => "list";

    #region Settings

    public SSettings<IListColumnRenderer<TItem>> Columns { get; } = new();

    #endregion
}