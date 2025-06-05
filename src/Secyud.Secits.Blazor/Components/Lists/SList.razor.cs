using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public partial class SList<TItem>
{
    protected override string ComponentName => "list";
}