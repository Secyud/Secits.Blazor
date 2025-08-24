﻿using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract partial class TimePickerTemplateBase
{
    [Parameter]
    public TimePrecision Pression { get; set; }

    protected TimeOnly ValueOrDefault => Master.CurrentValue;
    
    protected int Hour
    {
        get => ValueOrDefault.Hour;
        set
        {
            value = (value + 24) % 24;
            OnTimeChanged(hour: value);
        }
    }

    protected int Minute
    {
        get => ValueOrDefault.Minute;
        set
        {
            value = (value + 60) % 60;
            OnTimeChanged(minute: value);
        }
    }

    protected int Second
    {
        get => ValueOrDefault.Second;
        set
        {
            value = (value + 60) % 60;
            OnTimeChanged(second: value);
        }
    }

    protected void OnTimeChanged(int? hour = null, int? minute = null, int? second = null)
    {
        var time = new TimeOnly(
            hour ?? ValueOrDefault.Hour,
            minute ?? ValueOrDefault.Minute,
            second ?? ValueOrDefault.Second);
        Master.SetSingleValue(time).ConfigureAwait(false);
    }
}