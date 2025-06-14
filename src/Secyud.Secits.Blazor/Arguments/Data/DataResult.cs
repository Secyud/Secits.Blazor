namespace Secyud.Secits.Blazor;

public class DataResult<TItem>
{
    public int TotalCount { get; set; }
    public IEnumerable<TItem> Items { get; set; } = [];
}