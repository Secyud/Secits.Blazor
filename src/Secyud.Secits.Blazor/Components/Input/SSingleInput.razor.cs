using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SSingleInput<TValue> : IHasValue<TValue>, IInputInvokable<TValue>
{
    #region Parameters

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    #endregion

    public TValue CurrentValue { get; protected set; } = default!;

    public override async Task SetSingleValue(TValue value)
    {
        CurrentValue = value;
        await InputInvoker.InvokeAsync(
            u => u.InvokeValueChanged(this, value),
            () => ValueChanged.InvokeAsync(value));
    }

    public override TValue GetSingleValue()
    {
        return CurrentValue;
    }

    public override Task OnValueCleared()
    {
        CurrentValue = default!;
        return Task.CompletedTask;
    }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(Value, nameof(Value), async value =>
        {
            CurrentValue = value;
            await ValueContainer.InvokeAsync(u => u.SetValueFromParameterAsync(value));
        });
    }

    public async Task OnValueChangedAsync(TValue value)
    {
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);
    }

    public SSetting<IInputInvoker<TValue>> InputInvoker { get; } = new();
}