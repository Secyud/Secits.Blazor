using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Basic;
using Secyud.Secits.Blazor.Parameters;
using Secyud.Secits.Utils;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class SInputBase<TValue> :
    IValueComponent<TValue>, IColorComponent
{
    protected override string ComponentName => "ipt";

    #region Parameters

    [Parameter]
    public ColorType Color { get; set; } = ColorType.Primary;

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected override void BuildInitialClassStyle(ClassStyleBuilderContext context)
    {
        context.AppendClass("bd");
    }

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

        if (parameters.TryGetValue<TValue>(
                nameof(Value), out var parameter))
            OnValueParameterSet(parameter);
    }

    private void OnValueParameterSet(TValue value)
    {
        _currentValue = value;

        // ensure value can be cast to string
        if (!TryConvertToField(value, out var output))
        {
            throw new InvalidCastException(
                $"Type {typeof(TValue)} value {_currentValue} cast to string failed!");
        }

        ResetCurrentStringFromValue(output);
        _parsingFailed = false;
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

    protected abstract bool TryConvertToValue(string? value, out TValue output);

    protected virtual bool TryConvertToField(TValue value, out string? output)
    {
        output = value?.ToString();
        return true;
    }

    #endregion

    #region Mask

    public IInputMask? InputMask { get; set; }

    private bool _isMasked;
    private string? _maskedString;

    private bool TryParseMask(string? text)
    {
        _originString = text;
        if (InputMask is null) return false;
        var res = InputMask.TryParseMask(text, out _maskedString);

        // ensure input mask worked for masked string to value
        if (!InputMask.TryConvertMaskToText(_maskedString, out var origin))
        {
            throw new InvalidCastException(
                $"Please chack mask: {InputMask}, TryConvertMaskToText failed!" +
                $"\r\nmasked string: {_maskedString}.");
        }

        _originString = origin;

        return res;
    }

    private bool TryConvertTextToMask(string? text)
    {
        if (InputMask is null) return false;
        return InputMask.TryConvertTextToMask(text,
            out _maskedString);
    }

    #endregion

    #region ValueHandle

    private string? _currentString;
    private string? _originString;
    private TValue _currentValue = default!;
    private bool _parsingFailed;

    protected void OnInput(ChangeEventArgs args)
    {
        var text = args.Value?.ToString();
        ResetCurrentStringFromInput(text);
        ResetCurrentValue();
        if (ChangeMode == InputChangeMode.OnInput)
            SubmitChange();
    }

    protected void OnChange(ChangeEventArgs args)
    {
        var text = args.Value?.ToString();
        ResetCurrentStringFromInput(text);
        ResetCurrentValue();
        SubmitChange();
    }

    protected void SubmitChange()
    {
        _delayer?.Update(_currentValue);
    }

    protected void ResetCurrentStringFromInput(string? inputText)
    {
        _originString = inputText;
        _isMasked = TryParseMask(inputText);
        _currentString = _isMasked ? _maskedString : _originString;
    }

    protected void ResetCurrentStringFromValue(string? value)
    {
        _originString = value;
        _isMasked = TryConvertTextToMask(value);
        _currentString = _isMasked ? _maskedString : _originString;
    }

    protected void ResetCurrentValue()
    {
        string? value;
        if (_isMasked && InputMask is not null)
            InputMask.TryConvertMaskToText(_maskedString, out value);
        else
            value = _originString;

        _parsingFailed = !TryConvertToValue(value, out var output);

        if (!_parsingFailed) _currentValue = output;
    }

    #endregion

    #region Clear

    [Parameter]
    public bool ClearButtonVisible { get; set; } = true;

    protected void ClearValue()
    {
        OnValueParameterSet(default!);
        SubmitChange();
    }
    
    #endregion
}