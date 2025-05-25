using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Abstraction;
using Secyud.Secits.Blazor.Arguments;
using Secyud.Secits.Blazor.Utils;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class SInputBase<TValue> :
    ISchValue<TValue>, IScsTheme, IScsActive
{
    protected override string ComponentName => "input";

    protected virtual string? InputType => null;

    #region Parameter

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }

    [Parameter]
    public string? Format { get; set; }

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    #endregion

    #region Life Cycle

    protected override void OnInitialized()
    {
        _delayer.Delayed += OnSubmitInvoke;
        _delayer.DelayInterval = DelayInterval;
        base.OnInitialized();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(DelayInterval, nameof(DelayInterval),
            interval => _delayer.DelayInterval = interval);
        parameters.UseParameter(Value, nameof(Value), OnValueParameterSet);

        await base.SetParametersAsync(parameters);
    }

    protected override ValueTask HandleDisposeAsync()
    {
        _delayer.Dispose();
        return base.HandleDisposeAsync();
    }

    #endregion

    #region Delayer

    private readonly ValueDelayer<TValue> _delayer = new();

    [Parameter]
    public int DelayInterval { get; set; } = 200;

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

    protected virtual void OnInputValueHandle(object? obj)
    {
        Value = (TValue)obj!;
    }

    protected void OnInput(ChangeEventArgs args)
    {
        OnInputInvoke(args.Value,
            ChangeMode == InputChangeMode.OnInput);
    }

    protected void OnChange(ChangeEventArgs args)
    {
        OnInputInvoke(args.Value);
    }

    protected void OnInputInvoke(object? value, bool submit = true)
    {
        if (Readonly || Disabled) return;
        OnInputValueHandle(value);
        if (submit) SubmitChange();
    }

    /// <summary>
    /// Submits the current value change to be processed by the delayer.
    /// This method is responsible for notifying the internal value delayer
    /// to update its state with the latest value. If no delayer is assigned,
    /// this method performs no action.
    /// </summary>
    protected virtual void SubmitChange()
    {
        _delayer.Update(Value);
    }

    protected virtual void OnValueChanged(TValue? value)
    {
        InvokeAsync(() => ValueChanged.InvokeAsync(value));
    }

    #endregion

    #region Clear

    [Parameter]
    public bool ShowClearButton { get; set; }

    protected void ClearValue()
    {
        OnValueParameterSet(default!);
        SubmitChange();
    }

    protected virtual void OnValueParameterSet(TValue value)
    {
    }

    #endregion
}