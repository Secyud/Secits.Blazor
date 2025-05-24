namespace Secyud.Secits.Blazor.Components;

public readonly struct TimePrecision
{
    internal TimePrecision(DateTimePrecisionKind precisionKind)
    {
        PrecisionKind = precisionKind;
    }

    public DateTimePrecisionKind PrecisionKind { get; }
    
    public static implicit operator DateTimePrecision(TimePrecision value)
    {
        return new DateTimePrecision(value.PrecisionKind);
    }

    public static TimePrecision Default => new(DateTimePrecisionKind.Default);
    public static TimePrecision Second => new(DateTimePrecisionKind.Second);
    public static TimePrecision Minute => new(DateTimePrecisionKind.Minute);
    public static TimePrecision Hour => new(DateTimePrecisionKind.Hour);
}