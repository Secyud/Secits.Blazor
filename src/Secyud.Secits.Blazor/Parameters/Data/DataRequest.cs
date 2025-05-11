namespace Secyud.Secits.Blazor;

public class DataRequest
{
    public const int DefaultPageSize = 10;
    
    public int PageIndex { get; set; }

    public int PageSize { get; set; } = DefaultPageSize;

    public List<DataSorter> Sorters { get; } = [];

    public List<DataFilter> Filters { get; } = [];
}