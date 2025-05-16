using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public partial class SDropdown<TItem,TValue>: 
    IValueComponent<TValue>
{
    protected override string ComponentName => "dropdown";
    public TValue Value { get; set; } = default!;
    public EventCallback<TValue> ValueChanged { get; set; }
}