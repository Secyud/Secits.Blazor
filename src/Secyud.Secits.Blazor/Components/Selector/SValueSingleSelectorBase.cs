using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SValueSingleSelectorBase<TComponent, TValue> : SSelectorBase<TComponent, TValue>,
    ISchValue<TValue>
    where TComponent : SComponentBase
{
    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public RenderFragment<SSingleSelectorContext<TValue>>? SelectContent { get; set; }

    public override RenderFragment? GenerateSelectedContent() =>
        SelectContent?.Invoke(new(this, Value));

    public override async Task OnSelectionActivateAsync(TValue selection)
    {
        await OnValueSelectChangedAsync(Equals(selection, Value) ? default : selection);
    }

    protected virtual async Task OnValueSelectChangedAsync(TValue? value)
    {
        Value = value!;
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);
    }

    public override bool IsSelected(TValue? selection)
    {
        return selection is not null && Equals(Value, selection);
    }

    public override async Task ClearSelectAsync()
    {
        await OnValueSelectChangedAsync(default);
    }
}