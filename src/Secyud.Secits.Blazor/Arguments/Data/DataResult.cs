namespace Secyud.Secits.Blazor.Arguments;

public class DataResult<TItem>
{
    public int TotalCount { get; set; }
    public IEnumerable<TItem> Items { get; set; } = [];
}