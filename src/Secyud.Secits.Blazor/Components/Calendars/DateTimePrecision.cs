namespace Secyud.Secits.Blazor;

public readonly struct DateTimePrecision
{
    internal DateTimePrecision(EDateTimePrecision precision)
    {
        Precision = precision;
    }

    public EDateTimePrecision Precision { get; }

    public static implicit operator DatePrecision(DateTimePrecision value)
    {
        if (value.Precision
            is EDateTimePrecision.Second
            or EDateTimePrecision.Minute
            or EDateTimePrecision.Hour)
            return Default;

        return new DatePrecision(value.Precision);
    }

    public static implicit operator TimePrecision(DateTimePrecision value)
    {
        if (value.Precision
            is EDateTimePrecision.Day
            or EDateTimePrecision.Month
            or EDateTimePrecision.Year)
            return Default;
        return new TimePrecision(value.Precision);
    }

    public static DateTimePrecision Default => new(EDateTimePrecision.Default);
    public static DateTimePrecision Second => new(EDateTimePrecision.Second);
    public static DateTimePrecision Minute => new(EDateTimePrecision.Minute);
    public static DateTimePrecision Hour => new(EDateTimePrecision.Hour);
    public static DateTimePrecision Day => new(EDateTimePrecision.Day);
    public static DateTimePrecision Month => new(EDateTimePrecision.Month);
    public static DateTimePrecision Year => new(EDateTimePrecision.Year);
}