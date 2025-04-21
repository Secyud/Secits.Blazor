using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Parameters;
using Secyud.Secits.Utils;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class SInputBase<TValue> :
    IValueComponent<TValue>, IColorComponent
{
    protected override string ComponentName => "input";

    #region Parameters

    [Parameter]
    public ColorType Color { get; set; } = ColorType.Primary;

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    [CascadingParameter]
    public IInputMask? InputMask { get; set; }

    #endregion

    #region DelegateValueComponent

    [CascadingParameter]
    public IValueDelegateComponent<TValue>? DelegateValue { get; set; }

    private void UnbindValueComponentDelegate()
    {
        if (DelegateValue is not null)
        {
            DelegateValue.ValueSetAction = null;
        }
    }

    private void BindValueComponentDelegate(IValueDelegateComponent<TValue>? component)
    {
        UnbindValueComponentDelegate();
        if (component is not null)
        {
            component.ValueSetAction = OnParameterChanged;
        }

        DelegateValue = component;
    }

    #endregion

    #region Delayer

    private ValueDelayer<TValue>? _delayer;

    [Parameter]
    public int DelayInterval { get; set; } = 200;

    private void UnbindValueDelayer()
    {
        if (_delayer is null) return;
        _delayer.Delayed -= OnSubmitInvoke;
        _delayer.Dispose();
        _delayer = null;
    }

    private void BindValueDelayer(int timeInterval)
    {
        UnbindValueDelayer();
        _delayer = new ValueDelayer<TValue>(timeInterval);
        _delayer.Delayed += OnSubmitInvoke;
    }

    private void OnSubmitInvoke(object? sender, TValue? value)
    {
        InvokeAsync(async () =>
        {
            await ValueChanged.InvokeAsync(value);
            if (DelegateValue is not null)
                await DelegateValue.ValueChanged.InvokeAsync(value);
        });
    }

    #endregion

    #region Initialize

    protected override void OnInitialized()
    {
        BindValueDelayer(DelayInterval);
        base.OnInitialized();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<IValueDelegateComponent<TValue>>(
                nameof(DelegateValue), out var component))
            BindValueComponentDelegate(component);

        if (parameters.TryGetValue<int>(nameof(DelayInterval), out var delayInterval))
            BindValueDelayer(delayInterval);

        await base.SetParametersAsync(parameters);

        if (parameters.TryGetValue<TValue?>(
                nameof(Value), out var parameter))
        {
            OnParameterChanged(parameter);
        }
    }

    private void OnParameterChanged(TValue? value)
    {
        _currentValue = value;

        if (!TryConvertToField(value, out var output))
        {
            throw new InvalidCastException(
                $"Type {typeof(TValue)} value {_currentValue} cast to string failed!");
        }

        ResetCurrentString(output);
        Value = _currentValue;
        if (DelegateValue is null) return;
        DelegateValue.Value = _currentValue;
    }

    protected override ValueTask HandleDisposeAsync()
    {
        UnbindValueComponentDelegate();
        UnbindValueDelayer();
        return base.HandleDisposeAsync();
    }

    #endregion

    #region Converter

    protected abstract bool TryConvertToValue(string? value, out TValue? output);

    protected virtual bool TryConvertToField(TValue? value, out string? output)
    {
        output = value?.ToString();
        return true;
    }

    #endregion
}