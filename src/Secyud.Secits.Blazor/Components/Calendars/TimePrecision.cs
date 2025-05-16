namespace Secyud.Secits.Blazor;

public readonly struct TimePrecision
{
    internal TimePrecision(EDateTimePrecision precision)
    {
        Precision = precision;
    }

    public EDateTimePrecision Precision { get; }
    
    public static implicit operator DateTimePrecision(TimePrecision value)
    {
        return new DateTimePrecision(value.Precision);
    }

    public static TimePrecision Default => new(EDateTimePrecision.Default);
    public static TimePrecision Second => new(EDateTimePrecision.Second);
    public static TimePrecision Minute => new(EDateTimePrecision.Minute);
    public static TimePrecision Hour => new(EDateTimePrecision.Hour);
}