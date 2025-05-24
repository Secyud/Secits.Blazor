namespace Secyud.Secits.Blazor.Arguments;

public class DataFilter
{
    public bool Enabled { get; set; }
    public string? Field { get; set;} 
    public object? FilterValue { get; set; }
    public DataFilterType FilterType { get; set; }
}