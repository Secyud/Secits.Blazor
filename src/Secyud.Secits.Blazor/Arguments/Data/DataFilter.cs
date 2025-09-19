namespace Secyud.Secits.Blazor;

public abstract class DataFilter
{
    public bool Enabled { get; set; }
    public string? Field { get; set; }
    public abstract object? FilterValue { get; set; }
    public DataFilterType FilterType { get; set; }
}

public class DataFilter<TField> : DataFilter
{
    public TField FilterField { get; set; } = default!;

    public override object? FilterValue
    {
        get => FilterField;
        set => FilterField = value is TField f ? f : default!;
    }
}