namespace Secyud.Secits.Blazor.Arguments;

public abstract class DataResult
{
    public int TotalCount { get; set; }

    public static DataResult<TItem> Create<TItem>(
        IEnumerable<TItem> items, int totalCount)
    {
        return new DataResult<TItem>
        {
            Items = items,
            TotalCount = totalCount,
        };
    }
}

public class DataResult<TItem> : DataResult
{
    public IEnumerable<TItem> Items { get; set; } = [];
}