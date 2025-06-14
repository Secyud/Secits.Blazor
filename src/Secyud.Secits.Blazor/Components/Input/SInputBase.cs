using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SInputBase<TValue> : SActivableBase
{
    #region Parameters

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    #endregion

    public TValue CurrentValue { get; protected set; } = default!;

    public virtual async Task OnValueChangedAsync(TValue value)
    {
        CurrentValue = value;
        await ValueChanged.InvokeAsync(value);
    }
}