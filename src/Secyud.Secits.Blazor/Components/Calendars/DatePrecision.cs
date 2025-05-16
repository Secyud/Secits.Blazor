namespace Secyud.Secits.Blazor;

public readonly struct DatePrecision
{
    internal DatePrecision(EDateTimePrecision precision)
    {
        Precision = precision;
    }

    public EDateTimePrecision Precision { get; }
    
    public static implicit operator DateTimePrecision(DatePrecision value)
    {
        return new DateTimePrecision(value.Precision);
    }

    public static DatePrecision Default => new(EDateTimePrecision.Default);
    public static DatePrecision Day => new(EDateTimePrecision.Day);
    public static DatePrecision Month => new(EDateTimePrecision.Month);
    public static DatePrecision Year => new(EDateTimePrecision.Year);
}