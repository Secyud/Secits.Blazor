namespace Secyud.Secits.Blazor.Components;

public abstract partial class STimePickerBase
{
    protected TimeOnly ValueOrDefault => Value ?? default;

    protected int Hour
    {
        get => ValueOrDefault.Hour;
        set => OnTimeChanged(hour: value);
    }

    protected int Minute
    {
        get => ValueOrDefault.Minute;
        set => OnTimeChanged(minute: value);
    }

    protected int Second
    {
        get => ValueOrDefault.Second;
        set => OnTimeChanged(second: value);
    }

    protected void OnTimeChanged(
        int? hour = null, int? minute = null, int? second = null)
    {
        var time = new TimeOnly(
            hour ?? ValueOrDefault.Hour,
            minute ?? ValueOrDefault.Minute,
            second ?? ValueOrDefault.Second);
        OnInputInvoke(time);
    }
}