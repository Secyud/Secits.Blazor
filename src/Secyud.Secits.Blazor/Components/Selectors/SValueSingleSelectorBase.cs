using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SValueSingleSelectorBase<TComponent, TValue> :
    SSelectorBase<TComponent, TValue>, ISchValue<TValue>
    where TComponent : ScBusinessBase
{
    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected virtual async Task OnValueSelectChangedAsync(TValue? value)
    {
        Value = value!;
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);
    }

    protected override bool IsSelected(TValue? selection)
    {
        return selection is not null && Equals(Value, selection);
    }

    public override async Task ClearSelectAsync()
    {
        await OnValueSelectChangedAsync(default);
    }
}