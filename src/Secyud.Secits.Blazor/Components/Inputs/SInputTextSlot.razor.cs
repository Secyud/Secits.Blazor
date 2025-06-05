using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public partial class SInputTextSlot<TValue> : ISciInputSlotRenderer<TValue>
{
    protected virtual string? InputString => ParsingFailed ? ValueString : CurrentString;
    protected string? CurrentString { get; set; }
    protected string? ValueString { get; set; }
    protected bool ParsingFailed { get; set; }

    [Parameter]
    public bool SubmitOnInput { get; set; }

    public virtual async Task SetValueFromParameterAsync(TValue value)
    {
        await InvokeAsync(() =>
        {
            ValueString = value?.ToString();
            CurrentString = ValueString;
            ParsingFailed = false;
        });
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
        if (Master is null) return Task.CompletedTask;

        switch (CurrentString)
        {
            case null:
                Master.CurrentValue = default!;
                ParsingFailed = false;
                break;
            case TValue value:
                Master.CurrentValue = value;
                ParsingFailed = false;
                break;
            default:
                if (Master.ValueConverter.Get() is { } converter)
                {
                    ParsingFailed = !converter.TryConvert(CurrentString, out var value);
                    if (!ParsingFailed) Master.CurrentValue = value;
                    break;
                }

                try
                {
                    Master.CurrentValue = (TValue)Convert.ChangeType(CurrentString, typeof(TValue));
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
        if (Master is null || Master.Readonly || Master.Disabled) return;
        await OnInputStringHandleAsync(value);
        await OnInputValueHandleAsync();
        await OnInputInvokeAsync(value);
        if (submit) await Master.OnValueChangedAsync(Master.CurrentValue);
    }
}