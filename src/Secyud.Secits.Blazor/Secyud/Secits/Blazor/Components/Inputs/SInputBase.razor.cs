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

    #endregion

    #region DelegateValue

    [CascadingParameter]
    public IValueDelegateComponent<TValue>? ValueDelegate { get; set; }

    private void UnbindValueComponentDelegate()
    {
        if (ValueDelegate is not null)
            ValueDelegate.OnValueParameterSet = null;
    }

    private void BindValueComponentDelegate(IValueDelegateComponent<TValue>? component)
    {
        UnbindValueComponentDelegate();
        if (component is not null)
            component.OnValueParameterSet = OnValueParameterSet;

        ValueDelegate = component;
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
            if (ValueDelegate is not null)
                await ValueDelegate.ValueChanged.InvokeAsync(value);
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
                nameof(ValueDelegate), out var component))
            BindValueComponentDelegate(component);

        if (parameters.TryGetValue<int>(nameof(DelayInterval), out var delayInterval))
            BindValueDelayer(delayInterval);

        await base.SetParametersAsync(parameters);

        if (parameters.TryGetValue<TValue?>(
                nameof(Value), out var parameter))
            OnValueParameterSet(parameter);
    }

    private void OnValueParameterSet(TValue? value)
    {
        _currentValue = value;

        if (!TryConvertToField(value, out var output))
        {
            throw new InvalidCastException(
                $"Type {typeof(TValue)} value {_currentValue} cast to string failed!");
        }

        ResetCurrentString(output);
        Value = _currentValue;
        if (ValueDelegate is null) return;
        ValueDelegate.Value = _currentValue;
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

    #region Mask

    [CascadingParameter]
    public IInputMask? InputMask { get; set; }

    private bool _isMasked;
    private string? _maskedString;

    private bool TrySetMaskedString(string? text)
    {
        if (InputMask is null) return false;
        return InputMask.TryMaskValue(text,
            out _maskedString);
    }

    #endregion

    #region ValueHandle

    private string? _currentString;
    private string? _originString;
    private TValue? _currentValue;
    private bool _parsingFailed;

    protected void OnInput(ChangeEventArgs args)
    {
        var text = args.Value?.ToString();
        ResetCurrentString(text);
        ResetCurrentValue();
        if (ChangeMode == InputChangeMode.OnInput)
        {
            SubmitChange();
        }
    }

    protected void OnChange(ChangeEventArgs args)
    {
        var text = args.Value?.ToString();
        ResetCurrentString(text);
        ResetCurrentValue();
        SubmitChange();
    }

    protected void SubmitChange()
    {
        _delayer?.Update(_currentValue);
    }

    protected void ResetCurrentString(string? text)
    {
        _originString = text;
        _isMasked = TrySetMaskedString(text);
        _currentString = _isMasked ? _maskedString : _originString;
    }

    protected void ResetCurrentValue()
    {
        string? value;
        if (_isMasked && InputMask is not null)
        {
            InputMask.TryUnmaskValue(_maskedString, out value);
        }
        else
        {
            value = _originString;
        }

        _parsingFailed = !TryConvertToValue(value, out var output);

        if (!_parsingFailed)
        {
            _currentValue = output;
        }
    }

    #endregion
}