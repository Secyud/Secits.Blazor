using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Utils;

namespace Secyud.Secits.Blazor;

public abstract partial class SInputBase<TValue> :
    IValueComponent<TValue>, IColorComponent, IChildContentComponent
{
    protected override string ComponentName => "ipt";

    protected virtual string? InputType => null;

    #region Parameter

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public ColorType Color { get; set; }

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    #endregion

    #region Life Cycle

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

        if (parameters.TryGetValue<TValue>(nameof(Value), out var parameter))
            OnValueParameterSet(parameter);
    }

    protected override ValueTask HandleDisposeAsync()
    {
        UnbindValueComponentDelegate();
        UnbindValueDelayer();
        return base.HandleDisposeAsync();
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

    #region ValueHandle

    protected TValue CurrentValue { get; set; } = default!;

    protected virtual void OnValueParameterSet(TValue value)
    {
        CurrentValue = value;
        Value = CurrentValue;
        if (ValueDelegate is null) return;
        ValueDelegate.Value = CurrentValue;
    }

    protected virtual void OnInputValueHandle(object? obj)
    {
        CurrentValue = (TValue)Convert.ChangeType(obj, typeof(TValue))!;
    }

    protected void OnInput(ChangeEventArgs args)
    {
        OnInputValueHandle(args.Value);
        if (ChangeMode == InputChangeMode.OnInput)
            SubmitChange();
    }

    protected void OnChange(ChangeEventArgs args)
    {
        OnInputValueHandle(args.Value);
        SubmitChange();
    }

    protected void SubmitChange()
    {
        _delayer?.Update(CurrentValue);
    }

    #endregion
}