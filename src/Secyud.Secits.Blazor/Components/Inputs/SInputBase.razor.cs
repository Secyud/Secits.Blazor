using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Utils;

namespace Secyud.Secits.Blazor;

public abstract partial class SInputBase<TValue> :
    IValueComponent<TValue>, IThemeComponent, IChildContentComponent
{
    protected override string ComponentName => "input";

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
    public STheme Theme { get; set; }

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
        parameters.UseParameter<IValueDelegateComponent<TValue>>(
            nameof(ValueDelegate), BindValueComponentDelegate);
        
        parameters.UseParameter<int>(nameof(DelayInterval), BindValueDelayer);

        await base.SetParametersAsync(parameters);
        
        parameters.UseParameter<TValue>(nameof(Value), OnValueParameterSet);
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

    /// <summary>
    /// Unbinds and disposes the current value delayer instance.
    /// This method ensures that any delayed event subscriptions are removed
    /// and resources associated with the delayer are properly released.
    /// If no delayer is currently assigned, this method performs no action.
    /// </summary>
    private void UnbindValueDelayer()
    {
        if (_delayer is null) return;
        _delayer.Delayed -= OnSubmitInvoke;
        _delayer.Dispose();
        _delayer = null;
    }

    /// <summary>
    /// Binds a new value delayer instance with the specified time interval.
    /// This method first unbinds and disposes of any existing value delayer to ensure proper resource management.
    /// A new value delayer is then created and configured with the provided time interval.
    /// The <see cref="ValueDelayer{TValue}.Delayed"/> event is subscribed to invoke the <see cref="OnSubmitInvoke"/> method.
    /// </summary>
    /// <param name="timeInterval">The delay interval in milliseconds for the value delayer.</param>
    private void BindValueDelayer(int timeInterval)
    {
        UnbindValueDelayer();
        _delayer = new ValueDelayer<TValue>(timeInterval);
        _delayer.Delayed += OnSubmitInvoke;
    }

    /// <summary>
    /// Invokes the value change handling logic when a delayed event is triggered.
    /// This method is called in response to the <see cref="ValueDelayer{TValue}.Delayed"/> event
    /// and is responsible for propagating the updated value to the <see cref="OnValueChanged"/> method.
    /// </summary>
    /// <param name="sender">The source of the event, typically the <see cref="ValueDelayer{TValue}"/> instance.</param>
    /// <param name="value">The updated value that was delayed and is now being submitted.</param>
    private void OnSubmitInvoke(object? sender, TValue? value)
    {
        OnValueChanged(value);
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

    /// <summary>
    /// Submits the current value change to be processed by the delayer.
    /// This method is responsible for notifying the internal value delayer
    /// to update its state with the latest value. If no delayer is assigned,
    /// this method performs no action.
    /// </summary>
    protected void SubmitChange()
    {
        if (_delayer is null)
        {
            OnValueChanged(CurrentValue);
        }
        else
        {
            _delayer.Update(CurrentValue);
        }
    }

    private void OnValueChanged(TValue? value)
    {
        InvokeAsync(async () =>
        {
            await ValueChanged.InvokeAsync(value);
            if (ValueDelegate is not null)
                await ValueDelegate.ValueChanged.InvokeAsync(value);
        });
    }

    #endregion
}