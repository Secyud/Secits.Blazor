using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class TimePickerSlot : SSettingBase<SInput<TimeOnly>>, ILayoutSlotRenderer
{
    public abstract RenderFragment RenderSlot();

    [Parameter]
    public TimePrecision Pression { get; set; }

    protected TimeOnly ValueOrDefault => Master.CurrentValue;
    
    protected override void ApplySetting()
    {
        Master.SlotRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.SlotRenderer.Forgo(this);
    }

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
        Master.OnValueChangedAsync(time).ConfigureAwait(false);
    }
}