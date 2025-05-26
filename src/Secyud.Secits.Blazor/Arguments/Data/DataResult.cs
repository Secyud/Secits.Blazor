namespace Secyud.Secits.Blazor.Arguments;

public class DataResult<TItem>
{
    public DataResult(IEnumerable<TItem> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public int TotalCount { get; set; }
    
    public IEnumerable<TItem> Items { get; set; }
}