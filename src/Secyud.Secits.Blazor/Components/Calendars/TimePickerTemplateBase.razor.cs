using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract partial class TimePickerTemplateBase
{
    [Parameter]
    public TimePrecision Pression { get; set; }

    protected int Hour
    {
        get => GetValueOrDefault().Hour;
        set
        {
            value = (value + 24) % 24;
            OnTimeChanged(hour: value);
        }
    }

    protected int Minute
    {
        get => GetValueOrDefault().Minute;
        set
        {
            value = (value + 60) % 60;
            OnTimeChanged(minute: value);
        }
    }

    protected int Second
    {
        get => GetValueOrDefault().Second;
        set
        {
            value = (value + 60) % 60;
            OnTimeChanged(second: value);
        }
    }

    protected TimeOnly GetValueOrDefault()
    {
        return Master.InputInvoker.Get()?.GetActiveItem() ?? default;
    }

    protected void OnTimeChanged(int? hour = null, int? minute = null, int? second = null)
    {
        var time = new TimeOnly(
            hour ?? GetValueOrDefault().Hour,
            minute ?? GetValueOrDefault().Minute,
            second ?? GetValueOrDefault().Second);
        Master.InputInvoker.Get()?
            .SetActiveItemAsync(this, time).ConfigureAwait(false);
    }
}