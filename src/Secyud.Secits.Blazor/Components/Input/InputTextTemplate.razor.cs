using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class InputTextTemplate<TValue> : ICanActive, IValueContainer
{
    protected virtual string? InputString => ParsingFailed ? ValueString : CurrentString;
    protected string? CurrentString { get; set; }
    protected string? ValueString { get; set; }
    protected bool ParsingFailed { get; set; }
    protected TValue CachedValue { get; set; } = default!;

    [Parameter]
    public bool SubmitOnInput { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? Attributes { get; set; }

    public async Task OnValueUpdatedAsync(object sender)
    {
        if (sender == this) return;
        if (Master.InputInvoker.Get() is { } invoker)
            CachedValue = invoker.GetActiveItem();
        else
            CachedValue = default!;
        ValueString = CachedValue?.ToString();
        CurrentString = ValueString;
        ParsingFailed = false;
        await Task.CompletedTask;
    }

    private async Task OnInputAsync(ChangeEventArgs args)
    {
        await OnInputInvokeAsync(args.Value?.ToString(), SubmitOnInput);
    }

    private async Task OnChangeAsync(ChangeEventArgs args)
    {
        await OnInputInvokeAsync(args.Value?.ToString());
    }

    protected virtual Task OnInputStringHandleAsync(string? str)
    {
        CurrentString = str;
        return Task.CompletedTask;
    }

    protected virtual Task OnInputValueHandleAsync()
    {
        switch (CurrentString)
        {
            case null:
                CachedValue = default!;
                ParsingFailed = false;
                break;
            case TValue value:
                CachedValue = value;
                ParsingFailed = false;
                break;
            default:
                if (Master.ValueConverter.Get() is { } converter)
                {
                    ParsingFailed = !converter.TryParse(CurrentString, out var value);
                    if (!ParsingFailed) CachedValue = value;
                    break;
                }

                try
                {
                    CachedValue = (TValue)Convert.ChangeType(CurrentString, typeof(TValue));
                    ParsingFailed = false;
                }
                catch (Exception)
                {
                    // ignored
                    ParsingFailed = true;
                }

                break;
        }

        var invoker = Master.InputInvoker.Get();
        if (invoker is not null)
        {
            var currentItem = invoker.GetActiveItem();

            var converter = Master.ValueConverter.Get();
            ValueString = converter is null
                ? currentItem?.ToString()
                : converter.ToString(currentItem);
        }

        return Task.CompletedTask;
    }

    protected virtual async Task OnInputInvokeAsync(string? value, bool submit = true)
    {
        if (Master.Readonly || Master.Disabled) return;
        await OnInputStringHandleAsync(value);
        await OnInputValueHandleAsync();
        if (submit && Master.InputInvoker.Get() is { } invoker)
        {
            await invoker.SetActiveItemAsync(this, CachedValue);
        }
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        Master.ValueContainer.Apply(this);
        OnValueUpdatedAsync(null!).ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        base.ForgoSetting();
        Master.ValueContainer.Forgo(this);
    }

    private string? GetReadOnly()
    {
        return Master.Readonly || Readonly ? "readonly" : null;
    }

    private string? GetDisabled()
    {
        return Master.Disabled || Disabled ? "disabled" : null;
    }
}