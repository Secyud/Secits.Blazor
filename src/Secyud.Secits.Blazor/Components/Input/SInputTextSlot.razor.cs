using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SInputTextSlot<TValue> : ISciValueContainer<TValue>
{
    protected virtual string? InputString => ParsingFailed ? ValueString : CurrentString;
    protected string? CurrentString { get; set; }
    protected string? ValueString { get; set; }
    protected bool ParsingFailed { get; set; }
    protected TValue CachedValue { get; set; } = default!;

    [Parameter]
    public bool SubmitOnInput { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? Attributes { get; set; }

    public Task SetValueFromParameterAsync(TValue value)
    {
        CachedValue = value;
        ValueString = value?.ToString();
        CurrentString = ValueString;
        ParsingFailed = false;
        return Task.CompletedTask;
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
                    ParsingFailed = !converter.TryConvert(CurrentString, out var value);
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

        ValueString = Master.CurrentValue?.ToString();
        return Task.CompletedTask;
    }

    protected virtual async Task OnInputInvokeAsync(string? value, bool submit = true)
    {
        if (Master.Readonly || Master.Disabled) return;
        await OnInputStringHandleAsync(value);
        await OnInputValueHandleAsync();
        if (submit)
            await Master.OnValueChangedAsync(CachedValue);
    }

    protected override void ApplySetting()
    {
        Master.ValueContainer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ValueContainer.Forgo(this);
    }
}