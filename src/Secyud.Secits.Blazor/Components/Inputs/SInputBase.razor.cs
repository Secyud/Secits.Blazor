using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Utils;

namespace Secyud.Secits.Blazor;

public abstract partial class SInputBase<TValue> :
    IValueComponent<TValue>, IThemeComponent, IChildContentComponent, IActivableComponent
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
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public bool Borderless { get; set; }

    [Parameter]
    public bool Shadow { get; set; }

    [Parameter]
    public bool Background { get; set; }

    [Parameter]
    public bool Angular { get; set; }

    [Parameter]
    public bool Rounded { get; set; }

    [Parameter]
    public InputChangeMode ChangeMode { get; set; } = InputChangeMode.OnInput;

    #endregion

    #region Life Cycle

    protected override void OnInitialized()
    {
        _delayer.Delayed += OnSubmitInvoke;
        base.OnInitialized();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter<int>(nameof(DelayInterval), interval =>
        {
            _delayer.DelayInterval = interval;
        });

        await base.SetParametersAsync(parameters);

        parameters.UseParameter<TValue>(nameof(Value), OnValueParameterSet);
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

    protected TValue CurrentValue { get; set; } = default!;

    protected virtual void OnValueParameterSet(TValue value)
    {
        CurrentValue = value;
        Value = CurrentValue;
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
        _delayer.Update(CurrentValue);
    }

    private void OnValueChanged(TValue? value)
    {
        ValueChanged.InvokeAsync(value);
    }

    #endregion
}